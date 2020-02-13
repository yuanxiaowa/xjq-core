using MahApps.Metro.Controls;
using OdyLibrary;
using System;
using System.Collections.Generic;
using System.IO;
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
using WpfApp.entities;
using WpfApp1;

namespace WpfApp.Windows
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : MetroWindow
    {
        public Login()
        {
            InitializeComponent();
            mc_code.Text = Tools.McCode;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var code = auth_code.Text;
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("请输入授权码");
                return;
            }
            if (Tools.validate(code))
            {
                //if(!File.Exists(Tools.CodeFilePath))
                //{
                //    File.Create(Tools.CodeFilePath);
                //}
                File.WriteAllText(Tools.CodeFilePath, code);
                new MainWindow().Show();
                Close();
                Tools.startCounter(code);
            }
            else
            {
                MessageBox.Show("授权码不正确，请重试");
            }
        }
    }
}
