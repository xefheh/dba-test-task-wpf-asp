using System.Windows;
using Dba.TestTask.Desktop.Wpf.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dba.TestTask.Desktop.Wpf;

public class App : Application
{
    private readonly MainWindow _mainWindow;
    private readonly IHealthCheckerService _healthChecker;

    public App(MainWindow mainWindow, IHealthCheckerService healthChecker) => (_mainWindow, _healthChecker) = (mainWindow, healthChecker);

    protected override async void OnStartup(StartupEventArgs e)
    {
        ShutdownMode = ShutdownMode.OnMainWindowClose;
        if (!await _healthChecker.IsHealthAsync())
        {
            MessageBox.Show("Ошибка", "Ошибка соединения с сервером!");
            Shutdown();
        }
        _mainWindow.Show();
        MainWindow = _mainWindow;
        base.OnStartup(e);
    }
}