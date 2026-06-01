namespace MonitorWin11.Util;

public static class ByteConverter
{
    public static double BytesToMb(ulong bytes)
    {
        return bytes / 1024d / 1024d;
    }

    public static double BytesToGb(ulong bytes)
    {
        return bytes / 1024d / 1024d / 1024d;
    }

    public static double MegabytesToGb(double megabytes)
    {
        return megabytes / 1024d;
    }
}