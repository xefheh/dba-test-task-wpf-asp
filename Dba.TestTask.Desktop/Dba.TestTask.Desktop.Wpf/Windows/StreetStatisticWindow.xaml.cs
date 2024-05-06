using System.Windows;
using System.Windows.Input;
using Dba.TestTask.Desktop.Wpf.Common;

namespace Dba.TestTask.Desktop.Wpf.Windows;

public partial class StreetStatisticWindow : Window
{
    public StreetStatisticWindow()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var animation = AnimationManager.CreateCloseAnimation();
        animation.Completed += (_, _) => Close();
        BeginAnimation(OpacityProperty, animation);
    }

    private void StreetStatisticWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        var animation = AnimationManager.CreateOpenAnimation();
        BeginAnimation(OpacityProperty, animation);
    }

    private void StreetStatisticWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if(e.ChangedButton == MouseButton.Left) DragMove();
    }
}