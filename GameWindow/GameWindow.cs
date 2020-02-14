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
using CefSharp;
using CefSharp.WinForms;

namespace GameWindow
{
    public partial class GameWindow : Form
    {
        List<CefGameHandler> handlers = new List<CefGameHandler>();
        public GameWindow()
        {
            InitializeComponent();
            MinimizeBox = false;
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;
            var settings = new CefSettings()
            {
                AcceptLanguageList = "zh-CN",
                //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
            };
            //Example of setting a command line argument
            //Enables WebRTC
            //settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.CefCommandLineArgs["enable-system-flash"] = "1";
            settings.CefCommandLineArgs["enable-npapi"] = "1";
            settings.CefCommandLineArgs["plugin-policy"] = "allow";

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

        }
        void AddPage(UserInfoBase user)
        {
            var tabpage = new TabPage(user.Nickname + "-" + user.Name);
            var wb = new ChromiumWebBrowser();
            wb.Dock = DockStyle.Fill;
            tabpage.Controls.Add(wb);

            ////wb.Url = new Uri("http://frmmo.wan.360.cn/game_login.php?server_id=S2&src=360wan-2jxx-frmmo");
            //wb.Url = new Uri(user.AreaValue);
            //wb.ScriptErrorsSuppressed = true;
            tab.TabPages.Add(tabpage);
            //var externaljs = new ExternalJS();
            ////externaljs.OnReceived += Externaljs_OnReceived;
            //wb.ObjectForScripting = externaljs;
            //externaljs.OnLogined += Externaljs_OnLogined;
            //tab.SelectedIndex = tab.TabPages.Count - 1;

            var handler = new CefGameHandler(wb, user, serverProvider);
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
            this.Text = user.Nickname + "-" + user.Name;
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

    }
}
