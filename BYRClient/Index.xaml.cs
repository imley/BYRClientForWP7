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
using System.IO.IsolatedStorage;

namespace BYRClient
{
    public partial class Index : PhoneApplicationPage
    {
        public static User appUser;

        public Index()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string username = this.NavigationContext.QueryString["username"];

            User user;
            if(appUser != null && appUser.Id == username)
            {
                user = appUser;
            }
            else
            {
                user = new User();
                Action<string> failedAction = this.FailedInLogin;
                user.GetUserInfo(username, failedAction);
            }            
            DataContext = user;
            appUser = user;
        }

        private void FailedInLogin(string error)
        {
            MessageBox.Show(error);
            BackToMain();
        }

        private void BackToMain()
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

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            BackToMain();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/SectionPage.xaml?section=" + "this_can_never_be_it", UriKind.Relative));
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            BackToMain();
        }

        private void Test_Click(object sender, EventArgs e)
        {
            /*IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Clear();
            settings.Save();*/
            //this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            //Models.Section s = new Models.Section();
            //s.GetSectionInfo("Advertise");
            //Board b = new Board();
            //b.GetBoardInfo("CPP");
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            List<string> deleteList = new List<string>();
            // No remove at iteration, man.
            foreach (string key in settings.Keys)
            {
                if (key.StartsWith("section_cache_"))
                {
                    deleteList.Add(key);
                }
            }

            foreach (string key in deleteList)
            {
                settings.Remove(key);
            }
            settings.Save();
            Models.Section.cache = new Dictionary<string, Models.Section>();
        }

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("开发中，敬请期待！");
        }
    }
}