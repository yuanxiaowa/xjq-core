using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.entities;

namespace WpfApp1.utils
{
    class game
    {
        static List<NameValue> areas_of_360;
        static DateTime last_360;
        public static List<NameValue> Get360Areas()
        {
            if (areas_of_360 != null)
            {
                if (DateTime.Now - last_360 < TimeSpan.FromMinutes(10))
                {
                    return areas_of_360;
                }
            }
            var web = new HtmlWeb();
            var doc = web.Load("http://wan.360.cn/game/frmmo");
            last_360 = DateTime.Now;
            areas_of_360 = doc.DocumentNode.Descendants("a").Where(y => y.GetAttributeValue("class", "") == "area-tag").Select(item => new NameValue()
            {
                Name = new Regex("(?<=】).*|&nbsp;").Replace(item.GetAttributeValue("title", ""), ""),
                Value = item.GetAttributeValue("href", "")
            }).ToList();
            return areas_of_360;
        }
        static List<NameValue> areas_of_91wan;
        static DateTime last_91wan;
        public static List<NameValue> Get91wanAreas()
        {
            if (areas_of_91wan != null)
            {
                if (DateTime.Now - last_91wan < TimeSpan.FromMinutes(10))
                {
                    return areas_of_91wan;
                }
            }
            var web = new HtmlWeb();
            //var doc = web.Load("http://frxz2.91wan.com/list/");
            var doc = web.LoadFromBrowser("http://frxz2.91wan.com/list/");
            last_91wan = DateTime.Now;
            areas_of_91wan = doc.DocumentNode.Descendants("ul").Where(y => y.GetAttributeValue("id", "") == "server_all").First().Descendants("a").Select(item => new NameValue()
            {
                Name = new Regex("(?<=】).*|&nbsp;").Replace(item.LastChild.InnerText, ""),
                Value = item.GetAttributeValue("href", "")
            }).ToList();
            return areas_of_91wan;
        }
        static List<NameValue> areas_of_4399;
        static DateTime last_4399;
        public static List<NameValue> Get4399Areas()
        {
            if (areas_of_4399 != null)
            {
                if (DateTime.Now - last_4399 < TimeSpan.FromMinutes(10))
                {
                    return areas_of_4399;
                }
            }
            var web = new HtmlWeb();
            var doc = web.Load("http://frxz2.4399.com/select_server.html");
            last_4399 = DateTime.Now;
            areas_of_4399 = doc.DocumentNode.Descendants("ul").Where(y => y.GetAttributeValue("id", "") == "_all_server").First().Descendants("a").Select(item => new NameValue()
            {
                Name = new Regex("&nbsp;").Replace(item.FirstChild.InnerText, "").Trim(),
                Value = item.GetAttributeValue("href", "")
            }).ToList();
            return areas_of_4399;
        }
    }
}
