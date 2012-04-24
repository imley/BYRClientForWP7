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
    public partial class BoardPage : PhoneApplicationPage
    {
        private PopupSplash popup;
        private static Board currentData = null;

        public BoardPage()
        {
            InitializeComponent();            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string boardName = this.NavigationContext.QueryString["board"];
            int page;
            if (this.NavigationContext.QueryString.ContainsKey("page"))
            {
                page = int.Parse(this.NavigationContext.QueryString["page"]);
            }
            else
            {
                page = 1;
            }

            Board board;
            if (App.IsRecovered == true && currentData != null)
            {
                board = currentData;
                App.IsRecovered = false;
            }
            else
            {
                ShowPopup();
                board = new Board();
                board.GetBoardInfo(boardName, 10, page);
                board.RelatedPop = this.popup;
                
            }

            DataContext = board;
            boardList.ItemsSource = board.GUIArticles;

            currentData = board;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            String parent = ((Board)DataContext).Section;
            this.NavigationService.Navigate(new Uri("/SectionPage.xaml?section=" + parent, UriKind.Relative));
            e.Cancel = true;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.popup.CloseLoadingStatus();
        }

        private void ShowPopup()
        {
            this.popup = new PopupSplash();
            this.popup.ShowLoadingStatus();
        }

        private void articleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UIArticleItem selectedItem;
            selectedItem = (UIArticleItem)boardList.SelectedItem;
            // null exception can be caused by goback
            if (selectedItem != null)
            {
                selectedItem.Color = "Red";
                this.NavigationService.Navigate(new Uri("/ThreadsPage.xaml?id=" + selectedItem.Article.Id + "&board=" + selectedItem.Article.Board_name, UriKind.Relative));
            }
        }

        private void OnNextPageClick(object sender, EventArgs e)
        {
            if (currentData.Pagination.Page_current_count < currentData.Pagination.Page_all_count)
                this.NavigationService.Navigate(new Uri("/BoardPage.xaml?board=" + currentData.Name + "&page=" + (currentData.Pagination.Page_current_count + 1), UriKind.Relative));
            else
                MessageBox.Show("已经是最后一页了！");
            //currentData.GetBoardInfo(currentData.Name, 10, currentData.Pagination.Page_current_count+1);
        }

        private void OnPreviousPageClick(object sender, EventArgs e)
        {
            if (currentData.Pagination.Page_current_count > 1)
            {
                NavigationService.GoBack();
                //this.NavigationService.Navigate(new Uri("/BoardPage.xaml?board=" + currentData.Name + "&page=" + (currentData.Pagination.Page_current_count - 1), UriKind.Relative));
            }
            else
                MessageBox.Show("已经是第一页了！");
        }
    }
}