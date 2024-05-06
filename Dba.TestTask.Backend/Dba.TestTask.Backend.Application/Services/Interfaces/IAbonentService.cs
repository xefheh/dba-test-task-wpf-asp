using Dba.TestTask.Backend.Application.Requests.Abonents;
using Dba.TestTask.Backend.Application.ViewModels.Abonents;
using Dba.TestTask.Backend.Application.ViewModels.StreetStatistics;

namespace Dba.TestTask.Backend.Application.Services.Interfaces;

public interface IAbonentService
{
    Task<int> AddAbonentAsync(AddAbonentRequest request);
    Task<AbonentViewModel> GetAbonentAsync(int id);
    Task<AbonentViewModelCollection> GetAbonentsAsync();
    Task<AbonentViewModelCollection> GetAbonentsByPhoneAsync(string phone);
    Task<StreetStatisticViewModelCollection> GetStreetStatisticsAsync();
    Task RemoveAbonentAsync(int id);
    Task UpdateAbonentAsync(UpdateAbonentRequest request);
}