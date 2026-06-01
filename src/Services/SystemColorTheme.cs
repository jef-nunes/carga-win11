using Microsoft.Win32;

namespace MonitorWin11.Services;

public class SystemColorTheme
{
    public bool IsDarkMode()
    {
        const string key =
            @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        using RegistryKey? subKey =
            Registry.CurrentUser.OpenSubKey(key);

        object? value =
            subKey?.GetValue("AppsUseLightTheme");

        return value is int theme &&
               theme == 0;
    }
}