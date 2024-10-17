﻿using System;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using SukiUI.Controls;
using SukiUI.Toasts;
using SukiUI.Enums;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UotanToolbox.Common;
using SukiUI.Dialogs;

namespace UotanToolbox.Features.Home;

public partial class HomeView : UserControl
{
    public ISukiDialogManager dialogManager;
    public ISukiToastManager toastManager;
    public static string GetTranslation(string key)
    {
        return FeaturesHelper.GetTranslation(key);
    }

    public HomeView(ISukiDialogManager sukiDialogManager, ISukiToastManager sukiToastManager)
    {
        dialogManager = sukiDialogManager;
        toastManager = sukiToastManager;
        InitializeComponent();
    }

    public async void CopyButton_OnClick(object sender, RoutedEventArgs args)
    {
        if (sender is Button button)
        {
            Avalonia.Input.Platform.IClipboard clipboard = TopLevel.GetTopLevel(this)?.Clipboard;
            DataObject dataObject = new DataObject();
            if (button.Content != null)
            {
                string text = button.Content.ToString();
                if (text != null)
                {
                    dataObject.Set(DataFormats.Text, text);
                }
            }
            if (clipboard != null)
            {
                await clipboard.SetDataObjectAsync(dataObject);
            }

            toastManager.CreateSimpleInfoToast()
                .WithTitle(GetTranslation("Home_Copy"))
                .WithContent("o(*≧▽≦)ツ")
                .OfType(NotificationType.Success)
                .Dismiss().ByClicking()
                .Dismiss().After(TimeSpan.FromSeconds(3))
                .Queue();
        }
    }

    private async void OpenAFDI(object sender, RoutedEventArgs args)
    {
        if (Global.System == "Windows")
        {
            if (RuntimeInformation.OSArchitecture == Architecture.X64)
            {
                Process.Start(@"Drive\adb.exe");
            }
            else if (RuntimeInformation.OSArchitecture == Architecture.Arm64)
            {
                string drvpath = String.Format($"{Global.runpath}/Drive/adb/*.inf");
                string shell = String.Format("/add-driver {0} /subdirs /install", drvpath);
                string drvlog = await CallExternalProgram.Pnputil(shell);
                FileHelper.Write($"{Global.log_path}/drive.txt", drvlog);
                if (drvlog.Contains(GetTranslation("Basicflash_Success")))
                {
                    //SukiHost.ShowDialog(new PureDialog(GetTranslation("Common_InstallSuccess")), allowBackgroundClose: true);
                }
                else
                {
                    //SukiHost.ShowDialog(new PureDialog(GetTranslation("Common_InstallFailed")), allowBackgroundClose: true);
                }
            }
        }
        else
        {
            //SukiHost.ShowDialog(new PureDialog(GetTranslation("Basicflash_NotUsed")), allowBackgroundClose: true);
        }
    }

    private async void Open9008DI(object sender, RoutedEventArgs args)
    {
        if (Global.System == "Windows")
        {
            if (RuntimeInformation.OSArchitecture == Architecture.X64)
            {
                Process.Start(@"Drive\Qualcomm_HS-USB_Driver.exe");
            }
            else if (RuntimeInformation.OSArchitecture == Architecture.Arm64)
            {
                string drvpath = String.Format($"{Global.runpath}/drive/9008/*.inf");
                string shell = String.Format("/add-driver {0} /subdirs /install", drvpath);
                string drvlog = await CallExternalProgram.Pnputil(shell);
                FileHelper.Write($"{Global.log_path}/drive.txt", drvlog);
                if (drvlog.Contains(GetTranslation("Basicflash_Success")))
                {
                    //SukiHost.ShowDialog(new PureDialog(GetTranslation("Common_InstallSuccess")), allowBackgroundClose: true);
                }
                else
                {
                    //SukiHost.ShowDialog(new PureDialog(GetTranslation("Common_InstallFailed")), allowBackgroundClose: true);
                }
            }
        }
        else
        {
            //SukiHost.ShowDialog(new PureDialog(GetTranslation("Basicflash_NotUsed")), allowBackgroundClose: true);
        }
    }

    private async void OpenUSBP(object sender, RoutedEventArgs args)
    {
        if (Global.System == "Windows")
        {
            string cmd = @"drive\USB3.bat";
            ProcessStartInfo cmdshell = null;
            cmdshell = new ProcessStartInfo(cmd)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process f = Process.Start(cmdshell);
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                //SukiHost.ShowDialog(new PureDialog(GetTranslation("Common_Execution")), allowBackgroundClose: true);
            });
        }
        else
        {
            //SukiHost.ShowDialog(new PureDialog(GetTranslation("Basicflash_NotUsed")), allowBackgroundClose: true);
        }
    }

    private async void OpenReUSBP(object sender, RoutedEventArgs args)
    {
        if (Global.System == "Windows")
        {
            string cmd = @"drive\ReUSB3.bat";
            ProcessStartInfo cmdshell = null;
            cmdshell = new ProcessStartInfo(cmd)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process f = Process.Start(cmdshell);
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                //SukiHost.ShowDialog(new PureDialog(GetTranslation("Common_Execution")), allowBackgroundClose: true);
            });
        }
        else
        {
            //SukiHost.ShowDialog(new PureDialog(GetTranslation("Basicflash_NotUsed")), allowBackgroundClose: true);
        }
    }

    private void OpenWirelessADB(object sender, RoutedEventArgs args) => new WirelessADB(dialogManager,toastManager).Show();
}