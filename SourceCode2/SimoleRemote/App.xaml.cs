using SimpleRemote.Core;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Runtime.InteropServices;
using SimpleRemote.Core;
using SimpleRemote.Modes;

namespace SimpleRemote
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; ;
            this.Startup += App_Startup;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            MessageBox.Show("AppDomain发生未知异常:" + ex.Message + "\r\nStackTrace:" + ex.StackTrace, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            Current.Shutdown();
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("系统发生未知异常:" + e.Exception.Message + "\r\nStackTrace:" + e.Exception.StackTrace, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            Current.Shutdown();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args == null || e.Args.Length < 4)
            {
                MessageBox.Show("参数不匹配，参数为: 协议 服务器 用户名 密码(示例: SimpleRemote rdp 127.0.0.1:3390 administrator 123456)!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            else
            {
                string protype = e.Args[0];
                string servername = e.Args[1];
                string username = e.Args[2];
                string password = e.Args[3];
                string privatekey = null;
                if (e.Args.Length >= 5)
                {
                    privatekey = e.Args[4];
                }

                DbItemRemoteLink link = new DbItemRemoteLink();
                link.Name = servername;
                link.Id = link.Name.Replace(".", "_").Replace(":", "_");
                link.Server = servername;
                link.Password = password;
                link.UserName = username;
                link.Description = "";
                link.ExternalIsMaximize = true;
                link.ExternalWindowHeight = 600;
                link.ExternalWindowWidth = 800;
                if (!string.IsNullOrEmpty(privatekey))
                {
                    link.PrivateKey = privatekey;
                }

                switch (protype.ToLower())
                {
                    case "ssh":
                        link.Type = 2; // RemoteType.ssh;
                        link.ItemSetting = new DbItemSettingSsh();
                        break;
                    case "rdp":
                        link.Type = 1; // RemoteType.rdp;
                        link.ItemSetting = new DbItemSettingRdp();
                        break;
                    case "telenet":
                    case "telnet":
                        link.Type = 3; // RemoteType.telnet;
                        link.ItemSetting = new DbItemSettingTelnet();
                        break;
                    default:
                        link.Type = 2;// RemoteType.ssh;
                        link.ItemSetting = new DbItemSettingSsh();
                        break;
                }

                
                //main.Show();
                Current.Dispatcher.Invoke(() =>
                {
                    MainWindow mainform = new MainWindow();
                    RemoteItems.Open(link, DbItemSetting.OPEN_WINDOW);
                });
            }

        }
    }
}
