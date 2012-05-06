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
using System.Collections.ObjectModel;

namespace BYRClient.Models
{
    public class Article : ApiModel
    {
        private int id;
        private string flag;
        private string content;
        private string title;
        private string board_name;
        private int reply_count;
        private int post_time;
        private string postTime;
        private User user;
        private Attachment attachment;

        public ObservableCollection<string> GUIPieceList { set; get; }
        
        public string GUIPostTime {
            get
            {
                return postTime;
            }
            set
            {
                if (value != postTime)
                {
                    postTime = value;
                    NotifyPropertyChanged("GUIPostTime");
                }
            }
        }

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

        public string Flag 
        {
            get {
                return flag;
            }
            set {
                if (value != flag)
                {
                    flag = value;
                    NotifyPropertyChanged("Flag");
                }
            }
        }

        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                if (value != content)
                {
                    content = value;
                    NotifyPropertyChanged("Content");
                    string tmp = content;
                    while (tmp.Length > 0)
                    {                        
                        try
                        {
                            string piece = tmp.Substring(0, 800);
                            GUIPieceList.Add(piece);
                            tmp = tmp.Remove(0, 800);
                        }
                        catch (System.ArgumentOutOfRangeException e)
                        {
                            Console.Write(tmp);
                            GUIPieceList.Add(tmp);
                            tmp = "";
                        }
                    }
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

        public int Reply_count
        {
            get
            {
                return reply_count;
            }
            set
            {
                if (value != reply_count)
                {
                    reply_count = value;
                    NotifyPropertyChanged("Reply_count");
                }
            }
        }

        public int Post_time
        {
            get
            {
                return post_time;
            }
            set
            {
                if (value != post_time)
                {
                    post_time = value;
                    NotifyPropertyChanged("Post_time");
                    GUIPostTime = GetTimeFromDiff(post_time);
                }
            }
        }

        public User User
        {
            get
            {
                return user;
            }
            set
            {
                if (value != user)
                {
                    user = value;
                    NotifyPropertyChanged("User");
                }
            }
        }

        public Attachment Attachment
        {
            get
            {
                return attachment;
            }
            set
            {
                if (value != attachment)
                {
                    attachment = value;
                    NotifyPropertyChanged("Attachment");
                }
            }
        }

        #endregion

        public Article()
        {
            GUIPieceList = new ObservableCollection<string>();
        }
       
        public static void PostArticle(string boardName, string title, string content, int reid, Action<Board> success, Action<string> failure)
        {
            var request = new RestRequest();
            request.Resource = ("article/{name}/post.json?appkey="+App.api.getApiKey());

            request.AddParameter("name", boardName, ParameterType.UrlSegment);

            request.AddParameter("title", title, ParameterType.GetOrPost);
            request.AddParameter("content", content, ParameterType.GetOrPost);
            // -1 means this is a single post;
            if (reid != -1)
                request.AddParameter("reid", reid, ParameterType.GetOrPost);

            request.Method = Method.POST;

            App.api.Execute<Board>(request, success, failure);
        }
    }
}
