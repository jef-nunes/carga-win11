using System.Windows.Media;

namespace MonitorWin11.Ui.Formatting;

public static class ColorManager
{
    public static string CssVariableFromUsageLevel(double percent)
    {
        if (percent >= 0 && percent < 25)
        {
            return "--color-low";
        }
        if (percent >= 25 && percent < 50)
        {
            return "--color-medium";
        }

        if (percent >= 50 && percent < 75)
        {
            return "--color-high";
        }

        if (percent >= 75)
        {
            return "--color-extreme";
        }
        return "--color-neutral";
    }
    public static string CssClassFromUsageLevel(double percent)
    {
        if (percent >= 0 && percent < 25)
        {
            return "color-low";
        }
        if (percent >= 25 && percent < 50)
        {
            return "color-medium";
        }

        if (percent >= 50 && percent < 75)
        {
            return "color-high";
        }

        if (percent >= 75)
        {
            return "color-extreme";
        }
        return "color-neutral";
    }
}