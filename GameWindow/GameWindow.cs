using OdyLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Collections;
using GameWindow.entities;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GameWindow
{
    public partial class GameWindow : Form
    {
        List<GameHandler> handlers = new List<GameHandler>();
        public GameWindow()
        {
            InitializeComponent();
            //MinimizeBox = false;
        }
        void AddPage(UserInfoBase user)
        {
            var tabpage = new TabPage(user.Nickname + "-" + user.Name);
            var wb = new WebBrowser();
            wb.Dock = DockStyle.Fill;
            tabpage.Controls.Add(wb);
            //wb.Url = new Uri("http://frmmo.wan.360.cn/game_login.php?server_id=S2&src=360wan-2jxx-frmmo");
            wb.Url = new Uri(user.AreaValue);
            wb.ScriptErrorsSuppressed = true;
            tab.TabPages.Add(tabpage);

            var externaljs = new ExternalJS();
            //externaljs.OnReceived += Externaljs_OnReceived;
            wb.ObjectForScripting = externaljs;
            externaljs.OnLogined += Externaljs_OnLogined;
            tab.SelectedIndex = tab.TabPages.Count - 1;

            var handler = new GameHandler(wb, user, serverProvider);
            handlers.Add(handler);
        }
        OneServiceRemoteProvider serverProvider;

        public GameWindow(OneServiceRemoteProvider serverProvider, UserInfoBase user) : this()
        {
#if !DEBUG
            SuppressWininetBehavior();
#endif
            this.serverProvider = serverProvider;
            this.FormClosing += Form1_FormClosing;
            //serverProvider.SendMsg(ServiceRemoteType.InitClient, guid);
            InitRemoteProvider(user);
            AddPage(user);
            //AddPage(user);
            this.Text = user.Nickname + "-" + user.Name;

            //var tabpage = new TabPage("user.Nickname");
            //var wb = new WebBrowser();
            //wb.Dock = DockStyle.Fill;
            //tabpage.Controls.Add(wb);
            ////wb.Url = new Uri("http://frmmo.wan.360.cn/game_login.php?server_id=S2&src=360wan-2jxx-frmmo");
            //wb.Url = new Uri(user.AreaValue);
            //wb.ScriptErrorsSuppressed = true;
            //tab.TabPages.Add(tabpage);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var item in handlers)
            {
                item.Destroy();
            }
            try
            {
                serverProvider.SendMsg("close");
            }
            catch (Exception)
            {
            }
        }

        RemoteService remoteService;
        void InitRemoteProvider(UserInfoBase user)
        {
            var uuid = Guid.NewGuid().ToString();
            remoteService = new RemoteService(uuid, "client");
            remoteService.remoteProvider.OnReceivedMsg += RemoteProvider_OnReceivedMsg;
            serverProvider.SendMsg("client-init", user.GameHwnd, uuid);
        }
        private void RemoteProvider_OnReceivedMsg(string type, string name, string value)
        {
            var handler = new Action(() =>
            {
                if (type == "new-user")
                {
                    AddPage(JsonConvert.DeserializeObject<UserInfoBase>(value));
                    return;
                }
                //if (type == "close" && string.IsNullOrEmpty(name))
                //{
                //    this.Close();
                //    return;
                //}

                for (int i = 0; i < handlers.Count; i++)
                {
                    var item = handlers[i];
                    if (item.user.GameHwnd == name)
                    {
                        try
                        {
                            //if (type == ServiceRemoteType.ResetWindowName)
                            //{
                            //    this.Text = user.Nickname + "-" + user.Name;
                            //}
                            //else 
                            if (type == "close")
                            {
                                //tab.TabPages.Remove(tab.TabPages[i]);
                                //item.Destroy();
                                //handlers.Remove(item);
                                //serverProvider.SendMsg("game-exit", item.user.GameHwnd);
                                this.Close();
                            }
                            else if (type == "focus")
                            {
                                this.Show();
                                this.BringToFront();
                                this.Activate();
                                tab.SelectedIndex = i;
                                //Left = prev_position.X;
                                //Top = prev_position.Y;
                                //is_hidden = false;
                            }
                            else if (type == "refresh")
                            {
                                item.Refresh();
                                tab.SelectedIndex = i;
                            }
                            else
                            {
                                item.gop.operate(type, value);
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        return;
                    }
                }
            });
            if (this.InvokeRequired)
            {
                this.Invoke(handler);
                return;
            }
            handler.Invoke();
        }

        //[DllImportAttribute("gdi32.dll")]
        //private static extern int BitBlt(
        //  IntPtr hdcDest,     // handle to destination DC (device context)
        //  int nXDest,         // x-coord of destination upper-left corner
        //  int nYDest,         // y-coord of destination upper-left corner
        //  int nWidth,         // width of destination rectangle
        //  int nHeight,        // height of destination rectangle
        //  IntPtr hdcSrc,      // handle to source DC
        //  int nXSrc,          // x-coordinate of source upper-left corner
        //  int nYSrc,          // y-coordinate of source upper-left corner
        //  System.Int32 dwRop  // raster operation code
        //);
        //[DllImportAttribute("user32.dll")]
        //private static extern int GetWindowDC(IntPtr hwnd);

        private void Externaljs_OnLogined()
        {
            //serverProvider.SendMsg("cookie", user.GameHwnd, wb.Document.Cookie);
        }
        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("wininet.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetOption(int hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        private unsafe void SuppressWininetBehavior()
        {
            /* SOURCE: http://msdn.microsoft.com/en-us/library/windows/desktop/aa385328%28v=vs.85%29.aspx
            * INTERNET_OPTION_SUPPRESS_BEHAVIOR (81):
            *      A general purpose option that is used to suppress behaviors on a process-wide basis. 
            *      The lpBuffer parameter of the function must be a pointer to a DWORD containing the specific behavior to suppress. 
            *      This option cannot be queried with InternetQueryOption. 
            *      
            * INTERNET_SUPPRESS_COOKIE_PERSIST (3):
            *      Suppresses the persistence of cookies, even if the server has specified them as persistent.
            *      Version:  Requires Internet Explorer 8.0 or later.
            */


            int option = (int)3/* INTERNET_SUPPRESS_COOKIE_PERSIST*/;
            int* optionPtr = &option;

            bool success = InternetSetOption(0, 81/*INTERNET_OPTION_SUPPRESS_BEHAVIOR*/, new IntPtr(optionPtr), sizeof(int));
            if (!success)
            {
                MessageBox.Show("Something went wrong !>?");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
    }
}
