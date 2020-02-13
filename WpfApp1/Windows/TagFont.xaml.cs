using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp.Windows
{
    /// <summary>
    /// TagFont.xaml 的交互逻辑
    /// </summary>
    public partial class TagFont : Window
    {
        class FontEntity
        {
            public string Name { get; set; }
            public string Mask { get; set; }
            public int Row { get; set; }
            public string PointInfo { get; set; }
        }
        public TagFont()
        {
            InitializeComponent();
            Init();
        }

        List<FontEntity> fonts;
        string font_filename = "resources/font-enemies.txt";
        int last_index = 0;
        private void Init()
        {
            var text = File.ReadAllText(font_filename, Encoding.GetEncoding("gb2312"));
            lb_list.ItemsSource = fonts = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select((item, i) =>
                {
                    var arr = item.Split('$');
                    return new FontEntity()
                    {
                        Name = "敌人" + (i + 1),
                        Mask = arr[0],
                        Row = int.Parse(arr[3]),
                        PointInfo = arr[2]
                    };
                })
                .ToList();
            lb_list.SelectedIndex = 0;
            last_index = fonts.Count - 1;
            //Show(fonts[0].Mask);
        }

        void Show(string mat_str)
        {
            var mask = "";
            foreach (var c in mat_str)
            {
                mask += Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
            }
            var row = 11;
            var col = mask.Length / row;
            mask = mask.Substring(0, row * col);
            Bitmap bmap = new Bitmap(col, row);
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    var c = int.Parse(mask[i * row + j].ToString()) * 255;
                    bmap.SetPixel(i, j, Color.FromArgb(c, c, c));
                }
            }
            var bitmapImage = new BitmapImage();
            using (var ms = new MemoryStream())
            {
                bmap.Save(ms, ImageFormat.Bmp);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            img.Width = col;
            img.Source = bitmapImage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var filenames = Directory.EnumerateFiles("fuhun-name");
            var i = lb_list.SelectedIndex;
            foreach (var filename in filenames)
            {
                var fe = ExtractFont(filename);
                i = fonts.FindIndex(item => item.Mask == fe.Mask);
                if (i == -1)
                {
                    last_index++;
                    fe.Name = "敌人" + (last_index + 1);
                    fonts.Add(fe);
                    i = fonts.Count - 1;
                }
            }
            Refresh(i);
            SaveFonts();
        }

        private void lb_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_list.SelectedIndex == -1)
            {
                img.Source = null;
                return;
            }
            Show(fonts[lb_list.SelectedIndex].Mask);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var i = lb_list.SelectedIndex;
            if (i == -1)
            {
                return;
            }
            fonts.RemoveAt(i);
            Refresh(i);
            SaveFonts();
        }

        private void Refresh(int i)
        {
            lb_list.ItemsSource = null;
            lb_list.ItemsSource = fonts;
            if (i < fonts.Count)
            {
                lb_list.SelectedIndex = i;
                lb_list.ScrollIntoView(lb_list.SelectedItem);
            }
        }

        private void SaveFonts()
        {
            var list = fonts.Select((item, i) =>
            {
                return string.Join("$", new object[]
                {
                    item.Mask,
                    "敌人"+i,
                    item.PointInfo,
                    item.Row
                });
            });
            File.WriteAllText(font_filename, string.Join("\r\n", list), Encoding.GetEncoding("gb2312"));
        }

        /// <summary>
        /// 提取字体
        /// </summary>
        /// <param name="filename"></param>
        /// <see cref="http://www.360doc.com/content/15/0730/21/9200790_488444086.shtml"/>
        /// <returns></returns>
        FontEntity ExtractFont(string filename)
        {
            var bitmap = new Bitmap(filename);
            var width = bitmap.Width;
            var height = bitmap.Height;
            var baseColor = ColorTranslator.FromHtml("#f1ffb1");
            var matrix = new int[width, height];
            var total_point = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var color = bitmap.GetPixel(i, j);
                    if (
                        Math.Abs(baseColor.R - color.R) <= 0x55 &&
                        Math.Abs(baseColor.G - color.G) <= 0x55 &&
                        Math.Abs(baseColor.B - color.B) <= 0x55
                      )
                    {
                        matrix[i, j] = 1;
                        total_point++;
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
            var top = 0;
            var left = 0;
            var right = width - 1;
            var bottom = height - 1;
            //左
            for (; left < right; left++)
            {
                var is_emtpy = true;
                for (int j = 0; j <= bottom; j++)
                {
                    if (matrix[left, j] == 1)
                    {
                        is_emtpy = false;
                        break;
                    }
                }
                if (!is_emtpy)
                {
                    break;
                }
            }
            //右
            for (; right >= left; right--)
            {
                var is_emtpy = true;
                for (int j = 0; j <= bottom; j++)
                {
                    if (matrix[right, j] == 1)
                    {
                        is_emtpy = false;
                        break;
                    }
                }
                if (!is_emtpy)
                {
                    break;
                }
            }
            //上
            for (; top < bottom; top++)
            {
                var is_emtpy = true;
                for (int i = left; i <= right; i++)
                {
                    if (matrix[i, top] == 1)
                    {
                        is_emtpy = false;
                        break;
                    }
                }
                if (!is_emtpy)
                {
                    break;
                }
            }
            //下
            for (; bottom >= top; bottom--)
            {
                var is_emtpy = true;
                for (int i = left; i <= right; i++)
                {
                    if (matrix[i, bottom] == 1)
                    {
                        is_emtpy = false;
                        break;
                    }
                }
                if (!is_emtpy)
                {
                    break;
                }
            }
            var mask = "";
            for (int i = left; i <= right; i++)
            {
                for (int j = top; j <= Math.Min(bottom, top + 10); j++)
                {
                    mask += matrix[i, j];
                }
            }
            var r = mask.Length % 4;
            if (r > 0)
            {
                mask = mask.PadRight((mask.Length / 4 + 1) * 4, '0');
            }
            var arr = Regex.Split(mask, "(?<=^(?:\\d{4})+)(?!$)")
                    .Select(item => Convert.ToString(Convert.ToInt32(item, 2), 16).ToUpper());
            //var result = string.Format("{0}${1}${2}${3}", string.Join("", arr), "", $"0.0.{total_point}", bottom - top + 1);
            return new FontEntity()
            {
                Mask = string.Join("", arr),
                PointInfo = $"0.0.{total_point}",
                Row = bottom - top + 1
            };
        }
    }
}
