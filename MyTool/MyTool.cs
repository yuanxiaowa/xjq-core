using MyTool.entities;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using OdyLibrary;

namespace MyTool
{
    public partial class MyTool : Form
    {
        MyDm dm = new MyDm();
        public MyTool()
        {
            InitializeComponent();
            //helper.NewMouseMessage += (sender, e) =>
            //    Debug.WriteLine($"{e.MessageType}, x: {e.Position.x}, y: {e.Position.y}");
            //helper.NewKeyboardMessage += (sender, e) =>
            //    Debug.WriteLine($"{e.MessageType}, VirtKeyCode: {e.VirtKeyCode}");

            // Do something ...
            //helper.UninstallHooks();
            var t1 = new MouseMoveTool(ctrl_win_id);
            t1.MouseMove += T1_MouseMove;
            new MouseMoveTool(ctrl_start_pick, ctrl_start_pos).MouseMove += T2_MouseMove;
            new MouseMoveTool(ctrl_end_pick, ctrl_end_pos).MouseMove += T2_MouseMove;
            new MouseMoveTool(ctrl_pos_pick, ctrl_pos).MouseMove += T2_MouseMove;
        }

        private void T2_MouseMove(object sender, MouseEventArgs e)
        {
            var ctrl = sender as TextBox;
            var x = e.X;
            var y = e.Y;
            //dm.GetCursorPos(out x, out y);
            if (hwnd > 0)
            {
                var r = dm.GetWindowRect(hwnd);
                x -= r.X;
                y -= r.Y;
            }
            ctrl.Text = x + "," + y;
        }

        private void T1_MouseMove(object sender, MouseEventArgs e)
        {

            this.ctrl_win_id.Text = dm.GetMousePointWindow().ToString();
        }


