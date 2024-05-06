using Dba.TestTask.Backend.Application.Exceptions;
using Dba.TestTask.Backend.Application.Mapping.Static.Abonents;
using Dba.TestTask.Backend.Application.Persistence.Interfaces;
using Dba.TestTask.Backend.Application.Requests.Abonents;
using Dba.TestTask.Backend.Application.Services.Interfaces;
using Dba.TestTask.Backend.Application.ViewModels.Abonents;
using Dba.TestTask.Backend.Application.ViewModels.StreetStatistics;
using Dba.TestTask.Backend.Domain.Entities;
using Dba.TestTask.Backend.Domain.Enumerations;
using Dba.TestTask.Backend.Domain.ValueTypes;

namespace Dba.TestTask.Backend.Application.Services;

public class AbonentService : IAbonentService
{
    private readonly IAbonentRepository _repository;

    public AbonentService(IAbonentRepository repository) => _repository = repository;
    
    public async Task<int> AddAbonentAsync(AddAbonentRequest request)
    {
        var abonent = AbonentStaticMapper.MapFromRequest(request);
        return await _repository.AddAbonentAsync(abonent);
    }

    public async Task<AbonentViewModel> GetAbonentAsync(int id)
    {
        var abonent = await _repository.GetAbonentAsync(id);
        if (abonent == default) throw new NotFoundException(typeof(Abonent), id);
        return AbonentStaticMapper.MapFromEntity(abonent);
    }

    public async Task<AbonentViewModelCollection> GetAbonentsAsync()
    {
        var abonents = await _repository.GetAbonentsAsync();
        var abonentsVms = abonents
            .Select(AbonentStaticMapper.MapFromEntity)
            .ToList();
        
        return new AbonentViewModelCollection
        {
            Abonents = abonentsVms
        };
    }

    public async Task<AbonentViewModelCollection> GetAbonentsByPhoneAsync(string phone)
    {
        var abonents = await _repository.GetAbonentsByPhoneNumber(phone);
        var abonentsVms = abonents
            .Select(AbonentStaticMapper.MapFromEntity)
            .ToList();

        return new AbonentViewModelCollection
        {
            Abonents = abonentsVms
        };
    }

    public async Task<StreetStatisticViewModelCollection> GetStreetStatisticsAsync()
    {
        var abonents = await _repository.GetAbonentsAsync();
        var streetStatistics = abonents
            .GroupBy(abonent => abonent.Address.Street.Name)
            .Select(group => new StreetStatisticViewModel
            {
                StreetName = group.Key,
                AbonentsCount = group.Count()
            });

        return new StreetStatisticViewModelCollection { Statistics = streetStatistics.ToList() };
    }

    public async Task RemoveAbonentAsync(int id)
    {
        var abonent = await _repository.GetAbonentAsync(id);
        if (abonent == default) throw new NotFoundException(typeof(Abonent), id);
        await _repository.RemoveAbonentAsync(id);
    }

    public async Task UpdateAbonentAsync(UpdateAbonentRequest request)
    {
        var abonent = await _repository.GetAbonentAsync(request.Id);
        
        if (abonent == default) throw new NotFoundException(typeof(Abonent), request.Id);
        
        abonent.Surname = request.Surname;
        abonent.Name = request.Name;
        abonent.MiddleName = request.MiddleName;
        abonent.Address.FlatNumber = request.Address.FlatNumber;
        abonent.Address.HouseNumber = request.Address.HouseNumber;
        abonent.Address.Street.Name = request.Address.StreetName;

        abonent.HomePhone = CreatePhoneNumber(request.HomePhone, PhoneType.Home);

        abonent.WorkPhone = CreatePhoneNumber(request.WorkPhone, PhoneType.Work);

        abonent.MobilePhone = CreatePhoneNumber(request.MobilePhone, PhoneType.Mobile);
        
        await _repository.UpdateAbonentAsync(abonent);
    }

    private PhoneNumber? CreatePhoneNumber(string? number, PhoneType type)
    {
        if (string.IsNullOrEmpty(number)) return null;
        return new PhoneNumber
        {
            Number = number,
            Type = type
        };
    }
}