using System.Data;
using Dapper;
using Dba.TestTask.Backend.Application.Persistence.Interfaces;
using Dba.TestTask.Backend.Domain.Entities;
using Dba.TestTask.Backend.Domain.Enumerations;
using Dba.TestTask.Backend.Domain.ValueTypes;
using Dba.TestTask.Backend.Persistence.Factories.Interfaces;
using Dba.TestTask.Backend.Persistence.Queries;

namespace Dba.TestTask.Backend.Persistence.Repositories;

public class AbonentRepository : IAbonentRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public AbonentRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

    public async Task<Abonent?> GetAbonentAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();

        IEnumerable<Abonent?> abonents = await connection.QueryAsync<Abonent, Address,
            Street, PhoneNumber, PhoneNumber, PhoneNumber, Abonent>(
            ConstantQueries.GetAbonentsById,
            (abonent, address, street, homeNumber, workNumber, mobileNumber) =>
            {
                address.Street = street;
                abonent.Address = address;
                abonent.HomePhone = homeNumber;
                abonent.WorkPhone = workNumber;
                abonent.MobilePhone = mobileNumber;
                return abonent;
            },
            param: new { Id = id },
            splitOn: "Id,Id,Id,Id,Id");

        var abonentsArray = abonents as Abonent[] ?? abonents.ToArray();
        return abonentsArray.Length == 0 ? default : abonentsArray.First();
    }

    public async Task<IEnumerable<Abonent>> GetAbonentsAsync()
    {
        using var connection = _connectionFactory.CreateConnection();

        var abonents = await connection.QueryAsync<Abonent, Address,
            Street, PhoneNumber, PhoneNumber, PhoneNumber, Abonent>(
            ConstantQueries.GetAbonents,
            (abonent, address, street, homeNumber, workNumber, mobileNumber) =>
            {
                address.Street = street;
                abonent.Address = address;
                abonent.HomePhone = homeNumber;
                abonent.WorkPhone = workNumber;
                abonent.MobilePhone = mobileNumber;
                return abonent;
            },
            splitOn: "Id,Id,Id,Id,Id");

        return abonents;
    }

    public async Task<IEnumerable<Abonent>> GetAbonentsByPhoneNumber(string number)
    {
        using var connection = _connectionFactory.CreateConnection();

        var abonents = await connection.QueryAsync<Abonent, Address,
            Street, PhoneNumber, PhoneNumber, PhoneNumber, Abonent>(
            ConstantQueries.GetAbonentsByPhoneNumber,
            (abonent, address, street, homeNumber, workNumber, mobileNumber) =>
            {
                address.Street = street;
                abonent.Address = address;
                abonent.HomePhone = homeNumber;
                abonent.WorkPhone = workNumber;
                abonent.MobilePhone = mobileNumber;
                return abonent;
            },
            param: new { Number = number },
            splitOn: "Id,Id,Id,Id,Id");

        return abonents;
    }

    public async Task<int> AddAbonentAsync(Abonent abonent)
    {
        using var connection = _connectionFactory.CreateConnection();

        var streetId = await GetOrCreateStreetIdAsync(connection, abonent.Address.Street.Name);
        
        var addressId = await connection.QuerySingleAsync<int>(ConstantQueries.InsertAddress, new
        {
            HouseNumber = abonent.Address.HouseNumber,
            FlatNumber = abonent.Address.FlatNumber,
            StreetId = streetId
        });

        var abonentId = await connection.QuerySingleAsync<int>(ConstantQueries.InsertAbonent, new
        {
            Surname = abonent.Surname,
            Name = abonent.Name,
            MiddleName = abonent.MiddleName,
            AddressId = addressId
        });

        await InsertPhoneNumberAsync(connection, abonent.HomePhone, abonentId);
        await InsertPhoneNumberAsync(connection, abonent.WorkPhone, abonentId);
        await InsertPhoneNumberAsync(connection, abonent.MobilePhone, abonentId);
        
        return abonentId;
    }

    public async Task UpdateAbonentAsync(Abonent abonent)
    {
        using var connection = _connectionFactory.CreateConnection();

        var streetId = await GetOrCreateStreetIdAsync(connection, abonent.Address.Street.Name);

        await connection.ExecuteAsync(ConstantQueries.UpdateAbonent, new
        {
            Id = abonent.Id,
            Surname = abonent.Surname,
            Name = abonent.Name,
            MiddleName = abonent.MiddleName
        });

        await connection.ExecuteAsync(ConstantQueries.UpdateAddress, new
        {
            StreetId = streetId,
            HouseNumber = abonent.Address.HouseNumber,
            FlatNumber = abonent.Address.FlatNumber,
            Id = abonent.Address.Id
        });
        
        await RecreateOrDeleteNumberAsync(connection, abonent.HomePhone, PhoneType.Home, abonent.Id);
        await RecreateOrDeleteNumberAsync(connection, abonent.WorkPhone, PhoneType.Work, abonent.Id);
        await RecreateOrDeleteNumberAsync(connection, abonent.MobilePhone, PhoneType.Mobile, abonent.Id);
    }

    public async Task RemoveAbonentAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();

        await connection.ExecuteAsync(ConstantQueries.RemovePhonesByAbonentId, new { AbonentId = id });

        await connection.ExecuteAsync(ConstantQueries.RemoveAbonent, new { Id = id });
    }

    private async Task InsertPhoneNumberAsync(IDbConnection connection, PhoneNumber? phoneNumber, int abonentId)
    {
        if (phoneNumber == null) return;
        await connection.ExecuteAsync(ConstantQueries.InsertPhoneNumber, new
        {
            Number = phoneNumber.Number,
            PhoneType = phoneNumber.Type,
            AbonentId = abonentId
        });
    }

    private async Task RecreateOrDeleteNumberAsync(IDbConnection connection, PhoneNumber? phoneNumber, PhoneType type, int abonentId)
    {
        if (phoneNumber == null)
        {
            await connection.ExecuteAsync(ConstantQueries.RemoveConcretePhoneByAbonentId,
                new { AbonentId = abonentId, PhoneType = type });
        }
        else
        {
            await connection.ExecuteAsync(ConstantQueries.RemoveConcretePhoneByAbonentId,
                new { AbonentId = abonentId, PhoneType = type });
            await connection.ExecuteAsync(ConstantQueries.InsertPhoneNumber,
                new { AbonentId = abonentId, PhoneType = type, Number = phoneNumber.Number });
        }
    }

    private async Task<int> GetOrCreateStreetIdAsync(IDbConnection connection, string name)
    {
        var streetId = await connection.QuerySingleOrDefaultAsync<int?>(ConstantQueries.GetStreetByName,
            new { Name = name });

        return streetId ?? await connection.QuerySingleAsync<int>(ConstantQueries.InsertStreet, new { Name = name });
    }
}