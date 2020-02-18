using OdyLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.entities
{
    class Tg
    {
        public static List<string> GetWindowIds()
        {
            var dm = new MyDm();
            //dm.GetWindowClass()

            var str = dm.EnumWindow(0, "", "Afx:", 0b11110);
            //EnumWindows((int hWnd, int lParam) =>
            //{
            //    var name = dm.GetWindowClass(hWnd);
            //    if (name.StartsWith("Afx:"))
            //    {
            //        dm.EnumWindow
            //        var state = dm.GetWindowState(hWnd, 2);
            //        if (state == 1) { 
            //            list.Add(hWnd + " " + name + "" );
            //        }
            //    }
            //    return true;
            //}, IntPtr.Zero);
            //Console.WriteLine(list.ToArray());

            return str.Split(',').Select(item =>
            {
                var hwnd = int.Parse(item);
                var h1 = dm.EnumWindow(hwnd, "", "MacromediaFlashPlayerActiveX", 2 + 16);

                var arr = h1.Split(',');
                if (arr.Length > 0)
                {
                    return arr[0];
                }
                return "";

            }).Where(item => item.Length > 0).ToList();
        }

    }
}
