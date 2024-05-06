using Dba.TestTask.Desktop.Wpf.Models;
using Dba.TestTask.Desktop.Wpf.ViewModels.Abstraction;

namespace Dba.TestTask.Desktop.Wpf.ViewModels;

public class AbonentViewModel : BaseViewModel
{
    private string _surname;
    private string _name;
    private string _middleName;
    private string _streetName;
    private string _houseNumber;
    private string _flatNumber;
    private string? _homePhone;
    private string? _workPhone;
    private string? _mobilePhone;
    
    public AbonentViewModel(AbonentModel model)
    {
        _surname = model.Surname;
        _name = model.Name;
        _middleName = model.MiddleName;
        _streetName = model.Address.StreetName;
        _houseNumber = model.Address.HouseNumber;
        _flatNumber = model.Address.FlatNumber;
        _homePhone = model.HomePhone;
        _workPhone = model.WorkPhone;
        _mobilePhone = model.MobilePhone;
        Title = "ИЗМЕНЕНИЕ ИНФОРМАЦИИ ОБ АБОНЕНТЕ";
    }
    
    public AbonentViewModel()
    {
        Title = "СОЗДАНИЕ НОВОГО АБОНЕНТА";
    }
    
    public string Title { get; set; }

    public string Surname
    {
        get => _surname;
        set
        {
            if(value != _surname) SetField(ref _surname, value);   
        }
    }
    
    public string Name
    {
        get => _name;
        set
        {
            if(value != _name) SetField(ref _name, value);   
        }
    }
    
    public string MiddleName
    {
        get => _middleName;
        set
        {
            if(value != _middleName) SetField(ref _middleName, value);   
        }
    }
    
    public string StreetName
    {
        get => _streetName;
        set
        {
            if(value != _streetName) SetField(ref _streetName, value);   
        }
    }
    
    public string HouseNumber
    {
        get => _houseNumber;
        set
        {
            if(value != _houseNumber) SetField(ref _houseNumber, value);   
        }
    }
    
    public string FlatNumber
    {
        get => _flatNumber;
        set
        {
            if(value != _flatNumber) SetField(ref _flatNumber, value);   
        }
    }
    
    public string? HomePhone
    {
        get => _homePhone;
        set
        {
            if(value != _homePhone) SetField(ref _homePhone, value);   
        }
    }
    
    public string? WorkPhone
    {
        get => _workPhone;
        set
        {
            if(value != _workPhone) SetField(ref _workPhone, value);   
        }
    }
    
    public string? MobilePhone
    {
        get => _mobilePhone;
        set
        {
            if(value != _mobilePhone) SetField(ref _mobilePhone, value);   
        }
    }
}