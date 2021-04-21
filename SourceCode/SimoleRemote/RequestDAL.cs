using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LiteDB;
using Newtonsoft.Json;
using SimpleRemote.Controls;
using SimpleRemote.Modes;

namespace SimpleRemote
{
    public class RequestDAL
    {
        private const string URL = "http://ceshi.vaiwan.com/api/get-servers/";
        static readonly HttpClient client = new HttpClient();
        private static Dictionary<string, DbItemRemoteLink> DbItemRemoteLinkDIC = new Dictionary<string, DbItemRemoteLink>();

        public static async void GetData(TreeView partRemoteTree,string url= URL)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
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
