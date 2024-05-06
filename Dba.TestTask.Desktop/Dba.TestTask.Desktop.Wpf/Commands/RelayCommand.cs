using System.Windows.Input;

namespace Dba.TestTask.Desktop.Wpf.Commands;

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool>? _canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute == null || (parameter != null && _canExecute(parameter));

    public void Execute(object? parameter)
    {
        if (parameter != null) _execute(parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}