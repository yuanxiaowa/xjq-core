using CefSharp.WinForms;
using OdyLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameWindow.entities
{
    class CefGameHandler
    {
        public UserInfoBase user;
        public GameOperation gop;
        ChromiumWebBrowser wb;
        OneServiceRemoteProvider serverProvider;
        int hwnd = 0;
        //bool is_hidden = false;

        Hashtable url_map = new Hashtable();
        public CefGameHandler(ChromiumWebBrowser wb, UserInfoBase user, OneServiceRemoteProvider serverProvider)
        {
            this.wb = wb;
            this.user = user;
            hwnd = (int)wb.Handle;
            this.serverProvider = serverProvider;
            gop = new GameOperation(user);
            wb.Load(user.AreaValue);
            gop.StateChange += Gop_OnStateChange;
            gop.DataEvent += Gop_DataEvent;
            wb.FrameLoadEnd += Wb_FrameLoadEnd;
        }

        private void Gop_DataEvent(string arg1, string arg2)
        {
            if (arg1 == "enter")
            {
            }
            else if (arg1 == "fuhun-capture")
            {
                serverProvider.SendMsg(arg1, user.GameHwnd, arg2);
            }
            else if (arg1 == "fuhun-end")
            {
                var now = DateTime.Now;
                if (now.Hour == 19)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(100);
                        gop.operate("fuhun-init");
                    });
                }
            }
        }

        public void Refresh()
        {
            wb.Load(user.AreaValue);
            gop.operate("init");
        }

        int login_state = 0;

        private void Wb_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            var url = e.Url;
            var uri = new Uri(url);
            if (uri.Host.EndsWith(".1360.com") || uri.Host.EndsWith(".frxz2.91wan.com") || uri.Host.EndsWith(".my4399.com"))
            {
                login_state = 1;

                Task.Run(async() =>
                {
                    await Task.Delay(5000);
                    serverProvider.SendMsg("game-start", user.GameHwnd);
                    Gop_OnStateChange("在线");
                    gop.operate("init", hwnd + "");
                });
            }
        }


        private void Gop_OnStateChange(string state)
        {
            serverProvider.SendMsg("state-change", user.GameHwnd, state);
        }

        ~CefGameHandler()
        {
            Destroy();
        }

        public void Destroy()
        {
            //this.parent.remove(this, this.id);
            try
            {
                if (gop != null)
                {
                    gop.Abort();
                }
                serverProvider.SendMsg("game-exit", user.GameHwnd);
            }
            catch (Exception)
            {
            }
        }
    }
}
