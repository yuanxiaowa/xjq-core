using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;
//using mshtml;
using WpfApp1.entities;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using WpfApp1.utils;
using WpfApp1.Windows;
using WpfApp.utils;
using MahApps.Metro.Controls;
using OdyLibrary;
using System.Web;
using WpfApp.Windows;
using WpfApp.entities;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public ObservableCollection<UserInfo> Columns { get; set; } = new ObservableCollection<UserInfo>();
        //OneServiceRemoteProvider clientProvider;
        List<NameValue> menus = new List<NameValue> {
            new NameValue() { Name = "打开",Value="open" },
            new NameValue() { Name = "暂停",Value="pause" },
            new NameValue() { Name = "关闭" ,Value="close"},
            new NameValue() { Name="添加账号",Value="add" },
            new NameValue() { Name = "刷新" ,Value="refresh"},
            new NameValue() { Name = "编辑",Value="edit" },
            new NameValue() { Name = "删除",Value="delete" }
        };

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitClients();
            //this.cmb.ItemsSource = tools.GetAreas();
            //this.loadConfig();
            //this.ctrl_menu.ItemsSource = menus;
            //InitProvider();
            this.Closing += MainWindow_Closing;
            chk_auto_trans.Checked += OnChange;
            chk_auto_trans.Unchecked += OnChange;
            rb_revive_1.Checked += OnChange;
            rb_revive_1.Unchecked += OnChange;
            rb_revive_2.Checked += OnChange;
            rb_revive_2.Unchecked += OnChange;
            chk_auto_select_role.Checked += OnChange;
            chk_auto_select_role.Unchecked += OnChange;
            chk_locate_interval.Unchecked += OnChange;
            chk_locate_interval.Checked += OnChange;
            tb_locate_interval.TextChanged += OnChange;
        }

        private void InitClients()
        {
            var hwnds = Tg.GetWindowIds();
            var datas = hwnds.Select(item =>
             {
                 var column = Columns.FirstOrDefault(_item => _item.GameHwnd == item);
                 if (column != null)
                 {
                     return column;
                 }
                 var provider = new GameOperation();
                 column = new UserInfo()
                 {
                     GameHwnd = item,
                     Provider = provider,
                     Name = "凡人修仙",
                     State = "在线"
                 };
                 provider.DataEvent += (type, value) =>
                 {
                     RemoteProvider_OnReceivedMsg(type, column, value);
                 };
                 provider.StateChange += (state) =>
                   {
                       column.State = state;
                       RefreshTable();
                   };
                 provider.operate("init", item);
                 return column;
             }).ToList();
            dg.ItemsSource = Columns = new ObservableCollection<UserInfo>(datas);
        }

        GameSetting GetSetting()
        {
            var setting = new GameSetting()
            {
                AutoTrans = chk_auto_trans.IsChecked == true,
                AutoSelectRole = chk_auto_select_role.IsChecked == true,
                ReviveWay = rb_revive_1.IsChecked == true ? 0 : 1,
                LocateInterval = chk_locate_interval.IsChecked == true ? int.Parse(tb_locate_interval.Text) : 0
            };
            return setting;
        }

        private void OnChange(object sender, RoutedEventArgs e)
        {
            ExcuteAction("game-setting", JsonConvert.SerializeObject(GetSetting()));
        }

        RemoteService remoteService;
        string remote_name = Guid.NewGuid().ToString();
        //void InitProvider()
        //{
        //    remoteService = new RemoteService(remote_name, "server");
        //    remoteService.remoteProvider.OnReceivedMsg += RemoteProvider_OnReceivedMsg;
        //}

        void SetUserCookie(string str)
        {
            var arr = str.Split(new char[] { '&' });
            var column = Columns.First(item => item.GameHwnd == arr[0]);
            column.Cookie = arr[1];
            saveConfig();
        }

        private void RemoteProvider_OnReceivedMsg(string type, UserInfo column, string value)
        {
            try
            {
                if (type == "game-start")
                {
                    this.Invoke(async () =>
                    {
                        await Task.Delay(20);
                        try
                        {
                            column.Provider.operate("game-setting", JsonConvert.SerializeObject(GetSetting()));

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    });
                }
                else if (type == "game-exit")
                {
                    column.Reset();
                }
                else if (type == "state-change")
                {
                    column.State = value;
                }
                else if (type == "fuhun-capture")
                {
                    this.Invoke(new Action(() =>
                    {
                        ToTagEnemy(column, value);
                    }));
                }
                else if (type == "win-state")
                {
                    column.WinState = value;
                }
                else if (type == "fuhun-end")
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(1000);
                        //if (DateTime.Now.Hour == 19)
                        //{
                        //    column.Provider.operate("fuhun-init");
                        //}

                        column.Provider.operate("fuhun-init");
                    });
                }
                RefreshTable();
            }
            catch (Exception)
            {
            }
        }

        void ConnectClient(UserInfo user, string path)
        {
            //user.Provider = RemoteService.GetRemoteObject<OneServiceRemoteProvider>(path, "client");

            //var op = new GameOperation(column);
            //op.OnStateChange += RefreshTable;
            //op.OnEnterGame += () =>
            //{
            //    column.Provider.SendMsg(ServiceRemoteType.ResetWindowName);
            //};
            //op.operate("init");
            //column.Operator = op;
            //column.State = "在线";
            RefreshTable();
        }

        public void addUser(UserInfoBase user)
        {
            if (!String.IsNullOrEmpty(user.ID))
            {
                var column = this.Columns.First(item => item.ID == user.ID);
                column.Assign(user);
            }
            else
            {
                Columns.Add(new UserInfo(user));
            }
            this.RefreshTable();
            this.saveConfig();
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUser()
        {
            //var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.TesseractAndCube);
            //ocr.get
            //Test();
            //var  dm = new dmsoft();
            //var parent = dm.FindWindow("Chrome_WidgetWin_1", null);
            //var hwnd = dm.FindWindowEx(parent, "Chrome_WidgetWin_1", null);
            ////MessageBox.Show(hwnd + "");
            //dm.SetWindowText(parent, "hello");
            //dm.SendString(hwnd, "hello world");
            //dm.KeyPress(50);
            var win = new UserWindow(this);
            win.ShowDialog();
        }

        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrl_menu_Click(object sender, RoutedEventArgs e)
        {
            var menu_item = e.OriginalSource as MenuItem;
            var header = menu_item.Header as NameValue;
            if (header.Value == "add")
            {
                AddUser();
                return;
            }
            if (dg.SelectedIndex == -1)
            {
                return;
            }
            var item = Columns.ElementAt(dg.SelectedIndex);
            if (header.Value == "open")
            {
                if (item.Provider == null)
                {
                    item.GameHwnd = Guid.NewGuid().ToString();
                    //item.Win = new SubGame(this, item);
                    //item.Win.Show();
                    item.State = "登陆中";
                    openGame(item);
                }
                else
                {
                    SendMsg(item, "focus");
                }
                this.RefreshTable();
            }
            else if (header.Value == "pause")
            {
                if (item.Provider != null)
                {
                    SendMsg(item, "pause");
                }
            }
            else if (header.Value == "close")
            {
                if (item.Provider != null)
                {
                    SendMsg(item, header.Value);
                }
            }
            else if (header.Value == "refresh")
            {
                if (item.Provider != null)
                {
                    //if (item.Operator != null)
                    //{
                    //    item.Operator.Abort();
                    //    item.Operator = null;
                    //}
                    SendMsg(item, header.Value);
                }
            }
            else if (header.Value == "delete")
            {
                if (item.Provider != null)
                {
                    SendMsg(item, header.Value);
                }
                Columns.Remove(item);
                this.saveConfig();
            }
            else if (header.Value == "edit")
            {
                new UserWindow(this, item).ShowDialog();
            }

        }

        void SendMsg(UserInfo item, string name = "", string value = "")
        {
            try
            {
                item.Provider.operate(name, value);
            }
            catch (Exception)
            {
                item.Reset();
                RefreshTable();
            }
        }
        async void RefreshTable()
        {
            await Task.Delay(60);
            Dispatcher.Invoke(() =>
            {
                dg.Items.Refresh();
                //dg.ItemsSource = null;
                //dg.ItemsSource = columns;
            });
        }

        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ExcuteAction("shout", ctrl_msg.Text);
        }

        /// <summary>
        /// 寻路
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(ctrl_xunlu.Text))
            {
                MessageBox.Show("请输入坐标");
                return;
            }
            ExcuteAction("locate", ctrl_xunlu.Text.Trim());
        }

        void ExcuteAction(string type, string value = "")
        {
            foreach (var item in this.Columns)
            {
                if (item.Checked && item.Provider != null)
                {
                    try
                    {
                        item.Provider.operate(type, value);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        TagEnemy te;
        void ToTagEnemy(UserInfo user, string path)
        {
            if (te == null)
            {
                te = new TagEnemy(this);
            }
            te.Show();
            var arr = path.Split(new char[] { '?' });
            var filename = arr[0];
            var indexes = new int[] { };
            if (arr.Length > 1)
            {
                indexes = arr[1].Split(new char[] { ',' }).Select(item => int.Parse(item)).ToArray();
            }
            //te.AddTag(@"E:\Users\Administrator\source\repos\WpfApp1\MyTool\bin\Debug\debug\fuhun\637160951978766004.bmp", user);
            te.AddTag(filename, user, indexes);
            SetTagEnemy(user, indexes);
        }

        /// <summary>
        /// 清除标记敌人窗口
        /// </summary>
        public void clearDestroyTags()
        {
            te = null;
        }

        /// <summary>
        /// 开始附魂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //ToTagEnemy(Columns.First(), @"E:\Users\Administrator\source\repos\WpfApp1\WpfApp1\bin\Debug\fuhun\637160951978766004.bmp?1,3");
            //return;
            //new TagEnemy(@"E:\Users\Administrator\source\repos\WpfApp1\MyTool\bin\Debug\debug\fuhun\637159218502426004.bmp", this, columns.First()).ShowDialog();
            ExcuteAction("fuhun-init");
            //for (var i = 0; i < Columns.Count; i++)
            //{
            //    var column = Columns[i];
            //    if (column.Checked && column.Provider != null)
            //    {
            //        column.Provider.SendMsg("fuhun-init");
            //        //var path = column.Operator.Capture(0, 0, 2000, 2000);
            //        //new TagEnemy(path, this, column).ShowDialog();
            //    }
            //}
            //var dm = new dmsoft();
            //var main = dm.FindWindow("Notepad++", "");
            //var hwnd = dm.FindWindowEx(main, "Scintilla", "");
            //dm.BindWindowEx(hwnd, "gdi", "windows", "windows", "", 1);
            //dm.MoveTo(424, 152);
            //dm.LeftClick();
        }

        /// <summary>
        /// 标记完敌人，通知窗口
        /// </summary>
        /// <param name="user"></param>
        /// <param name="indexes"></param>
        public void SetTagEnemy(UserInfo user, int[] indexes)
        {
            user.Provider.operate("fuhun-tag", string.Join(",", indexes));
        }
        void openGame(UserInfoBase user)
        {
            //if (clientProvider != null)
            //{
            //    clientProvider.SendMsg("new-user", user.GameHwnd, JsonConvert.SerializeObject(user));
            //    return;
            //}
            var user_str = HttpUtility.UrlEncode(JsonConvert.SerializeObject(user));
#if DEBUG
            File.WriteAllText("a.txt", String.Format("{0} {1}", remote_name, user_str));
            //return;
#endif
            //MessageBox.Show(String.Format("{0} {1}", remote_name, user_str));
            Process.Start(
               //Process.GetCurrentProcess().MainModule.FileName,
#if DEBUG
               @"E:\Users\Administrator\source\repos\WpfApp1\GameWindow\bin\Debug\GameWindow.exe",
#else
               @"GameWindow.exe",
#endif
               string.Format("{0} {1}", remote_name, user_str)
            );
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            //clientProvider?.SendMsg("close");
            ExcuteAction("close");
        }

        void loadConfig()
        {
            var columns = config.loadUsers().Select(item => UserInfo.FromUserBase<UserInfo>(item));
            dg.ItemsSource =
                Columns = new ObservableCollection<UserInfo>(columns);
        }

        void saveConfig()
        {
            config.saveUsers(Columns.Select(item => item as UserInfoBase).ToList());
        }

        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ExcuteAction("screenshot");
        }

        /// <summary>
        /// 自动打架
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(ctrl_xunlu.Text))
            {
                MessageBox.Show("请输入坐标");
                return;
            }
            ExcuteAction("fight", ctrl_xunlu.Text);
        }

        /// <summary>
        /// 跨服挑战
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(ctrl_xunlu.Text))
            {
                MessageBox.Show("请输入坐标");
                return;
            }
            ExcuteAction("kuafu-boss", ctrl_xunlu.Text);
        }

        /// <summary>
        /// 全选/取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (this.Columns.Count == 0)
            {
                return;
            }
            var b = !Columns[0].Checked;
            foreach (var item in this.Columns)
            {
                item.Checked = b;
            }
            RefreshTable();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            new TagFont().ShowDialog();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            InitClients();
        }

        //private void Button_Click_5(object sender, RoutedEventArgs e)
        //{
        //    var dm = new Dm.dmsoft();
        //    dm.SetDict(0, "resources/font2.txt");
        //    object x;
        //    object y;
        //    var r = dm.FindStr(0, 0, 2000, 2000, "方", "ffffff-555555", 0.8, out x, out y);
        //    if (r > -1)
        //    {
        //        dm.MoveTo((int)x, (int)y);
        //    }
        //    else
        //    {
        //        dm.MoveTo(17, 16);
        //        dm.LeftClick();
        //    }
        //}
    }
}
