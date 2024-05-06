using Dba.TestTask.Desktop.Wpf.Services;
using Dba.TestTask.Desktop.Wpf.Services.Interfaces;
using Dba.TestTask.Desktop.Wpf.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dba.TestTask.Desktop.Wpf;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();
                services.AddHttpClient<IAbonentsService, AbonentsService>((serviceProvider, client) =>
                {
                    var uri = new Uri(config.GetConnectionString("BackendUrl")!);
                    client.BaseAddress = uri;
                });
                services.AddHttpClient<IHealthCheckerService, HealthCheckerService>((serviceProvider, client) =>
                {
                    var uri = new Uri(config.GetConnectionString("BackendUrl")!);
                    client.BaseAddress = uri;
                });
            })
            .Build();
        
        var app = host.Services.GetService<App>();
        app?.Run();
    }
}