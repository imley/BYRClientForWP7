using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BYRClient.Models
{
    public class AttFile
    {
        public string small;
        public string middle;
        public string url;

        public string Name { get; set; }
        public string Url 
        {
            get { return url; } 
            set 
            {
                //url = value.Replace("api.byr.cn/attachment", "bbs.byr.cn/att");
                url = value + "?appkey=" + App.api.getApiKey();
            } 
        }
        public string Size { get; set; }
        public string Thumbnail_small
        {
            get { return small; }
            set
            {
                //small = value.Replace("api.byr.cn/attachment", "bbs.byr.cn/att");
                small = value + "?appkey=" + App.api.getApiKey();
            }
        }
        public string Thumbnail_middle
        {
            get { return middle; }
            set
            {
                //middle = value.Replace("api.byr.cn/attachment", "bbs.byr.cn/att");
                middle = value + "?appkey=" + App.api.getApiKey();
            }
        }
    }
}
