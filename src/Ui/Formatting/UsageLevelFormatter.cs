namespace MonitorWin11.Ui.Formatting;

public static class UsageLevelFormatter
{
    public static string labelFromUsageLevel(double percent)
    {
        if (percent >= 0 && percent < 25)
        {
            return "Baixo";
        }
        if (percent >= 25 && percent < 50)
        {
            return "Médio";
        }

        if (percent >= 50 && percent < 75)
        {
            return "Alto";
        }

        if (percent >= 75)
        {
            return "Muito Alto";
        }
        return "";
    }
}