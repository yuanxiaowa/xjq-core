using System;
using System.Threading;

namespace WpfApp1.entities
{
    class Fight
    {
        GameOperation gop;
        public Fight(GameOperation gop)
        {
            this.gop = gop;
        }
        public void Action(string str)
        {
            gop.locate(str, gop.setting.AutoTrans);
            var revive_img = gop.setting.ReviveWay == 1 ? "复活点复活|复活点复活2" : "原地复活";
            var prev_time = DateTime.Now;
            while (true)
            {
                if (gop.setting.LocateInterval > 0)
                {
                    var now = DateTime.Now;
                    if (now - prev_time >= TimeSpan.FromSeconds(gop.setting.LocateInterval))
                    {
                        gop.setState("循环寻路中");
                        gop.locate(str, gop.setting.AutoTrans);
                        prev_time = now;
                    }
                }
                gop.setState("攻击目标中");
                gop.fangjineng();
                var p = gop.findImage(revive_img);
                if (p.X > -1)
                {
                    gop.setState("复活中");
                    gop.click(p.X, p.Y);
                    if (gop.setting.ReviveWay == 1)
                    {
                        gop.setState("准备移动到目标点");
                        Thread.Sleep(1000);
                        gop.locate(str, gop.setting.AutoTrans);
                    }
                }
            }
        }
    }
}
