using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Dba.TestTask.Desktop.Wpf.Common;
using Dba.TestTask.Desktop.Wpf.Models;
using Dba.TestTask.Desktop.Wpf.ViewModels;

namespace Dba.TestTask.Desktop.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;
    
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = viewModel;
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        var fadeAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = new Duration(TimeSpan.FromSeconds(0.3))
        };

        var storyboard = new Storyboard();
        storyboard.Children.Add(fadeAnimation);

        Storyboard.SetTarget(fadeAnimation, this);
        Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(Window.OpacityProperty));

        storyboard.Begin();
    }

    private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if(e.ChangedButton == MouseButton.Left) DragMove();

    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}
