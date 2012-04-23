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
using System.ComponentModel;

namespace BYRClient.Models
{
    public class UISectionItem : UIItem
    {
        private string type;
        private string color;

        public string DisplayName { get; set; }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value == "section")
                {
                    type = value;
                    Color = "LightSeaGreen";
                }
                else
                {
                    type = "board";
                    Color = "LightSkyBlue";
                }
            }
        }
        public string Id { get; set; }
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                if (value != color)
                {
                    color = value;
                    NotifyPropertyChanged("Color");
                }
            }
        }
    }
}
