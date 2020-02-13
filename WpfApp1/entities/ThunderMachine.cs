using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp1.entities
{
    //class ThunderMachine
    //{
    //    public dmsoft dm = new dmsoft();
    //    public int left = 0;
    //    public int top = 0;
    //    int width = 551;
    //    int height = 979;
    //    public ThunderMachine()
    //    {
    //        var main = dm.FindWindow("LDPlayerMainFrame", "");
    //        var hwnd = dm.FindWindowEx(main, "RenderWindow", "");
    //        dm.BindWindowEx(hwnd, "gdi", "windows", "windows", "", 0);
    //    }
    //    public void setDict(string filename)
    //    {
    //        dm.SetDict(0, filename);
    //    }

    //    public async Task waitForColorAndClick(string color, int x, int y)
    //    {
    //        while (!isColorAt(color, x, y))
    //        {
    //            await Task.Delay(100);
    //        }
    //        click(x, y);
    //    }

    //    public async Task waitForColor(string color, int x, int y)
    //    {
    //        while (!isColorAt(color, x, y))
    //        {
    //            await Task.Delay(100);
    //        }
    //    }

    //    public Boolean isColorAt(string color, int x, int y)
    //    {
    //        return dm.GetColor(x, y) == color;
    //    }

    //    public async Task WaitForStr(string str, string color)
    //    {
    //        int x = 0;
    //        int y = 0;
    //        while (findStr(str, color, out x, out y) == -1)
    //        {
    //            await Task.Delay(100);
    //        }
    //    }

    //    public async Task WaitForStrAndClick(string str, string color)
    //    {
    //        int x = 0;
    //        int y = 0;
    //        while (findStr(str, color, out x, out y) == -1)
    //        {
    //            await Task.Delay(100);
    //        }
    //        click(x, y);
    //    }

    //    public void findStrAndClick(string str, string color)
    //    {
    //        int x = 0;
    //        int y = 0;
    //        var r = findStr(str, color, out x, out y);
    //        if (r > -1)
    //        {
    //            click(x, y);
    //        }
    //    }

    //    public async Task move(int x1, int y1, int x2, int y2)
    //    {
    //        dm.MoveTo(x1, y1);
    //        dm.LeftDown();
    //        await Task.Delay(16);
    //        dm.MoveTo(x1 + (x2 - x1) / 2, y1 + (y2 - y1) / 2);
    //        await Task.Delay(16);
    //        dm.MoveTo(x2, y2);
    //        dm.LeftUp();
    //    }


    //    public int findStr(string str, string color, out int x, out int y)
    //    {
    //        object intX;
    //        object intY;
    //        var ret = dm.FindStr(left, top, width, height, str, color, 0.9, out intX, out intY);
    //        x = (int)intX;
    //        y = (int)intY;
    //        return ret;
    //    }

    //    public void click(int x, int y)
    //    {
    //        dm.MoveTo(x, y);
    //        dm.LeftClick();
    //    }

    //    public void keypress(int key)
    //    {
    //        dm.KeyPress(key);
    //    }
    //}
}
