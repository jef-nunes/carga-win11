using System.Management;

namespace MonitorWin11.Monitors;

public class RamMonitor : IMonitor
{
    private ulong _totalMem;

    public ulong UsedMem {get; private set;}

    public ulong FreeMem {get; private set;}

    public double UsedMemPercent {get; private set;}
  
    public RamMonitor()
    {
        // O indicador de memória total é obtida apenas uma vez
        using var searcher =
            new ManagementObjectSearcher(
                "SELECT TotalVisibleMemorySize FROM Win32_OperatingSystem"
            );

        foreach (ManagementObject os in searcher.Get())
        {
            _totalMem = Convert.ToUInt64(
                os["TotalVisibleMemorySize"]
            ) * 1024;
        }
    }

    public void Update()
    {
        // Indicador de memória livre obtido a cada atualização
        using var searcher =
            new ManagementObjectSearcher(
                "SELECT FreePhysicalMemory FROM Win32_OperatingSystem"
            );

        foreach (ManagementObject os in searcher.Get())
        {
            FreeMem = Convert.ToUInt64(
                os["FreePhysicalMemory"]
            ) * 1024;
        }

        UsedMem = _totalMem - FreeMem;
        UsedMemPercent = UsedMem * 100.0 / _totalMem;
    }
}