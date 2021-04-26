﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LiteDB;
using Newtonsoft.Json;
using SimpleRemote.Controls;
using SimpleRemote.Modes;

namespace SimpleRemote
{
    public class RequestDAL
    {
        private const string URL = "http://ceshi.vaiwan.com/api/get-servers/";
        private const string CheckLoginedURL = "http://oa.douwangkeji.com/auth/wechat";

        static CookieContainer cookieContainer = new CookieContainer();
        static HttpMessageHandler handler = new HttpClientHandler() { CookieContainer= cookieContainer, UseCookies = true };
        static readonly HttpClient client = new HttpClient(handler);
        private static Dictionary<string, DbItemRemoteLink> DbItemRemoteLinkDIC = new Dictionary<string, DbItemRemoteLink>();
        static CookieCollection cookies { get; set; }

        static RequestDAL()
        {
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:57.0) Gecko/20100101 Firefox/57.0");
            client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            client.DefaultRequestHeaders.Add("Keep-Alive", "timeout=600");
        }
        public static async void GetData(TreeView partRemoteTree,string url= URL)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                // Add Cookie
                try
                {
                    if (cookies != null && cookies.Count > 0)
                    {
                        cookieContainer.Add(cookies);
                    }
                }
                catch
                { }

                partRemoteTree.Items.Clear();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                var rs = JsonConvert.DeserializeObject<RS>(responseBody);
                if (rs != null && rs.code == 0&&rs.data.Count>0){
                    //访问远程数据成功！
                    List<DbRemoteTree> Items = new List<DbRemoteTree>();
                    foreach (var item in rs.data)
                    {
                        var treeitem = new DbRemoteTree(item.id.ToString(), $"{item.server_name}-{item.remark}-{item.remarks}", RemoteType.rdp,item.online_status);
                        switch (item.protocol)
                        {
                            case "ssh":
                                treeitem.Type = RemoteType.ssh;
                                break;
                            case "rdp":
                                treeitem.Type = RemoteType.rdp;
                                break;
                            case "telenet":
                            case "telnet":
                                treeitem.Type = RemoteType.telnet;
                                break;
                            default:
                                treeitem.Type = RemoteType.ssh;
                                break;
                        }
                        Items.Add(treeitem);

                        DbItemRemoteLink link = new DbItemRemoteLink();
                        link.Name = item.server_name;
                        link.Password = item.password;
                        link.UserName = item.name;
                        link.Description = item.remark;
                        link.ExternalIsMaximize =true;
                        link.ExternalWindowHeight = 600;
                        link.ExternalWindowWidth = 800;
                        link.Id = item.id.ToString();
                        link.Type = (int)treeitem.Type;
                        link.Server = item.hosts;
                        link.PrivateKey = null;
                        link.IsExpander1 = true;
                        link.IsExpander2 = true;

                        if (treeitem.Type == RemoteType.rdp)
                        {
                            link.ItemSetting = new DbItemSettingRdp();
                        }
                        else if (treeitem.Type == RemoteType.ssh)
                        {
                            link.ItemSetting = new DbItemSettingSsh();
                        }
                        else if (treeitem.Type == RemoteType.telnet)
                        {
                            link.ItemSetting = new DbItemSettingTelnet();
                        }

                        if (!DbItemRemoteLinkDIC.ContainsKey(item.id.ToString()))
                        {
                            DbItemRemoteLinkDIC.Add(item.id.ToString(), link);
                        }

                    }
                   
                    //Items.Add(new DbRemoteTree("001", "cerdp", RemoteType.rdp));
                    //Items.Add(new DbRemoteTree("002", "cessh", RemoteType.ssh));
                    //Items.Add(new DbRemoteTree("003", "cetelnet", RemoteType.telnet));
                    foreach (var item in Items)
                    {
                        RemoteTreeViewItem treeItem = new RemoteTreeViewItem(item);
                        partRemoteTree.Items.Add(treeItem);
                    }
                }
                else{
                    MessageBox.Show(responseBody);
                    throw new Exception($"获取远程配置(服务Url:{url})数据出错,！");
                }
            }
            catch (Exception e)
            {
                
                MessageBox.Show($"获取远程数据出错!(Error:{e.Message}");
            }
        }

        public static async void CheckLogined(Login loginForm,string loginurl= CheckLoginedURL)
        {
            bool blogined = false;
            ThreadPool.QueueUserWorkItem(async (o) => {
                while (!blogined)
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(loginurl);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        //检测是否登录成功 
                        if (response.StatusCode == System.Net.HttpStatusCode.Found
                        && !string.IsNullOrEmpty(responseBody)
                        && responseBody.Contains("退出"))
                        {
                            blogined = true;
                            loginForm.Dispatcher.Invoke(() =>
                            {
                                loginForm.Hide();
                            });

                            cookies = cookieContainer.GetCookies(new Uri(CheckLoginedURL));
                            //FileStream fileStream = new FileStream("SimoleRemoteSavedCookie.dat", System.IO.FileMode.Create);
                            //BinaryFormatter b = new BinaryFormatter();
                            //b.Serialize(fileStream, cookies);
                            //fileStream.Close();

                            //打开主窗体
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                MainWindow main = new MainWindow();
                                main.Show();
                            });
                        }

                        //登录成功后进入主窗体;

                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show($"获取远程登录信息出错!(Error:{e.Message}");
                    }
                }
            },null);
            Thread.Sleep(1000);
            
        }

        public static async void SetQRCode(Image image, string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(responseBody)
                    && responseBody.Contains("微信登录")
                    && responseBody.Contains("qrcode lightBorder"))
                {
                    string qrurl ="http:"+GetWebImageURL(responseBody);
                    image.Dispatcher.Invoke(() => {
                        image.Source = new BitmapImage(new Uri(qrurl, UriKind.Absolute));
                        image.UpdateLayout();
                    });
                    
                }
                else
                {
                    MessageBox.Show("获取图片失败！");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"获取登录二维码出错!(Error:{e.Message}");
            }

        }

        private static string GetWebImageURL(string html)
        {
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            
            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(html);

            // 取得匹配项列表   
            foreach (Match match in matches)
            {
                if (match.Value.Contains("qrcode lightBorder"))
                {
                    return match.Groups["imgUrl"].Value;
                }
            }
            return "";
        }

        public static DbItemRemoteLink GetItemRemoteLink(string uuid)
        {
            if (!string.IsNullOrEmpty(uuid) && DbItemRemoteLinkDIC.ContainsKey(uuid))
            {
                return DbItemRemoteLinkDIC[uuid];
            }
            else
            {
                return null;
            }
        }

    }

    public class RS
    {
        public int code
        {
            get;
            set;
        }

        public string message
        {
            get;
            set;
        }

        public List<RSData> data
        {
            get;
            set;
        }
    }

    public class RSData
    {
        //"online_status": "\u6389\u7ebf",
        public string online_status
        {
            get;
            set;
        }
        //"id": 11,
        public int id
        {
            get;
            set;
        }
        //"server_name": "kaishishu1",
        public string server_name
        {
            get;
            set;
        }
        //"product_type": "N\u6d59\u6c5f\u821f\u5c71\u7535\u4fe1\u4e00\u578b",
        public string product_type
        {
            get;
            set;
        }
        //"hosts": "zjzhoushan.webok.net:20145",
        public string hosts
        {
            get;
            set;
        }
        //"name": "administrator",
        public string name
        {
            get;
            set;
        }
        //"password": "123456",
        public string password
        {
            get;
            set;
        }
        //"parameters": {
        //	"product_type": "N\u6d59\u6c5f\u821f\u5c71\u7535\u4fe1\u4e00\u578b",
        //	"service_name": "kaishishu1",
        //	"remote_ip": "zjzhoushan.webok.net:20145",
        //	"service_password": "123123",
        //	"create_time": "2020\u5e746\u670820\u65e5",
        //	"expire_date": "2021-04-2623:58:52",
        //	"status": "\u6b63\u5e38",
        //	"system": "Windows7(32\u4f4d)",
        //	"remark": "\u963f\u6cd5\u72d7\u5f00\u59cb\u6570",
        //	"id": "211820",
        //	"login_url": "vpsadm2.asp?id=211820&go=a"
        //},
        public Dictionary<string,string> parameters
        {
            get;
            set;
        }
        //"remark": "\u963f\u6cd5\u72d7\u5f00\u59cb\u6570",
        public string remark
        {
            get;
            set;
        }
        //"remarks": "\u672a\u8bbe\u7f6e\u5907\u6ce8",
        public string remarks
        {
            get;
            set;
        }
        //"status": "\u6b63\u5e38",
        public string status
        {
            get;
            set;
        }
        //"opening_time": "2020\u5e746\u670820\u65e5",
        public string opening_time
        {
            get;
            set;
        }
        //"expire_date": "2021-04-2623:58:52",
        public string expire_date
        {
            get;
            set;
        }
        //"create_time": "2021-04-15T07:54:45.320Z",
        public string create_time
        {
            get;
            set;
        }
        //"update_time": "2021-04-21T01:17:38.197Z",
        public string update_time
        {
            get;
            set;
        }
        //"server_type": "Windows7(32\u4f4d)"
        public string server_type
        {
            get;
            set;
        }
        public string protocol
        {
            get;
            set;
        }
        
    }
}
