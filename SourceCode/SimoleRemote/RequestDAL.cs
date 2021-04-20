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

                List<DbRemoteTree> Items = new List<DbRemoteTree>();
                Items.Add(new DbRemoteTree("001", "cerdp", RemoteType.rdp) );
                Items.Add(new DbRemoteTree("002", "cessh", RemoteType.ssh));
                Items.Add(new DbRemoteTree("003", "cetelnet", RemoteType.telnet));
                foreach (var item in Items)
                {
                    RemoteTreeViewItem treeItem = new RemoteTreeViewItem(item);
                    partRemoteTree.Items.Add(treeItem);
                }
        
                //MessageBox.Show( responseBody);
            }
            catch (Exception e)
            {
                
                MessageBox.Show($"获取远程数据出错!(Error:{e.Message}");
            }
        }

    }
}
