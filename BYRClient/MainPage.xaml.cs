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
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.Windows.Controls.Primitives;
using BYRClient.Models;
using System.Windows.Navigation;
using System.ComponentModel;

namespace BYRClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        private PopupSplash popup;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            string user;
            string pass;
            settings.TryGetValue("username", out user);
            settings.TryGetValue("password", out pass);
            if (user == null) user = "guest";
            if (pass == null) pass = "";
            usernameBox.Text = user;
            passwordBox.Password = pass;
        }
                
        private void ShowPopup()
        {
            this.popup = new PopupSplash();
            this.popup.ShowLoadingStatus();
        }

        private void CheckLogin(string user, string pass)
        {
            App.api.SetAuthinfo(user, pass);

            Index.appUser = new User();
            Action<string> failedAction = this.LoginFailed;
            Action<User> successAction = this.LoginSuccess;

            Index.appUser.CheckUserLogin(user, failedAction, successAction);
        }

        private void LoginSuccess(User user)
        {
            if (user.User_name != null && user.Msg == null)
            {
                this.popup.CloseLoadingStatus();
                this.NavigationService.Navigate(new Uri("/Index.xaml?username=" + user.Id, UriKind.Relative));
            }
            else
            {
                LoginFailed(user.Msg);
            }

        }

        private void LoginFailed(string msg)
        {
            Index.appUser = new User();
            this.popup.CloseLoadingStatus();
            MessageBox.Show(msg);
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Password;

            CheckLogin(username, password);
            ShowPopup();
        }
    }
}