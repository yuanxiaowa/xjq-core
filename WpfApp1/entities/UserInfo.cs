using OdyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.entities;

namespace WpfApp1.entities
{

    public class UserInfo : UserInfoBase
    {
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; } = true;
        /// <summary>
        /// 游戏状态
        /// </summary>
        public string State { get; set; } = "离线";
        /// <summary>
        /// 窗口状态
        /// </summary>
        public string WinState { get; set; } = "-";
        public GameOperation Provider { get; set; }
        public UserInfo()
        {
        }

        public UserInfo(UserInfoBase user) : base(user)
        {
        }

        public void Reset()
        {
            State = "离线";
            WinState = "-";
            GameHwnd = null;
            Provider = null;
        }
    }
}
