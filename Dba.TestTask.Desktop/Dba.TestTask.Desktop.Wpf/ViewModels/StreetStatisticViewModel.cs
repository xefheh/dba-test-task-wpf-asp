using System.Collections.ObjectModel;
using Dba.TestTask.Desktop.Wpf.Models;
using Dba.TestTask.Desktop.Wpf.ViewModels.Abstraction;

namespace Dba.TestTask.Desktop.Wpf.ViewModels;

public class StreetStatisticViewModel : BaseViewModel
{
    private ObservableCollection<StreetStatisticModel> _statistics;

    public StreetStatisticViewModel(IEnumerable<StreetStatisticModel> statistics) =>
        Statistics = new ObservableCollection<StreetStatisticModel>(statistics);
        
    public ObservableCollection<StreetStatisticModel> Statistics
    {
        get => _statistics;
        set => SetField(ref _statistics, value);
    }
}