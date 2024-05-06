using Dba.TestTask.Backend.Domain.Entities;

namespace Dba.TestTask.Backend.Application.Persistence.Interfaces;

public interface IAbonentRepository
{
    Task<Abonent?> GetAbonentAsync(int id);
    Task<IEnumerable<Abonent>> GetAbonentsAsync();
    Task<IEnumerable<Abonent>> GetAbonentsByPhoneNumber(string number);
    Task<int> AddAbonentAsync(Abonent abonent);
    Task UpdateAbonentAsync(Abonent abonent);
    Task RemoveAbonentAsync(int id);
}