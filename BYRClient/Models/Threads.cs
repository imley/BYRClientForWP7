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
using System.Reflection;
using RestSharp;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BYRClient.Models
{
    public class Threads : ApiModel
    {
        private int id;
        private string title;
        private string board_name;
        private List<Article> article;
        private Pagination pagination;

        // not meta data
        private string pageStr;

        public ObservableCollection<UIArticleItem> GUIArticles { set; get; }


        #region accessor      
       
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != title)
                {
                    title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        public string Board_name
        {
            get
            {
                return board_name;
            }
            set
            {
                if (value != board_name)
                {
                    board_name = value;
                    NotifyPropertyChanged("Board_name");
                }
            }
        }

        public Pagination Pagination
        {
            get
            {
                return pagination;
            }
            set
            {
                if (value != pagination)
                {
                    pagination = value;
                    NotifyPropertyChanged("Pagination");
                    if (pagination != null)
                    {
                        string tmp = "";
                        tmp = tmp + pagination.Page_current_count + " / " + pagination.Page_all_count;
                        PageStr = tmp;
                    }
                }
            }
        }

        public String PageStr
        {
            get
            {
                return pageStr;
            }
            set
            {
                if (value != pageStr)
                {
                    pageStr = value;
                    NotifyPropertyChanged("PageStr");
                }
            }
        }

        public List<Article> Article
        {
            get
            {
                return article;
            }
            set
            {
                if (value != article)
                {
                    article = value;
                    NotifyPropertyChanged("Article");
                    foreach (Article a in article)
                    {
                        UIArticleItem uba = new UIArticleItem();
                        uba.Article = a;
                        GUIArticles.Add(uba);
                    }
                }
            }
        }
        #endregion

        public Threads() {
            GUIArticles = new ObservableCollection<UIArticleItem>();
            article = new List<Article>();
            PageStr = "1 / N";
        }

        public void GetThreadsInfo(string id, string board, int page)
        {
            if (GUIArticles.Count > 0)
            {
                GUIArticles.Clear();
            }
            var request = new RestRequest();
            request.Resource = "threads/{boardName}/{id}.json";

            request.AddParameter("id", id, ParameterType.UrlSegment);
            request.AddParameter("boardName", board, ParameterType.UrlSegment);
            request.AddParameter("page", page);
            App.api.Execute<Threads>(request, SetData, FailOnRequest);            
        }
    }
}
