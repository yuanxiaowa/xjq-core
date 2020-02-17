using Newtonsoft.Json;
using OdyLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.entities
{
    class Tools
    {
        public static string McCode;
        public static int LEN = 18;
        static Tools()
        {
            McCode = new MyDm().GetMachineCode();
        }
        public static bool validate(string code)
        {
            code = new SymmetryEncrypt().Decrypto(code);
            var t = long.Parse(code.Substring(0, LEN));
            if (DateTime.Now > new DateTime(t))
            {
                return false;
            }
            code = code.Substring(LEN);
            return code == McCode;
        }
        private async static Task<DateTime> GetNow()
        {
            var req = WebRequest.Create("http://api.m.taobao.com/rest/api3.do?api=mtop.common.getTimestamp");
            var res = await req.GetResponseAsync();
            using (var sr = new StreamReader(res.GetResponseStream()))
            {
                var content = sr.ReadToEnd();
                var mc = Regex.Match(content, "\"(\\d{13})\"");
                var start = new DateTime(1970, 1, 1, 8, 0, 0);
                return start + TimeSpan.FromMilliseconds(long.Parse(mc.Groups[1].Value));
            }
        }
        public static async void startCounter(string code)
        {
            try
            {
                code = new SymmetryEncrypt().Decrypto(code);
                var t = long.Parse(code.Substring(0, LEN));
                var diff = new DateTime(t) - await GetNow();
                if (diff.TotalMilliseconds <= int.MaxValue)
                {
                    await Task.Delay(diff);
                }
                else
                {
                    await Task.Delay(int.MaxValue);
                    startCounter(code);
                    return;
                }
            }
            catch (Exception)
            {
            }
            MessageBox.Show("授权已过期，请重新登录");
            Application.Current.Shutdown();
        }
        public static string CodeFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/code.txt";
    }
}
