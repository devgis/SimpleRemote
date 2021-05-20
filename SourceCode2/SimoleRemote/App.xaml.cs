using SimpleRemote.Core;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Runtime.InteropServices;
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
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args == null || e.Args.Length < 4)
            {
                MessageBox.Show("参数不匹配，参数为: 协议 服务器 用户名 密码!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            else
            {
                string protype = e.Args[0];
                string servername = e.Args[1];
                string username = e.Args[2];
                string password = e.Args[3];

                //RemoteItems.Open(selectItem, DbItemSetting.OPEN_WINDOW);

                DbItemRemoteLink link = new DbItemRemoteLink();
                link.Name = servername;
                link.Server = servername;
                link.Password = password;
                link.UserName = username;
                link.Description = "";
                link.ExternalIsMaximize = true;
                link.ExternalWindowHeight = 600;
                link.ExternalWindowWidth = 800;
                
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
                Application.Current.Dispatcher.Invoke(() =>
                {
                    RemoteItems.Open(link, DbItemSetting.OPEN_WINDOW);
                });
            }
            
        }
    }
}
