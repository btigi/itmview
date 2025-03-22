using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Win32;
using ItmView.Views;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace ItmView;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static bool IsAdministrator => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

    public override async void OnFrameworkInitializationCompleted()
    {
        var exit = true;
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var args = desktop.Args;

            if (Debugger.IsAttached && args?.Length == 0)
            {
                args = [@"D:\Games\IE\bg\Override\SW1H22.itm"];
            }

            if (args != null && args.Length > 0)
            {
                if (string.Compare(args[0], "register", true) == 0)
                {
                    if (IsAdministrator)
                    {
                        var appPath = System.Environment.ProcessPath;
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\.itm", "", "IEAppFile");

                        Registry.SetValue(@"HKEY_CLASSES_ROOT\IEAppFile", "", "Infinity Engine ITM file");
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\IEAppFile\DefaultIcon", "", $"{appPath},0");
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\IEAppFile\shell\open\command", "", $"\"{appPath}\" \"%1\"");
                    }
                }
                else if (File.Exists(args[0]))
                {
                    desktop.MainWindow = new MainWindow(args[0]);
                    exit = false;
                }
            }
        }

        if (exit)
        {
            Environment.Exit(0);
        }

        base.OnFrameworkInitializationCompleted();
    }
}