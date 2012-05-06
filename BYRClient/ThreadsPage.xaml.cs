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
        private PopupSplash popup;
        private PopupPostControl postControl;
        private PopupImageControl imageControl;
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
            //TODO: DRY, DRY!!!
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
            articleList.ItemsSource = t.GUIArticles;

            currentData = t;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.popup.CloseLoadingStatus();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            //String parent = ((Threads)DataContext).Board_name;
            //this.NavigationService.Navigate(new Uri("/BoardPage.xaml?board=" + parent, UriKind.Relative));
            if (postControl != null && postControl.IsOpen)
            {
                postControl.IsOpen = false;
                e.Cancel = true;
            }
            else if (imageControl != null && imageControl.IsOpen)
            {
                imageControl.IsOpen = false;
                e.Cancel = true;
            }
            else
            {
                e.Cancel = BackToBoard();
            }
        }

        private void ShowPopup()
        {
           this.popup = new PopupSplash();
           this.popup.ShowLoadingStatus();
        }

        private void articleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private bool BackToBoard()
        {
            while (NavigationService.CanGoBack)
            {
                IEnumerator<JournalEntry> list = NavigationService.BackStack.GetEnumerator();
                list.MoveNext();
                JournalEntry current = list.Current;
                string uri = current.Source.ToString();
                if (uri.StartsWith("/BoardPage.xaml"))
                {
                    NavigationService.GoBack();
                    return true;
                }
                else
                {
                    NavigationService.RemoveBackEntry();
                    return false;
                }
            }

            return true;
        }

        private void OnNextPageClick(object sender, EventArgs e)
        {
            if (currentData.Pagination.Page_current_count < currentData.Pagination.Page_all_count + 1)
            {
                //this.NavigationService.Navigate(new Uri("/ThreadsPage.xaml?board=" + currentData.Board_name + "&id=" + currentData.Id + "&page=" + (currentData.Pagination.Page_current_count + 1), UriKind.Relative));
                this.popup.CloseLoadingStatus();
                ShowPopup();
                currentData.RelatedPop = this.popup;
                currentData.GetThreadsInfo(currentData.Id.ToString(), currentData.Board_name, currentData.Pagination.Page_current_count + 1);                
            }
            else
                MessageBox.Show("已经是最后一页了！");            
        }

        private void OnPreviousPageClick(object sender, EventArgs e)
        {
            if (currentData.Pagination.Page_current_count > 1)
            {
                //this.NavigationService.Navigate(new Uri("/ThreadsPage.xaml?board=" + currentData.Board_name + "&id=" + currentData.Id + "&page=" + (currentData.Pagination.Page_current_count - 1), UriKind.Relative));
                ShowPopup();
                currentData.RelatedPop = this.popup;
                currentData.GetThreadsInfo(currentData.Id.ToString(), currentData.Board_name, currentData.Pagination.Page_current_count - 1);
                
            }
            else
                MessageBox.Show("已经是第一页了！");
        }

        private void OnClickReply(object sender, RoutedEventArgs e)
        {
            if (articleList.SelectedItem == null)
            {
                articleList.SelectedIndex = 0;
            }
            UIArticleItem item = (UIArticleItem)articleList.SelectedItem;
            Article article = item.Article;
            postControl = new PopupPostControl();
            postControl.closeEventHander += new EventHandler(PostControlCloseHandler);
            postControl.ShowReply(article.Board_name, article);
            ApplicationBar.IsVisible = false;
        }

        private void OnReplyPostClick(object sender, EventArgs e)
        {
            articleList.SelectedIndex = 0;
            UIArticleItem item = (UIArticleItem)articleList.SelectedItem;
            Article article = item.Article;
            postControl = new PopupPostControl();
            postControl.closeEventHander += new EventHandler(PostControlCloseHandler);
            postControl.ShowReply(article.Board_name, article);
            ApplicationBar.IsVisible = false;
        }

        private void PostControlCloseHandler(object sender, EventArgs e)
        {
            ApplicationBar.IsVisible = true;
        }

        private void OnImageTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image image = (Image)sender;
            AttFile att = (AttFile)image.DataContext;
            imageControl = new PopupImageControl();
            //imageControl.ShowImage(att.Thumbnail_middle);
            imageControl.ShowImage(att.Url);
            ApplicationBar.IsVisible = false;
            imageControl.closeEventHander += new EventHandler(ImageControlCloseHandler);
        }

        private void ImageControlCloseHandler(object sender, EventArgs e)
        {
            ApplicationBar.IsVisible = true;
        }

    }
}