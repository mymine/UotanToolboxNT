﻿using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using SukiUI.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UotanToolbox.Common;
using UotanToolbox.Features.Components;

namespace UotanToolbox.Features.Customizedflash;

public partial class CustomizedflashView : UserControl
{
    private static string GetTranslation(string key) => FeaturesHelper.GetTranslation(key);
    public CustomizedflashView()
    {
        InitializeComponent();
    }

    public async Task Fastboot(string fbshell)//Fastboot实时输出
    {
        await Task.Run(() =>
        {
            string cmd = Path.Combine(Global.bin_path, "platform-tools", "fastboot");
            ProcessStartInfo fastboot = new ProcessStartInfo(cmd, fbshell)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            using Process fb = new Process();
            fb.StartInfo = fastboot;
            fb.Start();
            fb.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            fb.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            fb.BeginOutputReadLine();
            fb.BeginErrorReadLine();
            fb.WaitForExit();
            fb.Close();
        });
    }

    private async void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
    {
        if (!String.IsNullOrEmpty(outLine.Data))
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                StringBuilder sb = new StringBuilder(CustomizedflashLog.Text);
                CustomizedflashLog.Text = sb.AppendLine(outLine.Data).ToString();
                CustomizedflashLog.ScrollToLine(StringHelper.TextBoxLine(CustomizedflashLog.Text));
            });
        }
    }

    private async void OpenSystemFile(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false
        });
        if (files.Count >= 1)
        {
            SystemFile.Text = StringHelper.FilePath(files[0].Path.ToString());
        }
    }

    private async void FlashSystemFile(object sender, RoutedEventArgs args)
    {
        if (await GetDevicesInfo.SetDevicesInfoLittle())
        {
            if (SystemFile.Text != null)
            {
                MainViewModel sukiViewModel = GlobalData.MainViewModelInstance;
                if (sukiViewModel.Status == GetTranslation("Home_Fastboot") || sukiViewModel.Status == GetTranslation("Home_Fastbootd"))
                {
                    BusyFlash.IsBusy = true;
                    CustomizedflashLog.Text = "";
                    string shell = String.Format($"-s {Global.thisdevice} flash system \"{SystemFile.Text}\"");
                    await Fastboot(shell);
                    BusyFlash.IsBusy = false;
                }
                else
                {
                    SukiHost.ShowDialog(new ConnectionDialog("请将设备进入Fastboot模式后执行！"));
                }
            }
        }
        else
        {
            SukiHost.ShowDialog(new ConnectionDialog("请选择文件！"));
        }
    }

    private async void OpenProductFile(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false
        });
        if (files.Count >= 1)
        {
            ProductFile.Text = StringHelper.FilePath(files[0].Path.ToString());
        }
    }
    private async void FlashProductFile(object sender, RoutedEventArgs args)
    {
        if (await GetDevicesInfo.SetDevicesInfoLittle())
        {
            if (ProductFile.Text != null)
            {
                MainViewModel sukiViewModel = GlobalData.MainViewModelInstance;
                if (sukiViewModel.Status == GetTranslation("Home_Fastboot") || sukiViewModel.Status == GetTranslation("Home_Fastbootd"))
                {
                    BusyFlash.IsBusy = true;
                    CustomizedflashLog.Text = "";
                    string shell = String.Format($"-s {Global.thisdevice} flash product \"{ProductFile.Text}\"");
                    await Fastboot(shell);
                    BusyFlash.IsBusy = false;
                }
                else
                {
                    SukiHost.ShowDialog(new ConnectionDialog("请将设备进入Fastboot模式后执行！"));
                }
            }
            else
            {
                SukiHost.ShowDialog(new ConnectionDialog("请选择文件！"));
            }
        }
    }
    private async void OpenVenderFile(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false
        });
        if (files.Count >= 1)
        {
            VenderFile.Text = StringHelper.FilePath(files[0].Path.ToString());
        }
    }
    private async void FlashVenderFile(object sender, RoutedEventArgs args)
    {
        if (await GetDevicesInfo.SetDevicesInfoLittle())
        {
            if (VenderFile.Text != null)
            {
                MainViewModel sukiViewModel = GlobalData.MainViewModelInstance;
                if (sukiViewModel.Status == GetTranslation("Home_Fastboot") || sukiViewModel.Status == GetTranslation("Home_Fastbootd"))
                {
                    BusyFlash.IsBusy = true;
                    CustomizedflashLog.Text = "";
                    string shell = String.Format($"-s {Global.thisdevice} flash vendor \"{VenderFile.Text}\"");
                    await Fastboot(shell);
                    BusyFlash.IsBusy = false;
                }
                else
                {
                    SukiHost.ShowDialog(new ConnectionDialog("请将设备进入Fastboot模式后执行！"));
                }
            }
            else
            {
                SukiHost.ShowDialog(new ConnectionDialog("请选择文件！"));
            }
        }
    }
    private async void OpenBootFile(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false
        });
        if (files.Count >= 1)
        {
            BootFile.Text = StringHelper.FilePath(files[0].Path.ToString());
        }
    }
    private async void FlashBootFile(object sender, RoutedEventArgs args)
    {
        if (await GetDevicesInfo.SetDevicesInfoLittle())
        {
            if (BootFile.Text != null)
            {
                MainViewModel sukiViewModel = GlobalData.MainViewModelInstance;
                if (sukiViewModel.Status == GetTranslation("Home_Fastboot") || sukiViewModel.Status == GetTranslation("Home_Fastbootd"))
                {
                    BusyFlash.IsBusy = true;
                    CustomizedflashLog.Text = "";
                    string shell = String.Format($"-s {Global.thisdevice} flash boot \"{BootFile.Text}\"");
                    await Fastboot(shell);
                    BusyFlash.IsBusy = false;
                }
                else
                {
                    SukiHost.ShowDialog(new ConnectionDialog("请将设备进入Fastboot模式后执行！"));
                }
            }
            else
            {
                SukiHost.ShowDialog(new ConnectionDialog("请选择文件！"));
            }
        }
    }
    private async void OpenSystemextFile(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false
        });
        if (files.Count >= 1)
        {
            SystemextFile.Text = StringHelper.FilePath(files[0].Path.ToString());
        }
    }
    private async void FlashSystemextFile(object sender, RoutedEventArgs args)
    {
        if (await GetDevicesInfo.SetDevicesInfoLittle())
        {
            if (SystemextFile.Text != null)
            {
                MainViewModel sukiViewModel = GlobalData.MainViewModelInstance;
                if (sukiViewModel.Status == GetTranslation("Home_Fastboot") || sukiViewModel.Status == GetTranslation("Home_Fastbootd"))
                {
                    BusyFlash.IsBusy = true;
                    CustomizedflashLog.Text = "";
                    string shell = String.Format($"-s {Global.thisdevice} flash system_ext \"{SystemextFile.Text}\"");
                    await Fastboot(shell);
                    BusyFlash.IsBusy = false;
                }
                else
                {
                    SukiHost.ShowDialog(new ConnectionDialog("请将设备进入Fastboot模式后执行！"));
                }
            }
            else
            {
                SukiHost.ShowDialog(new ConnectionDialog("请选择文件！"));
            }
        }
    }
    private async void OpenOdmFile(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false
        });
        if (files.Count >= 1)
        {
            OdmFile.Text = StringHelper.FilePath(files[0].Path.ToString());
        }
    }
    private async void FlashOdmFile(object sender, RoutedEventArgs args)
    {
        if (await GetDevicesInfo.SetDevicesInfoLittle())
        {
            if (OdmFile.Text != null)
            {
                MainViewModel sukiViewModel = GlobalData.MainViewModelInstance;
                if (sukiViewModel.Status == GetTranslation("Home_Fastboot") || sukiViewModel.Status == GetTranslation("Home_Fastbootd"))
                {
                    BusyFlash.IsBusy = true;
                    CustomizedflashLog.Text = "";
                    string shell = String.Format($"-s {Global.thisdevice} flash odm \"{OdmFile.Text}\"");
                    await Fastboot(shell);
                    BusyFlash.IsBusy = false;
                }
                else
                {
                    SukiHost.ShowDialog(new ConnectionDialog("请将设备进入Fastboot模式后执行！"));
                }
            }
            else
            {
                SukiHost.ShowDialog(new ConnectionDialog("请选择文件！"));
            }
        }
    }
    private async void OpenVenderbootFile(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false
        });
        if (files.Count >= 1)
        {
            VenderbootFile.Text = StringHelper.FilePath(files[0].Path.ToString());
        }
    }
    private async void FlashVenderbootFile(object sender, RoutedEventArgs args)
    {
        if (await GetDevicesInfo.SetDevicesInfoLittle())
        {
            if (VenderbootFile.Text != null)
            {
                MainViewModel sukiViewModel = GlobalData.MainViewModelInstance;
                if (sukiViewModel.Status == GetTranslation("Home_Fastboot") || sukiViewModel.Status == GetTranslation("Home_Fastbootd"))
                {
                    BusyFlash.IsBusy = true;
                    CustomizedflashLog.Text = "";
                    string shell = String.Format($"-s {Global.thisdevice} flash vendor_boot \"{VenderbootFile.Text}\"");
                    await Fastboot(shell);
                    BusyFlash.IsBusy = false;
                }
                else
                {
                    SukiHost.ShowDialog(new ConnectionDialog("请将设备进入Fastboot模式后执行！"));
                }
            }
            else
            {
                SukiHost.ShowDialog(new ConnectionDialog("请选择文件！"));
            }
        }
    }
    private async void OpenInitbootFile(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false
        });
        if (files.Count >= 1)
        {
            InitbootFile.Text = StringHelper.FilePath(files[0].Path.ToString());
        }
    }
    private async void FlashInitbootFile(object sender, RoutedEventArgs args)
    {
        if (await GetDevicesInfo.SetDevicesInfoLittle())
        {
            if (InitbootFile.Text != null)
            {
                MainViewModel sukiViewModel = GlobalData.MainViewModelInstance;
                if (sukiViewModel.Status == GetTranslation("Home_Fastboot") || sukiViewModel.Status == GetTranslation("Home_Fastbootd"))
                {
                    BusyFlash.IsBusy = true;
                    CustomizedflashLog.Text = "";
                    string shell = String.Format($"-s {Global.thisdevice} flash init_boot \"{InitbootFile.Text}\"");
                    await Fastboot(shell);
                    BusyFlash.IsBusy = false;
                }
                else
                {
                    SukiHost.ShowDialog(new ConnectionDialog("请将设备进入Fastboot模式后执行！"));
                }
            }
            else
            {
                SukiHost.ShowDialog(new ConnectionDialog("请选择文件！"));
            }
        }
    }
    private async void OpenImageFile(object sender, RoutedEventArgs args)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false
        });
        if (files.Count >= 1)
        {
            ImageFile.Text = StringHelper.FilePath(files[0].Path.ToString());
        }
    }
    private async void FlashImageFile(object sender, RoutedEventArgs args)
    {
        if (await GetDevicesInfo.SetDevicesInfoLittle())
        {
            if (ImageFile.Text != null)
            {
                MainViewModel sukiViewModel = GlobalData.MainViewModelInstance;
                if (sukiViewModel.Status == GetTranslation("Home_Fastboot") || sukiViewModel.Status == GetTranslation("Home_Fastbootd"))
                {
                    BusyImageFlash.IsBusy = true;
                    CustomizedflashLog.Text = "";
                    string shell = String.Format($"-s {Global.thisdevice} flash {Part.Text} \"{ImageFile.Text}\"");
                    await Fastboot(shell);
                    BusyImageFlash.IsBusy = false;
                }
                else
                {
                    SukiHost.ShowDialog(new ConnectionDialog("请将设备进入Fastboot模式后执行！"));
                }
            }
            else
            {
                SukiHost.ShowDialog(new ConnectionDialog("请选择文件！"));
            }
        }
    }
    private async void DisableVbmeta(object sender, RoutedEventArgs args)
    {
        if (await GetDevicesInfo.SetDevicesInfoLittle())
        {
            MainViewModel sukiViewModel = GlobalData.MainViewModelInstance;
            if (sukiViewModel.Status == GetTranslation("Home_Fastboot") || sukiViewModel.Status == GetTranslation("Home_Fastbootd"))
            {
                CustomizedflashLog.Text = "";
                string shell = String.Format($"-s {Global.thisdevice} --disable-verity --disable-verification flash vbmeta {Global.runpath}/Image/vbmeta.img");
                await Fastboot(shell);
            }
            else
            {
                SukiHost.ShowDialog(new ConnectionDialog("请将设备进入Fastboot模式后执行！"));
            }
        }
    }
    private async void SetOther(object sender, RoutedEventArgs args)
    {
        if (await GetDevicesInfo.SetDevicesInfoLittle())
        {
            MainViewModel sukiViewModel = GlobalData.MainViewModelInstance;
            if (sukiViewModel.Status == GetTranslation("Home_Fastboot") || sukiViewModel.Status == GetTranslation("Home_Fastbootd"))
            {
                CustomizedflashLog.Text = "";
                string shell = String.Format($"-s {Global.thisdevice} set_active other");
                await Fastboot(shell);
            }
            else
            {
                SukiHost.ShowDialog(new ConnectionDialog("请将设备进入Fastboot模式后执行！"));
            }
        }
    }
}