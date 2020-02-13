using OdyLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        public static async void startCounter(string code)
        {
            try
            {
                code = new SymmetryEncrypt().Decrypto(code);
                var t = long.Parse(code.Substring(0, LEN));
                var diff = new DateTime(t) - DateTime.Now;
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
            //Application.Current.Shutdown();
        }
        public static string CodeFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/code.txt";
    }
}
