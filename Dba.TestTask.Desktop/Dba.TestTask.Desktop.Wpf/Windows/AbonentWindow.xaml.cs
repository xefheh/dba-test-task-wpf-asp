using System.Windows;
using System.Windows.Input;
using Dba.TestTask.Desktop.Wpf.Common;
using Dba.TestTask.Desktop.Wpf.ViewModels;

namespace Dba.TestTask.Desktop.Wpf.Windows;

public partial class AbonentWindow : Window
{
    public AbonentWindow()
    {
        InitializeComponent();
    }

    private void AbonentWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if(e.ChangedButton == MouseButton.Left) DragMove();
    }

    private void AbonentWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        var fadeAnimation = AnimationManager.CreateOpenAnimation();
        this.BeginAnimation(Window.OpacityProperty, fadeAnimation);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var fadeAnimation = AnimationManager.CreateCloseAnimation();
        fadeAnimation.Completed += (_, _) => Close();
        this.BeginAnimation(Window.OpacityProperty, fadeAnimation);
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!IsValid())
        {
            MessageBox.Show("Не заполнены необходимые поля!\n(ВСЕ ПОЛЯ И ХОТЯ БЫ ОДИН НОМЕР ТЕЛЕФОНА)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        var fadeAnimation = AnimationManager.CreateCloseAnimation();
        fadeAnimation.Completed += (_, _) =>
        {
            DialogResult = true;
            this.Close();
        };
        BeginAnimation(Window.OpacityProperty, fadeAnimation);
    }

    private bool IsValid()
    {
        if (DataContext is not AbonentViewModel context) return false;
        return   !string.IsNullOrEmpty(context.Name) &&
                 !string.IsNullOrEmpty(context.Surname) &&
                 !string.IsNullOrEmpty(context.MiddleName) &&
                 !string.IsNullOrEmpty(context.StreetName) &&
                 !string.IsNullOrEmpty(context.HouseNumber) &&
                 !string.IsNullOrEmpty(context.FlatNumber) &&
                (!string.IsNullOrEmpty(context.HomePhone) || !string.IsNullOrEmpty(context.MobilePhone) || !string.IsNullOrEmpty(context.WorkPhone));
    }
}