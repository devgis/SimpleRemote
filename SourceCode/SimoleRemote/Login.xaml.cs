using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleRemote
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        string url = "https://open.work.weixin.qq.com/wwopen/sso/qrConnect?appid=ww58251121245d429f&agentid=1000017&redirect_uri=http://oa.douwangkeji.com/auth/wechat/callback&state=hjGPSpqxghHYVVzR";
        public Login()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            RequestDAL.SetQRCode(qrCodeImage, url);

            RequestDAL.CheckLogined(this);
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            RequestDAL.SetQRCode(qrCodeImage, url);
        }
    }
}
