using Newtonsoft.Json;
using OdyLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.entities;

namespace WpfApp.utils
{
    class config
    {
        static string filename = "users.json";
        public static List<UserInfoBase> loadUsers()
        {
            if (File.Exists(filename))
            {
                var text = File.ReadAllText(filename);
                var rb = JsonConvert.DeserializeObject<UserInfoBase[]>(text);
                return rb.ToList();
            }
            return new List<UserInfoBase>();
        }

        public static void saveUsers(List<UserInfoBase> users)
        {
            File.WriteAllText("users.json", JsonConvert.SerializeObject(users));
        }
    }
}
