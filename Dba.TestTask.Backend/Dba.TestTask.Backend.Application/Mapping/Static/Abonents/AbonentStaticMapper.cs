using Dba.TestTask.Backend.Application.Informations;
using Dba.TestTask.Backend.Application.Requests.Abonents;
using Dba.TestTask.Backend.Application.ViewModels.Abonents;
using Dba.TestTask.Backend.Domain.Entities;
using Dba.TestTask.Backend.Domain.Enumerations;
using Dba.TestTask.Backend.Domain.ValueTypes;

namespace Dba.TestTask.Backend.Application.Mapping.Static.Abonents;

public static class AbonentStaticMapper
{
    public static Abonent MapFromRequest(AddAbonentRequest request)
    {
        var homeNumber = CreatePhoneNumber(request.HomePhone, PhoneType.Home);
        var workNumber = CreatePhoneNumber(request.WorkPhone, PhoneType.Work);
        var mobileNumber = CreatePhoneNumber(request.MobilePhone, PhoneType.Mobile);

        var street = new Street { Name = request.Address.StreetName };

        var address = new Address
        {
            HouseNumber = request.Address.HouseNumber,
            FlatNumber = request.Address.FlatNumber,
            Street = street
        };

        return new Abonent
        {
            Surname = request.Surname,
            Name = request.Name,
            MiddleName = request.MiddleName,
            Address = address,
            HomePhone = homeNumber,
            WorkPhone = workNumber,
            MobilePhone = mobileNumber
        };
    }
    
    public static Abonent MapFromRequest(UpdateAbonentRequest request)
    {
        var homeNumber = CreatePhoneNumber(request.HomePhone, PhoneType.Home);
        var workNumber = CreatePhoneNumber(request.WorkPhone, PhoneType.Work);
        var mobileNumber = CreatePhoneNumber(request.MobilePhone, PhoneType.Mobile);

        var street = new Street { Name = request.Address.StreetName };

        var address = new Address
        {
            HouseNumber = request.Address.HouseNumber,
            FlatNumber = request.Address.FlatNumber,
            Street = street
        };

        return new Abonent
        {
            Id = request.Id,
            Surname = request.Surname,
            Name = request.Name,
            MiddleName = request.MiddleName,
            Address = address,
            HomePhone = homeNumber,
            WorkPhone = workNumber,
            MobilePhone = mobileNumber
        };
    }

    public static AbonentViewModel MapFromEntity(Abonent abonent)
    {
        var homeNumber = abonent.HomePhone?.Number;
        var workNumber = abonent.WorkPhone?.Number;
        var mobileNumber = abonent.MobilePhone?.Number;

        return new AbonentViewModel
        {
            Id = abonent.Id,
            Surname = abonent.Surname,
            Name = abonent.Name,
            MiddleName = abonent.MiddleName,
            Address = new AddressInformation
            {
                StreetName = abonent.Address.Street.Name,
                HouseNumber = abonent.Address.HouseNumber,
                FlatNumber = abonent.Address.FlatNumber
            },
            HomePhone = homeNumber,
            WorkPhone = workNumber,
            MobilePhone = mobileNumber
        };
    }

    private static PhoneNumber? CreatePhoneNumber(string? number, PhoneType type)
    {
        if (number == null) return null;

        return new PhoneNumber
        {
            Number = number,
            Type = type
        };
    }
}