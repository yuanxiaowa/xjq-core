using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameWindow.entities
{
    class KuafuBoss
    {
        GameOperation gop;
        public KuafuBoss(GameOperation gop)
        {
            this.gop = gop;
        }

        public void Action(string msg)
        {
            var revive_img = gop.setting.ReviveWay != 1 ? "复活点复活|复活点复活2" : "原地复活";
            while (true)
            {
                gop.clear();
                gop.dm.KeyPress(Keys.M);
                Thread.Sleep(1000);
                var p = gop.findImage("跨服boss一层");
                if (p.X > -1)
                {
                    gop.setState("进入跨服一层");
                    p = gop.findImage("dot");
                    gop.click(p.X, p.Y);
                    gop.waitCompleteLocate();
                }
                p = gop.findImage("跨服boss二层");
                if (p.X > -1)
                {
                    gop.setState("进入跨服二层");
                    p = gop.findImage("dot");
                    gop.click(p.X, p.Y);
                    p = gop.waitForImage("是");
                    gop.click(p.X, p.Y);
                }
                gop.setState("等待进入虚宁洞天");
                gop.waitForImage("虚宁洞天");
                gop.setState("寻路到目的地");
                gop.locate(msg);
                while (true)
                {
                    gop.setState("攻击目标中");
                    gop.fangjineng();
                    p = gop.findImage(revive_img);
                    if (p.X > -1)
                    {
                        gop.setState("已死亡，复活中");
                        Thread.Sleep(500);
                        gop.click(p.X, p.Y);
                        Thread.Sleep(1000);
                        break;
                    }
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
