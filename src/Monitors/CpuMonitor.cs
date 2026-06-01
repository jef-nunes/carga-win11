using System.Diagnostics;

namespace MonitorWin11.Monitors;

public class CpuMonitor : IMonitor
{
    private readonly PerformanceCounter _performanceCounter;

    public float Load { get; private set; }

    public CpuMonitor()
    {
        _performanceCounter =
            new PerformanceCounter(
                "Processor",
                "% Processor Time",
                "_Total"
            );
    }

    public void Update()
    {
        Load = _performanceCounter.NextValue();
    }
}