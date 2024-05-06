using System.Net.Http;
using Dba.TestTask.Desktop.Wpf.Services.Interfaces;

namespace Dba.TestTask.Desktop.Wpf.Services;

public class HealthCheckerService : IHealthCheckerService
{
    private readonly HttpClient _client;

    public HealthCheckerService(HttpClient client) => _client = client;
    
    public async Task<bool> IsHealthAsync()
    {
        try
        {
            var response = await _client.GetAsync("/healthcheck");
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }
}