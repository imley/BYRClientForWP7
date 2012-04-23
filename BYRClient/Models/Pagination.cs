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
    public class Pagination
    {
        public int Page_all_count { get; set; }
        public int Page_current_count { get; set; }
        public int Item_page_count { get; set; }
        public int Item_all_count { get; set; }
    }
}
