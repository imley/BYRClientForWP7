﻿using System;
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
    public class Board : ApiModel
    {
        private string name;
        private string description;
        private string section;
        private List<Article> article;

        public ObservableCollection<UIArticleItem> GUIArticles { set; get; }


        #region accessor      
       
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value != description)
                {
                    description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        public string Section
        {
            get
            {
                return section;
            }
            set
            {
                if (value != section)
                {
                    section = value;
                    NotifyPropertyChanged("Section");
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

        public Board() {
            GUIArticles = new ObservableCollection<UIArticleItem>();
            article = new List<Article>();
        }


        public void GetBoardInfo(string boardName)
        {
            var request = new RestRequest();
            request.Resource = "board/{boardName}.json";

            request.AddParameter("boardName", boardName, ParameterType.UrlSegment);
            App.api.Execute<Board>(request, SetData, FailOnRequest);
        }
    }
}
