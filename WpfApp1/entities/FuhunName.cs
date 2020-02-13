using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.entities
{
    class FuhunName
    {
        //static BitmapData bracket_data;
        //static int width = 0;
        //static int height = 0;
        static string folder = "fuhun-name";
        static FuhunName()
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            //var bmap = new Bitmap("resources/附魂-bracket.bmp");
            //width = bmap.Width;
            //height = bmap.Height;
            //var rect = new Rectangle(0, 0, bmap.Width, bmap.Height);
            //bracket_data = bmap.LockBits(rect, ImageLockMode.ReadOnly, bmap.PixelFormat);
            //new Bitmap(117, 14);
            //img.GetPixel()
        }

        public static void ExtractName(string filename, int[] indexes)
        {
            var w = 157;
            var h = 15;
            var bmap = new Bitmap(filename);
            var name = DateTime.Now.Ticks + "";
            foreach (var index in indexes)
            {
                var _bmap = new Bitmap(w, h, bmap.PixelFormat);
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        _bmap.SetPixel(i, j, bmap.GetPixel(13 + i, 9 + j + 68 * index));
                    }
                }
                _bmap.Save(string.Format("{0}/{1}.bmp", folder, name + index), ImageFormat.Bmp);
            }
            //var rect = new Rectangle(0, 0, bmap.Width, bmap.Height);
            //var bmap_data = bmap.LockBits(rect, ImageLockMode.ReadOnly, bmap.PixelFormat);
            //unsafe
            //{
            //    var p = (byte*)bmap_data.Scan0;
            //    var _rect = new Rectangle(0, 0, w, h);
            //    foreach (var index in indexes)
            //    {
            //        var _bmap = new Bitmap(w, h, bmap.PixelFormat);
            //        var _data = _bmap.LockBits(_rect, ImageLockMode.ReadOnly, bmap.PixelFormat);
            //        var _p = (byte*)_data.Scan0;
            //        for (int i = 0; i < h; i++)
            //        {
            //            for (int j = 0; j < w; j++)
            //            {
            //                for (int k = 0; k < 3; k++)
            //                {
            //                    _p[(i * w + j) * 3 + k] = p[((9 + i + 68 * index) * bmap.Width + (13 + j)) * 3 + k];
            //                }
            //            }
            //        }
            //        _bmap.UnlockBits(_data);
            //        _bmap.Save(DateTime.Now.Ticks + ".bmp");
            //    }
            //}
            //bmap.UnlockBits(bmap_data);
        }
    }
}
