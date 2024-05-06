using System.Windows;
using System.Windows.Media.Animation;

namespace Dba.TestTask.Desktop.Wpf.Common;

public static class AnimationManager
{
    public static DoubleAnimation CreateOpenAnimation() =>
        CreateFadeUpAnimation(0, 1);

    public static DoubleAnimation CreateCloseAnimation() =>
        CreateFadeUpAnimation(1, 0);
    
    private static DoubleAnimation CreateFadeUpAnimation(int opacityStart, int opacityEnd)
    {
        var fadeAnimation = new DoubleAnimation
        {
            From = opacityStart,
            To = opacityEnd,
            Duration = new Duration(TimeSpan.FromSeconds(0.15))
        };
        return fadeAnimation;
    }
}