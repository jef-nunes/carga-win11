using System.Net.NetworkInformation;

namespace MonitorWin11.Monitors;

public class NetMonitor
{
    private long _lastBytesSent;
    private long _lastBytesReceived;
    private DateTime _lastUpdate;

    public double UploadKbps { get; private set; }
    public double DownloadKbps { get; private set; }
    public int TcpConnections { get; private set; }

    private NetworkInterface _nic;

    public NetMonitor()
    {
        _nic = GetActiveInterface();
        _lastUpdate = DateTime.UtcNow;

        var stats = _nic.GetIPv4Statistics();
        _lastBytesSent = stats.BytesSent;
        _lastBytesReceived = stats.BytesReceived;
    }

    private NetworkInterface GetActiveInterface()
    {
        return NetworkInterface.GetAllNetworkInterfaces()
            .First(n =>
                n.OperationalStatus == OperationalStatus.Up &&
                n.NetworkInterfaceType != NetworkInterfaceType.Loopback
            );
    }

    public void Update()
    {
        try
        {
            var now = DateTime.UtcNow;
            var interval = (now - _lastUpdate).TotalSeconds;

            var stats = _nic.GetIPv4Statistics();

            var sentDiff = stats.BytesSent - _lastBytesSent;
            var recvDiff = stats.BytesReceived - _lastBytesReceived;

            UploadKbps = (sentDiff / 1024d) / interval;
            DownloadKbps = (recvDiff / 1024d) / interval;

            _lastBytesSent = stats.BytesSent;
            _lastBytesReceived = stats.BytesReceived;
            _lastUpdate = now;
        }
        catch
        {
            UploadKbps = 0;
            DownloadKbps = 0;

            // tenta rebind se interface mudou
            _nic = GetActiveInterface();
        }

        TcpConnections = IPGlobalProperties
            .GetIPGlobalProperties()
            .GetActiveTcpConnections()
            .Length;
    }
}