        private void ctrl_file_path_DoubleClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ctrl_file_path.Text = openFileDialog1.FileName;
            }
        }

        int hwnd = 0;


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            dm.UnBindWindow();
        }

        private void ctrl_bind_Click(object sender, EventArgs e)
        {
            if (hwnd > 0)
            {
                dm.UnBindWindow();
                hwnd = 0;
                ctrl_bind.Text = "绑定";
            }
            else
            {
                var hwnd_str = ctrl_win_id.Text;
                if (String.IsNullOrEmpty(hwnd_str))
                {
                    return;
                }
                var _hwnd = Int32.Parse(hwnd_str);
                var r = dm.BindWindow(_hwnd, "gdi", "dx.mouse.position.lock.api|dx.mouse.position.lock.message|dx.mouse.clip.lock.api|dx.mouse.input.lock.api|dx.mouse.state.api|dx.mouse.api|dx.mouse.cursor");
                if (r == 1)
                {
                    hwnd = _hwnd;
                    ctrl_bind.Text = "解绑";
                }
                else
                {
                    MessageBox.Show("绑定失败");
                }
            }
        }

        private void ctrl_find_img_Click(object sender, EventArgs e)
        {
            var p1 = getPoint(ctrl_start_pos.Text);
            var p2 = getPoint(ctrl_end_pos.Text);
            var path = ctrl_file_path.Text.Trim();
            if (String.IsNullOrEmpty(path))
            {
                return;
            }
            var p = dm.FindPic(p1.X, p1.Y, p2.X, p2.Y, path, 0);
            if (p.X > 0)
            {
                AddLog("图片位置在：" + p.X + "," + p.Y);
            }
            else
            {
                AddLog("未找到图片");
            }
        }
        void AddLog(string msg)
        {
            ctrl_log.AppendText("\n" + DateTime.Now.ToString("HH:mm:ss") + ": " + msg);
            ctrl_log.ScrollToCaret();
        }

        private void ctrl_capture_Click(object sender, EventArgs e)
        {
            var p1 = getPoint(ctrl_start_pos.Text);
            var p2 = getPoint(ctrl_end_pos.Text);
            var folder = "captures";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var path = folder + "/" + DateTime.Now.Ticks + ".bmp";
            var r = dm.Capture(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y), path);
            if (r == 1)
            {
                AddLog("截图保存于 " + path);
            }
            else
            {
                AddLog("截图保存失败");
            }
        }

        int last_key = 0;
        private void ctrl_key_KeyDown(object sender, KeyEventArgs e)
        {
            ctrl_key.Text = e.KeyCode + "";
            last_key = e.KeyValue;
        }

        private void ctrl_btn_click_Click(object sender, EventArgs e)
        {
            var p = getPoint(ctrl_pos.Text);
            dm.MoveTo(p.X, p.Y);
            dm.LeftClick();
        }

        System.Drawing.Point getPoint(string str)
        {
            var r = str.Split(new char[] { ',', '，' });
            return new System.Drawing.Point(int.Parse(r[0]), int.Parse(r[1]));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var p = getPoint(ctrl_pos.Text);
            dm.MoveTo(p.X, p.Y);
            dm.RightClick();
        }

        private void ctrl_btn_key_Click(object sender, EventArgs e)
        {
            dm.KeyPress((Keys)last_key);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                dm.KeyPress((Keys)(48 + i));
                await Task.Delay(20);
            }
            dm.KeyPress((Keys)48);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var p = dm.FindPic(0, 0, 2000, 2000, "resources/arrow1.bmp");
            if (p.X > 0)
            {
                var folder = "fuhun";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var path = String.Format("{0}/{1}.bmp", folder, DateTime.Now.Ticks);
                dm.Capture(p.X - 203, p.Y, p.X, p.Y + 331, path);
                AddLog("附魂截图保存于 " + path);
            }
            else
            {
                AddLog("截图失败");
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                dm.KeyPress(Keys.Escape);
                await Task.Delay(20);
            }
            var px = getPoint(tb_cood.Text);
            dm.KeyPress(Keys.M);
            await Task.Delay(1000);
            var p = dm.FindPic(0, 0, 2000, 2000, "resources/自动寻路.bmp|resources/自动寻路2.bmp");
            dm.MoveTo(p.X + 120, p.Y - 8);
            dm.LeftDoubleClick();
            dm.SendString(hwnd, px.X + "");
            await Task.Delay(60);
            dm.MoveTo(p.X + 120, p.Y + 26);
            dm.LeftDoubleClick();
            dm.SendString(hwnd, px.Y + "");
            await Task.Delay(60);
            dm.MoveTo(p.X, p.Y);
            dm.LeftClick();
            if (chb_direct_fly.Checked)
            {
                var x1 = p.X - 450;
                var y1 = p.Y - 450;
                var x2 = p.X + 200;
                var y2 = p.Y;
                await Task.Delay(1000);
                p = dm.FindPic(x1, y1, x2, y2, "resources/flag.bmp");
                dm.MoveTo(p.X + 1, p.Y + 17);
                dm.LeftClick();
                await Task.Delay(1000);
                p = dm.FindPic(x1, y1, x2, y2, "resources/cloud.bmp");
                dm.MoveTo(p.X + 4, p.Y + 4);
                dm.LeftClick();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var p = dm.FindPic(0, 0, 2000, 2000, "resources/arrow1.bmp");
            if (p.X > -1)
            {
                var str = "";
                for (int i = 0; i < 5; i++)
                {
                    var x1 = p.X - 184;
                    var y1 = p.Y + 46 + i * 68;
                    str += dm.GetColor(x1, y1) == "114b51" ? "0" : "1";
                    //str += dm.GetColor(x1, y1)+ " ";
                }
                AddLog("检测结果 " + str);
            }
            else
            {
                AddLog("未检测到结果");
            }
        }

        /// <summary>
        /// 查找文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            var p1 = getPoint(ctrl_start_pos.Text);
            var p2 = getPoint(ctrl_end_pos.Text);
            var p = dm.FindStr(p1.X, p1.Y, p2.X, p2.Y, txt_str.Text, txt_color.Text);
            if (p.X > 0)
            {
                AddLog(string.Format("文字 {0} 位于 {1},{2}", txt_str.Text, p.X, p.Y));
            }
            else
            {
                AddLog(string.Format("未找到文字 {0}", txt_str.Text));
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            FontTool.CleanImage();
            AddLog("清理成功");
        }
        private void button9_Click(object sender, EventArgs e)
        {
            var mask_str = FontTool.CheckEnemy(dm);
            if (mask_str == null)
            {
                AddLog("未检测到结果");
                return;
            }
            AddLog("检测结果为: " + mask_str);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FontTool.FilterFontImage();
        }
    }
}
