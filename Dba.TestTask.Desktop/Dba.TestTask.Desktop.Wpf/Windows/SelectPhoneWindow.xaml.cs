using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Dba.TestTask.Desktop.Wpf.Common;

namespace Dba.TestTask.Desktop.Wpf.Windows;

public partial class SelectPhoneWindow : Window
{
    public string? PhoneNumber { get; private set; }
    
    public SelectPhoneWindow()
    {
        InitializeComponent();
    }

    private void SelectPhoneWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left) DragMove();
    }

    private void SelectPhoneWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        var fadeAnimation = AnimationManager.CreateOpenAnimation();
        BeginAnimation(OpacityProperty, fadeAnimation);
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        var fadeAnimation = AnimationManager.CreateCloseAnimation();
        fadeAnimation.Completed += (_, _) => Close();
        BeginAnimation(OpacityProperty, fadeAnimation);
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        PhoneNumber = PhoneNumberTextBox.Text;
        var fadeAnimation = AnimationManager.CreateCloseAnimation();
        fadeAnimation.Completed += (_, _) => Close();
        BeginAnimation(OpacityProperty, fadeAnimation);
    }
}