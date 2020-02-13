using Newtonsoft.Json;
using OdyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace GameWindow
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length >= 2)
            {

                try
                {
                    var user_str = HttpUtility.UrlDecode(args[1]);
                    var user = JsonConvert.DeserializeObject<UserInfoBase>(user_str);
                    var provider = RemoteService.GetRemoteObject<OneServiceRemoteProvider>(args[0], "server");
                    Application.Run(new GameWindow(provider, user));
                }
                catch (Exception evt)
                {
                    MessageBox.Show("出错了:" + evt.Message);
                }
            }
            else
            {
                Application.Run(new GameWindow());
            }
        }
    }
}
