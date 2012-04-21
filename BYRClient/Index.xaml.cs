using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using BYRClient.Models;
using System.ComponentModel;

namespace BYRClient
{
    public partial class Index : PhoneApplicationPage
    {
        public Index()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string username = this.NavigationContext.QueryString["username"];

            User user = new User();
            user.GetUserInfo(username);
            DataContext = user;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            while (NavigationService.CanGoBack)
            {
                IEnumerator<JournalEntry> list = NavigationService.BackStack.GetEnumerator();
                list.MoveNext();
                JournalEntry current = list.Current;
                string uri = current.Source.ToString();
                if (uri == "/MainPage.xaml")
                {
                    NavigationService.GoBack();
                }
                else
                {
                    NavigationService.RemoveBackEntry();
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //Models.Section s = new Models.Section();
            //s.GetSectionInfo("Advertise");
            Board b = new Board();
            b.GetBoardInfo("CPP");
            //this.NavigationService.Navigate(new Uri("/SectionPage.xaml?section=" + "this_can_never_be_it", UriKind.Relative));
        }
    }
}