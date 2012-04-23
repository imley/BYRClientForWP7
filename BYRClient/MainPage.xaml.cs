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

namespace BYRClient
{
    public partial class MainPage : PhoneApplicationPage
    {
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

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Password;

            App.api.SetAuthinfo(username, password);

            this.NavigationService.Navigate(new Uri("/Index.xaml?username="+username, UriKind.Relative));

        }
    }
}