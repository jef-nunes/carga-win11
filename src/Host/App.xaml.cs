using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using MonitorWin11.Models;
using MonitorWin11.Services;

namespace MonitorWin11.Host;

public partial class App : Application
{
    private DispatcherTimer? _timer;
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();

        services.AddWpfBlazorWebView();
        services.AddSingleton<AppSettings>();
        services.AddSingleton<MonitorManager>();
        services.AddSingleton<SystemSpecs>();
        services.AddSingleton<SystemColorTheme>();
        
        Services = services.BuildServiceProvider();
        
        var appSettings = Services.GetRequiredService<AppSettings>();
        
        appSettings.LoadSettings();
        
        //var systemColorTheme = Services.GetRequiredService<SystemColorTheme>();
        
        var systemSpecs = Services.GetRequiredService<SystemSpecs>();
        systemSpecs.Fetch();
        
        var monitorManager = Services.GetRequiredService<MonitorManager>();

        _timer = new DispatcherTimer();

        _timer.Interval = TimeSpan.FromMilliseconds(appSettings.MonitorUpdateIntervalMs);

        _timer.Tick += (_, _) =>
        {
            monitorManager.Update();
        };

        _timer.Start();

        var window = new MainWindow();
        MainWindow = window;
        window.Show();

        base.OnStartup(e);
    }
    
    protected override void OnExit(ExitEventArgs e)
    {
        _timer?.Stop();

        base.OnExit(e);
    }
}