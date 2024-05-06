using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;
using Dba.TestTask.Desktop.Wpf.Commands;
using Dba.TestTask.Desktop.Wpf.Models;
using Dba.TestTask.Desktop.Wpf.Services.Interfaces;
using Dba.TestTask.Desktop.Wpf.ViewModels.Abstraction;
using Dba.TestTask.Desktop.Wpf.Windows;

namespace Dba.TestTask.Desktop.Wpf.ViewModels;

public class MainViewModel : BaseViewModel
{
    private ObservableCollection<AbonentModel> _abonents;
    private ICollectionView? _abonentsView;
    
    private readonly ICommand? _updateAbonentCommand;
    private readonly ICommand? _removeAbonentCommand;
    private readonly ICommand? _addAbonentCommand;
    private readonly ICommand? _getAbonentByPhoneCommand;
    private readonly ICommand? _getStatisticsCommand;
    private readonly ICommand? _exportToXmlCommand;
    
    private readonly IAbonentsService _abonentsService;
    
    public MainViewModel(IAbonentsService abonentsService)
    {
        _abonentsService = abonentsService;
        _updateAbonentCommand = new RelayCommand(UpdateAbonentAsync, CanUpdateAbonent);
        _removeAbonentCommand = new RelayCommand(RemoveAbonentAsync, CanRemoveAbonent);
        _addAbonentCommand = new RelayCommand(AddAbonentAsync);
        _getAbonentByPhoneCommand = new RelayCommand(FilterAbonentByNumber);
        _exportToXmlCommand = new RelayCommand(ExportToXml);
        _getStatisticsCommand = new RelayCommand(GetStatistics);
        _ = ReloadAllAbonents();
    }
    
    public static string Title => "Абоненты компании"; 


    public ICommand UpdateAbonentCommand => _updateAbonentCommand ?? new RelayCommand(UpdateAbonentAsync, CanUpdateAbonent);
    public ICommand RemoveAbonentCommand => _removeAbonentCommand ?? new RelayCommand(RemoveAbonentAsync, CanRemoveAbonent);
    public ICommand AddAbonentCommand => _addAbonentCommand ?? new RelayCommand(AddAbonentAsync);
    public ICommand GetStatisticsCommand => _getStatisticsCommand ?? new RelayCommand(GetStatistics);
    public ICommand ExportToXmlCommand => _exportToXmlCommand ?? new RelayCommand(ExportToXml);
    public ICommand GetAbonentByPhoneCommand => _getAbonentByPhoneCommand ?? new RelayCommand(FilterAbonentByNumber);
    
    private string? _phoneNumber = string.Empty;
    
    public ObservableCollection<AbonentModel> Abonents
    {
        get => _abonents;
        set => SetField(ref _abonents, value);
    }

    public ICollectionView? AbonentsView
    {
        get => _abonentsView;
        set => SetField(ref _abonentsView, value);
    }

    private async void UpdateAbonentAsync(object parameter)
    {
        if (parameter is not AbonentModel abonent) return;
        var abonentWindow = new AbonentWindow { DataContext = new AbonentViewModel(abonent) };
        var result = abonentWindow.ShowDialog();
        if (result == null || !result.Value) return;
        if (abonentWindow.DataContext is not AbonentViewModel dialogContext) return;
        var updateModel = new UpdateAbonentModel
        {
            Id = abonent.Id,
            Surname = dialogContext.Surname,
            Name = dialogContext.Name,
            MiddleName = dialogContext.MiddleName,
            Address = new AddressModel
            {
                StreetName = dialogContext.StreetName,
                FlatNumber = dialogContext.FlatNumber,
                HouseNumber = dialogContext.HouseNumber
            },
            HomePhone = dialogContext.HomePhone,
            WorkPhone = dialogContext.WorkPhone,
            MobilePhone = dialogContext.MobilePhone
        };
        await _abonentsService.UpdateAbonentAsync(updateModel);
        await ReloadAllAbonents();
    }

    private async void RemoveAbonentAsync(object parameter)
    {
        if (parameter is not AbonentModel abonent) return;
        await _abonentsService.RemoveAbonentAsync(abonent.Id);
        await ReloadAllAbonents();
    }

    private async void AddAbonentAsync(object parameter)
    {
        var abonentWindow = new AbonentWindow() { DataContext = new AbonentViewModel() };
        var result = abonentWindow.ShowDialog();
        if (result == null || !result.Value) return;
        if (abonentWindow.DataContext is not AbonentViewModel dialogContext) return;
        var newAbonent = new AddAbonentModel
        {
            Surname = dialogContext.Surname,
            Name = dialogContext.Name,
            MiddleName = dialogContext.MiddleName,
            Address = new AddressModel
            {
                StreetName = dialogContext.StreetName,
                FlatNumber = dialogContext.FlatNumber,
                HouseNumber = dialogContext.HouseNumber
            },
            HomePhone = dialogContext.HomePhone,
            WorkPhone = dialogContext.WorkPhone,
            MobilePhone = dialogContext.MobilePhone
        };
        await _abonentsService.AddAbonentAsync(newAbonent);
        await ReloadAllAbonents();
    }

    private bool CanUpdateAbonent(object parameter) => parameter is AbonentModel;
    
    private bool CanRemoveAbonent(object parameter) => _abonents.Any() && parameter is AbonentModel;

    private async void FilterAbonentByNumber(object parameter)
    {
        var selectPhoneWindow = new SelectPhoneWindow();
        var result = selectPhoneWindow.ShowDialog();
        if (result == null || !result.Value) return;
        _phoneNumber = selectPhoneWindow.PhoneNumber;
        await ReloadAllAbonents();
    }

    private async Task ReloadAllAbonents(object? parameter = default)
    {
        var abonents = string.IsNullOrEmpty(_phoneNumber)
            ? await _abonentsService.GetAbonentsAsync()
            : await _abonentsService.GetAbonentsByPhoneAsync(_phoneNumber);
        var filter = AbonentsView?.Filter;
        Abonents = new ObservableCollection<AbonentModel>(abonents.Abonents);
        AbonentsView = CollectionViewSource.GetDefaultView(Abonents);
        if (filter != null) AbonentsView.Filter = filter;
    }

    private async void GetStatistics(object parameter)
    {
        var streetStatistics = await _abonentsService.GetStreetStatisticsAsync();
        
        var viewModel = new StreetStatisticViewModel(streetStatistics.Statistics);

        var window = new StreetStatisticWindow { DataContext = viewModel };
        window.ShowDialog();
    }

    private void ExportToXml(object parameter)
    {
        var serializableCollection = new ObservableCollection<AbonentModel>(AbonentsView.Cast<AbonentModel>());
        var serializer = new XmlSerializer(typeof(ObservableCollection<AbonentModel>));
        Directory.CreateDirectory("Reports");
        using var writer = new StreamWriter($"Reports/report_{DateTime.Now:dd_MM_yyyy_HH_mm}.xml");
        serializer.Serialize(writer, serializableCollection);
    }
}