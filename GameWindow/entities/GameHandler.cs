using mshtml;
using OdyLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameWindow.entities
{
    class GameHandler
    {
        public UserInfoBase user;
        public GameOperation gop;
        WebBrowser wb;
        OneServiceRemoteProvider serverProvider;
        //bool is_hidden = false;

        Hashtable url_map = new Hashtable();
        public GameHandler(WebBrowser wb, UserInfoBase user, OneServiceRemoteProvider serverProvider)
        {
            this.wb = wb;
            this.user = user;
            this.serverProvider = serverProvider;
            gop = new GameOperation(user);
            wb.DocumentCompleted += wb_DocumentCompleted;
            //wb.Url = new Uri(user.AreaValue);
            wb.Url = new Uri(user.AreaValue);
            //ShowInTaskbar = false;
            gop.StateChange += Gop_OnStateChange;
            gop.DataEvent += Gop_DataEvent;
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
            wb.Url = new Uri(user.AreaValue);
            gop.operate("init");
        }

        int login_state = 0;
        private async void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //var host = new Uri(user.AreaValue).Host;
            if (e.Url.Host.EndsWith(".1360.com") || e.Url.Host.EndsWith(".frxz2.91wan.com") || e.Url.Host.EndsWith(".my4399.com"))
            {
                login_state = 1;
                serverProvider.SendMsg("game-start", user.GameHwnd);
                Gop_OnStateChange("在线");
                gop.operate("init", wb.Handle + "");
            }
            else
            {
                if (login_state == 1)
                {
                    //if (gop != null)
                    //{
                    //    gop.Abort();
                    //}
                    return;
                }
                string code;
                var is91 = e.Url.ToString().Contains("91wan.com/user/game_login.php");
                var is4399 = e.Url.ToString().Contains("web.4399.com/user/login_game.php");
                if (is91)
                {
                    code = File.ReadAllText("resources/enter91.js");
                }
                else if (is4399)
                {
                    code = File.ReadAllText("resources/enter4399.js");
                }
                else
                {
                    code = File.ReadAllText("resources/enter.js");
                }
                IHTMLWindow2 win = wb.Document.Window.DomWindow as IHTMLWindow2;
                //var win = (IHTMLWindow2)doc.parentWindow;
                code = code.Replace("{{Name}}", user.Name).Replace("{{Password}}", user.Password).Replace("{{AreaValue}}", user.AreaValue);
                //MessageBox.Show(code);
                while (login_state == 0 && wb.Url.AbsoluteUri == e.Url.AbsoluteUri)
                {
                    //this.Text = new Random().Next().ToString();
                    //await Task.Delay(1000);
                    try
                    {
                        win.execScript(code, "javascript");
                        if (is91 || is4399)
                        {
                            return;
                        }
                    }
                    catch (Exception)
                    {
                    }
                    await Task.Delay(3000);
                }
            }
        }


        private void Gop_OnStateChange(string state)
        {
            serverProvider.SendMsg("state-change", user.GameHwnd, state);
        }

        private void wb_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //FieldInfo fi = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            //if (fi != null)
            //{
            //    object browser = fi.GetValue(wb);
            //    if (browser != null)
            //        browser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, browser, new object[] { true });
            //}
        }

        ~GameHandler()
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

    //[ComVisible(true)]
    //[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    //public interface IApp
    //{
    //    void procMessage();
    //}

    //[ComVisible(true)]
    //[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    //public interface ITestObject
    //{
    //    IApp App { get; }
    //}

    //[ComVisible(true)]
    //[ClassInterface(ClassInterfaceType.None)]
    //[ComDefaultInterface(typeof(ITestObject))]
    //public class TestObject : ITestObject
    //{
    //    readonly ExternalJS _app = new ExternalJS();

    //    public IApp App
    //    {
    //        get { return _app; }
    //    }
    //}

    [ComVisible(true)]
    public class ExternalJS // : IApp
    {
        //public delegate void MsgDeletegate(string str);
        public event Action OnLogined;

        public void end_login()
        {
            OnLogined();
            //OnReceived(str);
        }
    }
}
