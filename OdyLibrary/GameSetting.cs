using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdyLibrary
{
    [Serializable]
    public class GameSetting
    {
        /// <summary>
        /// 自动传送
        /// </summary>
        public bool AutoTrans { get; set; }
        public bool AutoSelectRole { get; set; }
        /// <summary>
        /// 复活方式
        /// </summary>
        public int ReviveWay { get; set; }
        public int LocateInterval { get; set; }
    }
}
