using Newtonsoft.Json;
using OdyLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.entities;
using WpfApp.utils;
using WpfApp.Windows;

namespace WpfApp1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //#if DEBUG
            //            if (Tools.McCode == "3397f2273c0b91efbe6f94536108b31b")
            //            {
            //                new RegMachine().Show();
            //                return;
            //            }
            //#endif
            //new MainWindow().Show();
            tools.AutoRegCom("regsvr32 /s resources/dm.dll");

            var path = Tools.CodeFilePath;
            var is_ok = File.Exists(path);
            string code = "";
            if (is_ok)
            {
                code = File.ReadAllText(path);
                is_ok = Tools.validate(code);
            }
            if (is_ok)
            {
                new MainWindow().Show();
                Tools.startCounter(code);
            }
            else
            {
                new Login().Show();
            }
        }
    }
}
