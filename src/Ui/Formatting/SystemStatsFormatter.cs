namespace MonitorWin11.Ui.Formatting;

public static class SystemStatsFormatter
{
    public static string FormatMemory(double mb)
    {
        if (mb < 1024)
            return $"{mb:F0} MB";

        return $"{mb / 1024d:F1} GB";
    }

    public static string FormatCpu(double cpu)
    {
        return $"{cpu:F0}%";
    }
    
    public static string FormatDisk(double mb)
    {
        if (mb < 1024)
            return $"{mb:F0} MB";

        return $"{mb / 1024d:F1} GB";
    }
}