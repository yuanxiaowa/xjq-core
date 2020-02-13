using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp.entities;
using WpfApp1;
using WpfApp1.entities;

namespace WpfApp.Windows
{
    /// <summary>
    /// TagEnemy.xaml 的交互逻辑
    /// </summary>
    public partial class TagEnemy : Window
    {
        static int count = 4;
        class CachedUser
        {
            public string Filename { get; set; }
            public int[] Indexes { get; set; }
            public UserInfo User { get; set; }
        }
        MainWindow parent;
        CachedUser[] users = new CachedUser[count];
        Queue<CachedUser> cached_users = new Queue<CachedUser>();
        public TagEnemy(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.Owner = parent;
            //con.Children
            //brush.ImageSource = GetImageSource(filename);
            for (int i = 0; i < count; i++)
            {
                var container = new Grid();
                container.VerticalAlignment = VerticalAlignment.Top;
                container.SetValue(Grid.ColumnProperty, i);
                con.Children.Add(container);
            }
            Width = count * 215;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        void clearGrid(int i)
        {
            var container = con.Children[i] as Grid;
            container.Children.Clear();
            users[i] = null;
        }
        void initGrid(int i, string filename, int[] indexes)
        {
            var container = con.Children[i] as Grid;
            var img = new Image();
            img.Width = 203;
            img.Height = 331;
            img.Source = GetImageSource(filename);
            img.VerticalAlignment = VerticalAlignment.Top;
            container.Children.Add(img);
            var grid = new Grid();
            container.Children.Add(grid);
            for (int j = 0; j < 5; j++)
            {
                var button = new Button();
                button.Content = "" + (j + 1);
                button.Width = 200;
                button.Height = 50;
                button.Background = new SolidColorBrush(Color.FromArgb(60, 50, 20, 255));
                var tk = new Thickness();
                tk.Top = 70 * j;
                button.Margin = tk;
                button.VerticalAlignment = VerticalAlignment.Top;
                button.BorderBrush = indexes.Contains(j) ? Brushes.Red : Brushes.White;
                button.Click += Button_Click1;
                button.Focusable = false;
                button.Foreground = Brushes.HotPink;
                button.FontSize = 30;
                grid.Children.Add(button);
            }
            //var btn_grid = new Grid();
            //container.Children.Add(btn_grid);
            var btn = new Button();
            btn.Content = "取消";
            btn.Width = 70;
            btn.Height = 30;
            var tk2 = new Thickness();
            tk2.Top = 340;
            tk2.Left = 10;
            btn.Margin = tk2;
            btn.HorizontalAlignment = HorizontalAlignment.Left;
            container.Children.Add(btn);
            btn.Click += Btn_Click;

            btn = new Button();
            btn.Content = "确定";
            btn.Width = 110;
            btn.Height = 30;
            btn.HorizontalAlignment = HorizontalAlignment.Left;
            tk2 = new Thickness();
            tk2.Top = 340;
            tk2.Left = 90;
            btn.Margin = tk2;
            container.Children.Add(btn);
            btn.Click += Btn_Click;
        }
        void UpdateGrid(int i, CachedUser data)
        {
            clearGrid(i);
            users[i] = data;
            initGrid(i, data.Filename, data.Indexes);
        }

        public void AddTag(string filename, UserInfo user, int[] indexes)
        {
            this.Activate();
            this.Focus();
            var data = new CachedUser()
            {
                Filename = filename,
                User = user,
                Indexes = indexes
            };
            for (int i = 0; i < users.Length; i++)
            {
                var item = users[i];
                if (item != null && item.User == user)
                {
                    UpdateGrid(i, data);
                    return;
                }
            }
            cached_users.Enqueue(data);
            Next();

        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var container = btn.Parent as Grid;
            var index = con.Children.IndexOf(container);
            if (btn.Content.ToString() == "确定")
            {
                var btns_con = container.Children[1] as Grid;
                var btns = btns_con.Children;
                var list = new List<int>();
                for (int i = 0; i < btns.Count; i++)
                {

                    var _btn = btns[i] as Button;
                    if (_btn.BorderBrush != Brushes.White)
                    {
                        list.Add(i);
                    }
                }
                var indexes = list.ToArray();
                parent.SetTagEnemy(users[index].User, indexes);
                FuhunName.ExtractName(users[index].Filename, indexes);
            }
            clearGrid(index);
            Next();
        }

        void Next()
        {
            if (cached_users.Count == 0)
            {
                return;
            }
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i] == null)
                {
                    users[i] = cached_users.Dequeue();
                    initGrid(i, users[i].Filename, users[i].Indexes);
                    break;
                }
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.BorderBrush == Brushes.White)
            {
                button.BorderBrush = Brushes.Red;
            }
            else
            {
                button.BorderBrush = Brushes.White;
            }
        }

        BitmapImage GetImageSource(string filename)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            using (Stream ms = new MemoryStream(File.ReadAllBytes(filename)))
            {
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                bitmap.Freeze();
            }
            return bitmap;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.clearDestroyTags();
        }
    }
}
