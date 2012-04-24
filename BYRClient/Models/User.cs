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
    public class User : ApiModel
    {
        private string id;
        private string user_name;
        private string face_url;
        private int face_width;
        private int face_height;

        //used for login check
        public string Code { get; set; }
        public string Msg { get; set; }

        public string Id
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

        public string User_name 
        {
            get {
                return user_name;
            }
            set {
                if (value != user_name)
                {
                    user_name = value;
                    NotifyPropertyChanged("User_name");
                }
            }
        }

        public string Face_url
        {
            get
            {
                return face_url;
            }
            set
            {
                if (value != face_url)
                {
                    face_url = value;
                    NotifyPropertyChanged("Face_url");
                }
            }

        }

        public void CheckUserLogin(string id, Action<string> failed, Action<User> success)
        {
            var request = new RestRequest();
            request.Resource = "user/query/{userId}.json";

            request.AddParameter("userId", id, ParameterType.UrlSegment);
            App.api.Execute<User>(request, success, failed);
        }

        public void GetUserInfo(string id, Action<string> failed)
        {
            var request = new RestRequest();
            request.Resource = "user/query/{userId}.json";

            request.AddParameter("userId", id, ParameterType.UrlSegment);
            App.api.Execute<User>(request, SetData, failed);
        }
        
        /*public void SetData(User actual)
        {
            foreach (var property in actual.GetType().GetProperties())
            {
                PropertyInfo propertyS = this.GetType().GetProperty(property.Name);
                var value = property.GetValue(actual, null);
                propertyS.SetValue(this, value, null);
            }
        }*/
    }
}
