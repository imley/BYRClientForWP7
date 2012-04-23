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
using System.Windows.Controls.Primitives;

namespace BYRClient
{
    public partial class ThreadsPage : PhoneApplicationPage
    {
        private Popup popup;
        //private static Dictionary<string, Threads> currentPage = new Dictionary<string, Threads>();
        private static Threads currentData = null;

        public ThreadsPage()
        {
            InitializeComponent();
        }

        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string id = this.NavigationContext.QueryString["id"];
            string board = this.NavigationContext.QueryString["board"];
            int page = 1;
            if (this.NavigationContext.QueryString.ContainsKey("page"))
            {
                page = int.Parse(this.NavigationContext.QueryString["page"]);
            }
            //TO-DO: DRY, DRY!!!
            Threads t;            
            if (App.IsRecovered == true && currentData != null)
            {
                t = currentData;
                App.IsRecovered = false;
            }
            else 
            {
                ShowPopup();
                t = new Threads();
                t.GetThreadsInfo(id, board, page);
                t.RelatedPop = this.popup;                
            }            
            
            DataContext = t;
            boardList.ItemsSource = t.GUIArticles;

            currentData = t;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.popup.IsOpen = false;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            String parent = ((Threads)DataContext).Board_name;
            this.NavigationService.Navigate(new Uri("/BoardPage.xaml?board=" + parent, UriKind.Relative));
            e.Cancel = true;
        }

        private void ShowPopup()
        {
            this.popup = new Popup();
            this.popup.Child = new PopupSplash();
            this.popup.IsOpen = true;
        }

        private void articleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {    
        }

        private void OnNextPageClick(object sender, EventArgs e)
        {
            if(currentData.Pagination.Page_current_count < currentData.Pagination.Page_all_count)
                this.NavigationService.Navigate(new Uri("/ThreadsPage.xaml?board=" + currentData.Board_name + "&id=" + currentData.Id + "&page=" + (currentData.Pagination.Page_current_count+1), UriKind.Relative));
            //currentData.GetThreadsInfo(currentData.Id.ToString(), currentData.Board_name, currentData.Pagination.Page_current_count + 1);
        }

        private void OnPreviousPageClick(object sender, EventArgs e)
        {
            if (currentData.Pagination.Page_current_count > 1)
                this.NavigationService.Navigate(new Uri("/ThreadsPage.xaml?board=" + currentData.Board_name + "&id=" + currentData.Id + "&page=" + (currentData.Pagination.Page_current_count - 1), UriKind.Relative));
        }
    }
}