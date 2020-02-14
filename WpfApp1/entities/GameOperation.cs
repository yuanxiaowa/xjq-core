using Newtonsoft.Json;
using OdyLibrary;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WpfApp1.entities
{
    public class GameOperation
    {
        static string folder_capture = "game-captures";
        public MyDm dm = new MyDm();
        public GameSetting setting = new GameSetting();
        UserInfoBase user;
        Fuhun fuhun;
        Fight fight;
        KuafuBoss kuafuBoss;
        public GameOperation(UserInfoBase user)
        {
            this.user = user;
            fuhun = new Fuhun(this);
            fight = new Fight(this);
            kuafuBoss = new KuafuBoss(this);
            if (!Directory.Exists(folder_capture))
            {
                Directory.CreateDirectory(folder_capture);
            }
        }

        public void shout(string str)
        {
            try
            {
                var p = waitForImage("send", 100);
                click(p.X - 100, p.Y + 6);
                Thread.Sleep(100);
                dm.SendString2(hwnd, str);
                Thread.Sleep(100);
                click(p.X, p.Y);
            }
            catch (Exception)
            {

            }
        }
        int hwnd = 0;

        public void clear()
        {
            send_key(Keys.Escape, 3);
        }
        public void setState(string state)
        {
            StateChange(stateTitle + "-" + state);
        }
        public void locate(string dest, bool auto_trans = false)
        {
            setState("寻找坐标中");
            clear();
            var p = new Point();
            while (p.X <= 0)
            {
                dm.KeyPress(Keys.M);
                Thread.Sleep(1000);
                p = findImage("自动寻路|自动寻路2");
            }
            setState("寻路中");
            var arr = dest.Split(new string[] { ",", "，" }, StringSplitOptions.None);
            dblclick(p.X + 120, p.Y - 8);
            //await Task.Delay(30);
            //await send_chars(new char[] { (char)8, (char)8, (char)8 });
            //await Task.Delay(30);
            dm.SendString(hwnd, arr[0]);
            Thread.Sleep(60);
            //await Task.Delay(100);
            dblclick(p.X + 120, p.Y + 26);
            //await Task.Delay(30);
            //await send_chars(new char[] { (char)8, (char)8, (char)8 });
            //await Task.Delay(30);
            dm.SendString(hwnd, arr[1]);
            //send_normal_string(arr[1]);
            Thread.Sleep(60);
            click(p.X, p.Y);
            if (auto_trans)
            {
                var x1 = p.X - 450;
                var y1 = p.Y - 450;
                var x2 = p.X + 200;
                var y2 = p.Y;
                Thread.Sleep(1000);
                p = findImage("flag", x1, y1, x2, y2);
                click(p.X + 1, p.Y + 17);
                Thread.Sleep(1000);
                p = findImage("cloud", x1, y1, x2, y2);
                click(p.X + 4, p.Y + 4);
            }
            dm.KeyPress(Keys.Escape);
            Thread.Sleep(2000);
            waitCompleteLocate();
            setState("寻路完成");
        }

        public void waitCompleteLocate()
        {
            var p = findImage("tr-corner");
            var x = 5;
            var y = 5;
            var len = x * y;
            var prev_data = new string[len];
            while (true)
            {
                var current_data = new string[len];
                var is_dirty = false;
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        var index = i * y + j;
                        current_data[index] = dm.GetColor(p.X - (x + 1) * 10, p.Y + (y + 1) * 10);
                        if (current_data[index] != prev_data[index])
                        {
                            is_dirty = true;
                        }
                    }
                }
                if (!is_dirty)
                {
                    break;
                }
                prev_data = current_data;
                Thread.Sleep(1000);
            }
        }

        void send_chars(char[] cs)
        {
            foreach (var item in cs)
            {
                dm.KeyPress((Keys)item);
                Thread.Sleep(20);
            }
        }
        public void send_key(Keys key, int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                dm.KeyPress(key);
                Thread.Sleep(20);
            }
        }

        void SelectRole()
        {
            setState("进入游戏中");
            try
            {
                if (user.RoleIndex == 0)
                {
                    while (true)
                    {
                        if (dm.GetColor(50, 50) != "000000")
                        {
                            break;
                        }
                        var p = findImage("进入游戏");
                        if (p.X > -1)
                        {
                            Thread.Sleep(1500);
                            dblclick(p.X, p.Y);
                        }

                    }

                }
                else
                {
                    var p = waitForImage("角色选择");
                    Thread.Sleep(1500);
                    dblclick(p.X, p.Y + 90 * (user.RoleIndex + 1));
                    while (dm.GetColor(50, 50) == "000000")
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
            setState("游戏中");
            click(17, 61);
        }

        public event Action<string> StateChange;
        public event Action<string, string> DataEvent;
        public void fangjineng()
        {
            for (int i = 1; i < 10; i++)
            {
                dm.KeyPress((Keys)(48 + i));
                Thread.Sleep(30);
            }
        }
        public Point waitForImage(string src, int delay = 500, int x1 = 0, int y1 = 0, int x2 = 2000, int y2 = 2000, int dir = 0)
        {
            while (true)
            {
                var p = findImage(src, x1, y1, x2, y2, dir);
                if (p.X > 0)
                {
                    return p;
                }
                Thread.Sleep(delay);
            }
        }
        public Point waitForMap(string src, int delay = 500)
        {
            while (true)
            {
                var p = findMap(src);
                if (p.X > 0)
                {
                    return p;
                }
                Thread.Sleep(delay);
            }
        }
        public Point findMap(string str)
        {
            return findImage(str, 500, 0, 2000, 30);
        }
        public Point findImage(string str, int x1 = 0, int y1 = 0, int x2 = 2000, int y2 = 2000, int dir = 0)
        {
            var s = str.Split(new char[] { '|' }).Select(src => String.Format("resources/{0}.bmp", src));
            return dm.FindPic(x1, y1, x2, y2, string.Join("|", s.ToArray()), dir);

        }
        public Point findColor(string src)
        {
            return dm.FindColor(0, 0, 2000, 2000, src);

        }

        public void click(int x, int y)
        {
            dm.MoveTo(x, y);
            dm.LeftClick();
        }

        public void dblclick(int x, int y)
        {
            dm.MoveTo(x, y);
            dm.LeftDoubleClick();
        }
        void init_window(int h_1)
        {
            setState("寻找游戏窗口");
            //Thread.Sleep(3000);
            var h_2 = dm.FindWindowEx(h_1, "Shell Embedding", "");
            var h_3 = dm.FindWindowEx(h_2, "Shell DocObject View", "");
            var h_4 = dm.FindWindowEx(h_3, "Internet Explorer_Server", "");
            var hwnd = dm.FindWindowEx(h_4, "MacromediaFlashPlayerActiveX", "");
            if (hwnd <= 0)
            {
                MessageBox.Show("游戏未找到,请刷新试试");
                throw new Exception("游戏未找到");
            }

            this.hwnd = hwnd;
            var r = dm.BindWindowEx(hwnd, "gdi",
                "dx.mouse.position.lock.api|dx.mouse.clip.lock.api|dx.mouse.input.lock.api|dx.mouse.state.api|dx.mouse.api|dx.mouse.cursor"
                //"windows"
                );
            setState("游戏中");
            DataEvent("enter", "");
            if (setting.AutoSelectRole)
            {
                SelectRole();
            }
        }

        void screenshot(string name)
        {
            Capture(0, 0, 2000, 2000, name);
        }

        public string Capture(int x1, int y1, int x2, int y2, string name = "")
        {
            if (string.IsNullOrEmpty(user.ID))
            {
                name = user.ID;
            }
            string path = string.Format("{0}/{1}.bmp", folder_capture, name);
            dm.Capture(x1, y1, x2, y2, path);
            return path;
        }

        public void Abort()
        {
            killTask();
        }

        void killTask()
        {
            if (thread != null)
            {
                try
                {
                    thread.Abort();
                }
                catch (Exception)
                {
                }
            }
        }

        string stateTitle = "";
        Thread thread;
        public void operate(string type, string msg = "")
        {
            ThreadStart action = null;
            stateTitle = "";
            if (type == "init")
            {
                if (hwnd > 0)
                {
                    unbind();
                }
                action = () =>
                {
                    try
                    {
                        Thread.Sleep(3000);
                        init_window(int.Parse(msg));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                };
            }
            else if (type == "locate")
            {
                action = () =>
                {
                    locate(msg, setting.AutoTrans);
                };
            }
            else if (type == "shout")
            {
                action = () =>
                {
                    shout(msg);
                };
            }
            else if (type == "pause")
            {
                action = () =>
                {
                    setState("已暂停");
                };
            }
            else if (type == "screenshot")
            {
                action = () =>
                {
                    screenshot(new Random().Next() + ".bmp");
                };
            }
            else if (type == "fuhun-init")
            {
                stateTitle = "附魂";
                action = () =>
                {
                    setState("报名中");
                    fuhun.Sign();
                    setState("准备竞技");
                    //findMap("中立-附魂战场");
                    DataEvent?.Invoke("fuhun-capture", fuhun.GetCapture());
                    setState("标记敌人中");
                    //DataEvent?.Invoke("fuhun-capture", @"E:\Users\Administrator\source\repos\WpfApp1\MyTool\bin\Debug\debug\fuhun\637159218502426004.bmp");
                };
            }
            else if (type == "fuhun-tag")
            {
                stateTitle = "附魂";
                action = () =>
                {
                    setState("PK中");
                    fuhun.PK(msg);
                    DataEvent?.Invoke("fuhun-end", "");
                };
            }
            else if (type == "fight")
            {
                stateTitle = "打架";
                action = () => fight.Action(msg);
            }
            else if (type == "kuafu-boss")
            {
                stateTitle = "跨服boss";
                action = () => kuafuBoss.Action(msg);
            }
            else if (type == "game-setting")
            {
                setting = JsonConvert.DeserializeObject<GameSetting>(msg);
            }
            if (action != null)
            {
                killTask();
                thread = new Thread(action);
                thread.Start();
            }
        }

        public void unbind()
        {
            if (hwnd > 0)
            {
                dm.UnBindWindow();
            }
        }
        ~GameOperation()
        {
            unbind();
            Abort();
        }
    }
}
