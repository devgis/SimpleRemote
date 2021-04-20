using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using LiteDB;
using Newtonsoft.Json;
using SimpleRemote.Modes;

namespace SimpleRemote
{
    public class RequestDAL
    {
        private const string URL = "http://ceshi.vaiwan.com/api/get-servers/";
        static readonly HttpClient client = new HttpClient();

        public static async void GetData(string url= URL)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                MessageBox.Show( responseBody);
            }
            catch (Exception e)
            {
                
                MessageBox.Show($"获取远程数据出错!(Error:{e.Message}");
            }
        }

    }
}
