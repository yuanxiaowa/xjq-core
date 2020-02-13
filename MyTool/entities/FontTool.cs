using OdyLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyTool.entities
{
    public class FontTool
    {
        static string source_dir = @"C:\Users\Administrator\Documents\WeChat Files\yuanxiaowa\FileStorage\File\2020-02";
        static string output_dir_1 = "images";
        public static void CleanImage()
        {
            HashSet<string> ht = new HashSet<string>();
            int ht_num = 0;
            if (!Directory.Exists(output_dir_1))
            {
                Directory.CreateDirectory(output_dir_1);
            }
            var filenames = Directory.EnumerateFiles($"{source_dir}\\fuhun-name");
            foreach (var filename in filenames)
            {
                var attrs = File.GetAttributes(filename);
                var hash = SHA1.Create();
                using (var stream = new FileStream(filename, FileMode.Open))
                {
                    var hashBytes = hash.ComputeHash(stream);
                    var hashCode = BitConverter.ToString(hashBytes);
                    if (ht.Contains(hashCode))
                    {
                        continue;
                    }
                    var bitmap = new Bitmap(stream);
                    bitmap.Save(string.Format("{0}/{1}.bmp", output_dir_1, ++ht_num), ImageFormat.Bmp);
                    ht.Add(hashCode);
                }
                //File.Delete(filename);
            }
        }
        static string[] GetEnemyNames()
        {
            var text = File.ReadAllText(font_file, Encoding.GetEncoding("gb2312"));
            var regex = new Regex(@"\$(.*?)\$");
            var mcs = regex.Matches(text);
            var names = mcs.Cast<Match>().Select(item => item.Groups[1].Value).ToArray();
            return names;
        }

        static string enemy_fonts;
        static string font_file = "resources/font-enemies.txt";
        public static string CheckEnemy(MyDm dm)
        {
            if (string.IsNullOrEmpty(enemy_fonts))
            {
                enemy_fonts = string.Join("|", GetEnemyNames());
                dm.SetDict(0, font_file);
            }
            var p = dm.FindPic(0, 0, 2000, 2000, "resources/arrow1.bmp");
            if (p.X <= 0)
            {
                return null;
            }
            var w = 203;
            var h = 331;
            var left = p.X - w;
            var top = p.Y;
            var mask_str = "";
            for (int i = 0; i < 5; i++)
            {
                var x1 = left + 13;
                var y1 = top + 9 + 68 * i;
                var r = dm.FindStr(x1, y1, x1 + 157, y1 + 15, enemy_fonts, "f1ffb1-555555");
                mask_str += r.X > 0 ? 1 : 0;
            }
            return mask_str;
        }

        static Regex digit_reg = new Regex("(\\d+)");
        public static void FilterFontImage()
        {
            var filenames = Directory.EnumerateFiles(output_dir_1).Where(item => item.EndsWith(".bmp"));
            var nums = GetEnemyNames().Select(item => string.Format("{0}\\{1}.bmp", output_dir_1, digit_reg.Match(item).Groups[1].Value));
            foreach (var filename in filenames)
            {
                if (!nums.Contains(filename))
                {
                    File.Delete(filename);
                    //Console.WriteLine(filename);
                }
            }
        }
    }
}
