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

namespace BYRClient.Models
{
    public class Article : ApiModel
    {
        private int id;
        private string flag;
        private string content;
        private string title;
        private User user;

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

        #endregion


        public void GetUserInfo(string id)
        {
            var request = new RestRequest();
            request.Resource = "user/query/{userId}.json";

            request.AddParameter("userId", id, ParameterType.UrlSegment);
            App.api.Execute<User>(request, SetData, FailOnRequest);
        }
    }
}
