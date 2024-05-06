namespace Dba.TestTask.Desktop.Wpf.Services.Interfaces;

public interface IHealthCheckerService
{
    public Task<bool> IsHealthAsync();
}