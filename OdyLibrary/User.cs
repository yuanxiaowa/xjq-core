using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdyLibrary
{
    [Serializable]
    public class UserInfoBase
    {
        public string ID { get; set; }
        public string GameHwnd { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string AreaName { get; set; }
        public string AreaValue { get; set; }
        public string Cookie { get; set; }
        public int RoleIndex { get; set; }

        public UserInfoBase() { }
        public UserInfoBase(UserInfoBase user)
        {
            ID = new Random().Next() + "";
            Name = user.Name;
            Nickname = user.Nickname;
            Password = user.Password;
            AreaName = user.AreaName;
            AreaValue = user.AreaValue;
            RoleIndex = user.RoleIndex;
        }
        public void Assign(UserInfoBase user)
        {
            Name = user.Name;
            Password = user.Password;
            Nickname = user.Nickname;
            AreaName = user.AreaName;
            AreaValue = user.AreaValue;
            RoleIndex = user.RoleIndex;
        }
        public static T FromUserBase<T>(UserInfoBase user) where T : UserInfoBase
        {
            var t = System.Activator.CreateInstance<T>();
            t.ID = user.ID;
            t.Name = user.Name;
            t.Nickname = user.Nickname;
            t.Password = user.Password;
            t.AreaName = user.AreaName;
            t.AreaValue = user.AreaValue;
            t.RoleIndex = user.RoleIndex;
            return t;
        }
    }

}
