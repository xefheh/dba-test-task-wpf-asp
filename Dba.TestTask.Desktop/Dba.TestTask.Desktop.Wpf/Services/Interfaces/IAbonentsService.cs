using Dba.TestTask.Desktop.Wpf.Models;

namespace Dba.TestTask.Desktop.Wpf.Services.Interfaces;

public interface IAbonentsService
{
    Task<AbonentModelCollection> GetAbonentsAsync();
    Task<AbonentModelCollection> GetAbonentsByPhoneAsync(string phoneNumber);
    Task<StreetStatisticModelCollection> GetStreetStatisticsAsync();
    Task AddAbonentAsync(AddAbonentModel addModel);
    Task UpdateAbonentAsync(UpdateAbonentModel updateModel);
    Task RemoveAbonentAsync(int id);
}