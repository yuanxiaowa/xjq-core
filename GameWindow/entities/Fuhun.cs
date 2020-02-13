using OdyLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameWindow.entities
{
    public class Fuhun
    {
        GameOperation gop;
        static string folder = "fuhun";
        static string enemy_names = "";
        static string font_file = "resources/font-enemies.txt";
        static Fuhun()
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var text = File.ReadAllText(font_file, Encoding.GetEncoding("gb2312"));
            var regex = new Regex(@"\$(.*?)\$");
            var mcs = regex.Matches(text);
            var names = mcs.Cast<Match>().Select(item => item.Groups[1].Value).ToArray();
            enemy_names = string.Join("|", names);
        }
        public Fuhun(GameOperation gop)
        {
            this.gop = gop;
            gop.dm.SetDict(0, font_file);
        }
        public void Sign()
        {
            gop.clear();
            var p = gop.findMap("剩余时间|中立-附魂战场");
            if (p.X > -1)
            {
                return;
            }
            p = gop.findMap("中立-附魂战场休息室");
            if (p.X <= 0)
            {
                p = gop.waitForImage("附魂-icon|附魂-icon2", 500, 300, 0, 2000, 200);
                gop.click(p.X, p.Y);
                Thread.Sleep(1000);
                p = gop.waitForImage("前往传送|立即前往");
                gop.click(p.X, p.Y);
                Thread.Sleep(1000);
                p = gop.waitForImage("进入|进入2");
                gop.click(p.X, p.Y);
                Thread.Sleep(1000);
                gop.send_key(Keys.Escape, 3);
            }
            p = gop.findImage("参加竞技|参加竞技2|取消竞技");
            if (p.X <= 0)
            {
                gop.locate("7,42");
                p = gop.waitForImage("附魂报名人物");
                gop.setState("点击npc报名");
                gop.dblclick(p.X, p.Y);
                gop.waitCompleteLocate();
                Thread.Sleep(1000);
            }
            while (true)
            {
                p = gop.findImage("参加竞技|参加竞技2");
                if (p.X > -1)
                {
                    gop.setState("报名");
                    gop.click(p.X, p.Y);
                }
                Thread.Sleep(1000);
                p = gop.findImage("取消竞技");
                if (p.X > -1)
                {
                    break;
                }
            }
            gop.setState("等待进入战场");
            while (true)
            {
                p = gop.findMap("剩余时间|中立-附魂战场");
                if (p.X > 0)
                {
                    Thread.Sleep(1000);
                    break;
                }
                p = gop.findImage("附魂进入|附魂进入2");
                if (p.X > 0)
                {
                    gop.click(p.X, p.Y);
                    Thread.Sleep(21000);
                    break;
                }
                Thread.Sleep(1000);
            }
        }

        public void PK(string indexes_str)
        {
            // 1272,197
            var indexes = new int[] { };
            if (!string.IsNullOrEmpty(indexes_str))
            {
                indexes = indexes_str.Split(new char[] { ',' }).Select(s => int.Parse(s)).ToArray();
            }
            Point p_result = new Point();
            try
            {
                if (indexes.Length == 0)
                {
                    gop.setState("无敌人，坐等收益");
                    gop.locate(string.Format("{0},{1}", 64, 106));

                    while (true)
                    {
                        var _p = gop.findImage("领取奖励|结算列表");
                        if (_p.X > -1)
                        {
                            p_result = _p;
                            break;
                        }
                        Thread.Sleep(3000);
                    }
                    return;
                }
                var p = gop.findImage("arrow1", 500, 190, 2000, 540);
                // 1088,243 
                // 1088,311 
                // 1088,379 
                // 1088,447
                // 1088,515 215657
                var xue_dead = "114b51";
                while (true)
                {
                    foreach (var i in indexes)
                    {
                        var x1 = p.X - 184;
                        var y1 = p.Y + 46 + i * 68;
                        if (gop.dm.GetColor(x1, y1) == xue_dead)
                        {
                            continue;
                        }
                        gop.click(x1, y1);
                        gop.setState("移动到目标" + (i + 1));
                        Thread.Sleep(1000);
                        gop.waitCompleteLocate();
                        gop.setState("攻击目标中");
                        while (gop.dm.GetColor(x1, y1) != xue_dead)
                        {
                            var p2 = gop.findImage("复活点复活|复活点复活2", 0, 200, 2000, 2000);
                            if (p2.X > -1)
                            {
                                gop.setState("已死亡，等待复活");
                                Thread.Sleep(500);
                                gop.click(p2.X, p2.Y);
                                Thread.Sleep(1000);
                                gop.setState("移动到目标" + (i + 1));
                                gop.click(x1, y1);
                                gop.waitCompleteLocate();
                            }
                            p2 = gop.findImage("领取奖励|结算列表");
                            if (p2.X > -1)
                            {
                                p_result = p2;
                                return;
                            }
                            gop.fangjineng();
                        }
                        gop.setState(string.Format("目标{0}已死亡，切换到下一目标", i + 1));
                    }
                    var _p = gop.findImage("领取奖励|结算列表");
                    if (_p.X > -1)
                    {
                        p_result = _p;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (p_result.X > 0)
                {
                    gop.setState("已结束，领取奖励");
                    gop.click(p_result.X, p_result.Y);
                    Thread.Sleep(10000);
                }
                else
                {
                    gop.setState("任务已终止");
                }
            }
        }

        public string GetCapture()
        {
            var p = gop.waitForImage("arrow1");
            var path = String.Format("{0}/{1}.bmp", folder, DateTime.Now.Ticks);
            var left = p.X - 203;
            var top = p.Y;
            gop.dm.Capture(left, top, p.X, p.Y + 331, path);
            var list = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                var x1 = 13 + left;
                var y1 = 9 + 68 * i + top;
                var point = gop.dm.FindStr(x1, y1, x1 + 157, y1 + 15, enemy_names, "f1ffb1-555555");
                if (point.X > -1)
                {
                    list.Add(i);
                }
            }
            if (list.Count > 0)
            {
                path += "?" + string.Join(",", list.ToArray());
            }
            return path;
        }
    }
}
