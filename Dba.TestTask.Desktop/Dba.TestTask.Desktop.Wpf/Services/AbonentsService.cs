using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Web;
using Dba.TestTask.Desktop.Wpf.Models;
using Dba.TestTask.Desktop.Wpf.Services.Interfaces;

namespace Dba.TestTask.Desktop.Wpf.Services;

public class AbonentsService : IAbonentsService
{
    private readonly HttpClient _httpClient;

    public AbonentsService(HttpClient httpClient) => _httpClient = httpClient; 
    
    public async Task<AbonentModelCollection> GetAbonentsAsync()
    {
        var abonents = await _httpClient.GetFromJsonAsync<AbonentModelCollection?>("/Abonent");
        return abonents ?? new AbonentModelCollection();
    }

    public async Task<AbonentModelCollection> GetAbonentsByPhoneAsync(string phoneNumber)
    {
        var urlPhone = HttpUtility.UrlEncode(phoneNumber);
        var abonents = await _httpClient.GetFromJsonAsync<AbonentModelCollection?>($"/Abonent?phoneNumber={urlPhone}");
        return abonents ?? new AbonentModelCollection();
    }

    public async Task<StreetStatisticModelCollection> GetStreetStatisticsAsync()
    {
        var streetStatistics = await _httpClient.GetFromJsonAsync<StreetStatisticModelCollection?>("/Abonent/StreetStatistics");
        return streetStatistics ?? new StreetStatisticModelCollection();
    }

    public async Task AddAbonentAsync(AddAbonentModel addModel) => await
        _httpClient.PostAsJsonAsync("/Abonent/Add", addModel);

    public async Task UpdateAbonentAsync(UpdateAbonentModel updateModel) => await
        _httpClient.PutAsJsonAsync("/Abonent/Update", updateModel);

    public async Task RemoveAbonentAsync(int id) => await
        _httpClient.DeleteAsync($"/Abonent/Remove/{id}");

}