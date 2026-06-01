using System.Management;

namespace MonitorWin11.Monitors;

public class StorageMonitor : IMonitor
{
    private ulong _totalDisk;

    public ulong UsedDisk { get; private set; }

    public ulong FreeDisk { get; private set; }

    public double UsedDiskPercent {get; private set;}

    public StorageMonitor()
    {
        // Disco total obtido apenas uma vez (C:)
        using var searcher =
            new ManagementObjectSearcher(
                "SELECT Size FROM Win32_LogicalDisk WHERE DeviceID='C:'"
            );

        foreach (ManagementObject disk in searcher.Get())
        {
            _totalDisk = Convert.ToUInt64(disk["Size"]);
        }
    }

    public void Update()
    {
        // Espaço livre obtido a cada atualização
        using var searcher =
            new ManagementObjectSearcher(
                "SELECT FreeSpace FROM Win32_LogicalDisk WHERE DeviceID='C:'"
            );

        foreach (ManagementObject disk in searcher.Get())
        {
            FreeDisk = Convert.ToUInt64(disk["FreeSpace"]);
        }

        UsedDisk = _totalDisk - FreeDisk;
        UsedDiskPercent = UsedDisk * 100 / _totalDisk;
    }
}