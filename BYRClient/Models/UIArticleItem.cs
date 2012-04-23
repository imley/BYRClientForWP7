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
    public class UIArticleItem : UIItem
    {
        private string color = "LightBlue";
        private Article article;

        public Article Article
        {
            get
            {
                return article;
            }
            set
            {
                if (value != article)
                {
                    article = value;
                    NotifyPropertyChanged("Article");
                }
            }
        }

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
