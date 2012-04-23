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
    public partial class BoardPage : PhoneApplicationPage
    {
        public BoardPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string boardName = this.NavigationContext.QueryString["board"];

            Board board = new Board();
            board.GetBoardInfo(boardName);
            DataContext = board;

            boardList.ItemsSource = board.GUIArticles;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            String parent = ((Board)DataContext).Section;
            this.NavigationService.Navigate(new Uri("/SectionPage.xaml?section=" + parent, UriKind.Relative));
            e.Cancel = true;
        }

        private void articleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UIArticleItem selectedItem;
            selectedItem = (UIArticleItem)boardList.SelectedItem;
            selectedItem.Color = "Red";

            this.NavigationService.Navigate(new Uri("/ThreadsPage.xaml?id=" + selectedItem.Article.Id + "&board=" + selectedItem.Article.Board_name, UriKind.Relative));            
        }
    }
}