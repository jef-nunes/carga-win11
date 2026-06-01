using System.Management;
using MonitorWin11.Util;

namespace MonitorWin11.Services;

public class SystemSpecs
{
    // CPU
    public string Cpu { get; private set; } = "Desconhecido";

    // GPU
    public string Gpu { get; private set; } = "Desconhecido";

    // RAM total (GB)
    public string RamTotalGb { get; private set; } = "Desconhecido";

    // OS
    public string OsName { get; private set; } = "Desconhecido";

    // DISK total (GB)
    public string DiskTotalGb { get; private set; } = "Desconhecido";

    public void Fetch()
    {
        // CPU
        foreach (ManagementObject obj in
                 new ManagementObjectSearcher(
                     "SELECT Name FROM Win32_Processor").Get())
        {
            Cpu = obj["Name"]?.ToString() ?? "Desconhecido";
            break;
        }

        // GPU
        foreach (ManagementObject obj in
                 new ManagementObjectSearcher(
                     "SELECT Name FROM Win32_VideoController").Get())
        {
            Gpu = obj["Name"]?.ToString() ?? "Desconhecido";
            break;
        }

        // RAM total
        foreach (ManagementObject obj in
                 new ManagementObjectSearcher(
                     "SELECT TotalVisibleMemorySize FROM Win32_OperatingSystem").Get())
        {
            var value = obj["TotalVisibleMemorySize"];

            if (value != null &&
                ulong.TryParse(value.ToString(), out ulong memKb))
            {
                RamTotalGb =
                    Math.Round(memKb / 1024d / 1024d).ToString();
            }
            else
            {
                RamTotalGb = "0";
            }

            break;
        }

        // OS
        foreach (ManagementObject obj in
                 new ManagementObjectSearcher(
                     "SELECT Caption FROM Win32_OperatingSystem").Get())
        {
            OsName = obj["Caption"]?.ToString() ?? "Desconhecido";
            break;
        }

        // DISCO total
        foreach (ManagementObject obj in
                 new ManagementObjectSearcher(
                     "SELECT Size FROM Win32_LogicalDisk WHERE DeviceID='C:'").Get())
        {
            var size = obj["Size"];

            if (size != null &&
                ulong.TryParse(size.ToString(), out ulong total))
            {
                DiskTotalGb =
                    Math.Round(ByteConverter.BytesToGb(total)).ToString();
            }
            else
            {
                DiskTotalGb = "0";
            }

            break;
        }
    }
}