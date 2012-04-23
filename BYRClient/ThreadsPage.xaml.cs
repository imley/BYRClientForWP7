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
using System.Windows.Navigation;
using System.ComponentModel;
using BYRClient.Models;

namespace BYRClient
{
    public partial class ThreadsPage : PhoneApplicationPage
    {
        public ThreadsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string id = this.NavigationContext.QueryString["id"];
            string board = this.NavigationContext.QueryString["board"];

            Threads t = new Threads();
            t.GetThreadsInfo(id, board);
            DataContext = t;

            boardList.ItemsSource = t.GUIArticles;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            String parent = ((Threads)DataContext).Board_name;
            this.NavigationService.Navigate(new Uri("/BoardPage.xaml?board=" + parent, UriKind.Relative));
            e.Cancel = true;
        }

        private void articleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UIArticleItem selectedItem;
            selectedItem = (UIArticleItem)boardList.SelectedItem;
            selectedItem.Color = "Red";
            
            //this.NavigationService.Navigate(new Uri("/ThreadsPage.xaml?id=" + selectedItem.Article.Id, UriKind.Relative));            
        }
    }
}