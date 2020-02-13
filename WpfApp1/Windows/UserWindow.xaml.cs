using MahApps.Metro.Controls;
using OdyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.entities;
using WpfApp1.utils;

namespace WpfApp1.Windows
{
    /// <summary>
    /// User.xaml 的交互逻辑
    /// </summary>
    public partial class UserWindow : MetroWindow
    {
        MainWindow parent;
        List<NameValue> areas = new List<NameValue>();
        string id = "";
        public UserWindow(MainWindow parent)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.parent = parent;
            ShowInTaskbar = false;
            loadAreas();
            var roles = new List<NameValue>();
            for (int i = 0; i < 4; i++)
            {
                roles.Add(new NameValue()
                {
                    Name = (i + 1) + "",
                    Value = i + ""
                });
            }
            ctrl_role.ItemsSource = roles;
        }
        string AreaName;
        public UserWindow(MainWindow parent, UserInfoBase user) : this(parent)
        {
            this.ctrl_name.Text = user.Name;
            this.ctrl_password.Password = user.Password;
            AreaName = user.AreaName;
            this.id = user.ID;
            this.ctrl_role.SelectedIndex = user.RoleIndex;
        }

        private void Ctrl_Area_Checked(object sender, RoutedEventArgs e)
        {
            loadAreas();
        }

        void loadAreas()
        {
            new Thread(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    ctrl_area.ItemsSource = this.areas = (
                    ctrl_Is9wan.IsChecked == true ?
                        game.Get91wanAreas() :
                        ctrl_Is4399.IsChecked == true ?
                            game.Get4399Areas() : game.Get360Areas());
                    this.ctrl_area.Text = AreaName;
                });
            }).Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Name = ctrl_name.Text.Trim();
            var Password = ctrl_password.Password;
            var index = ctrl_area.SelectedIndex;
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Password) || index < 0)
            {
                MessageBox.Show("请将信息补充完整");
                return;
            }
            var area = areas[index];
            parent.addUser(new UserInfoBase()
            {
                ID = id,
                Name = Name,
                Password = Password,
                Nickname = ctrl_nickname.Text,
                AreaName = area.Name,
                AreaValue = area.Value,
                RoleIndex = ctrl_role.SelectedIndex
            });
            if (ctrl_continue.IsChecked != true)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("添加成功");
            }
        }
    }
}
