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
using System.Windows.Controls.Primitives;
using System.Runtime.Serialization;
using System.Windows.Navigation;

namespace BYRClient.Models
{
    [DataContract]
    public class ApiModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public PopupSplash RelatedPop { get; set; }
        private bool failed = false;
        public bool Failed 
        { 
            get
            {
                return failed;
            }
            set
            {
                failed = value;
            } 
        }

        /// <summary>
        /// Raise the PropertyChanged event and pass along the property that changed
        /// </summary>
        protected void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        protected void SetIsDone()
        {
            if (this.RelatedPop != null)
                this.RelatedPop.CloseLoadingStatus();
        }

        public void FailOnRequest(String error)
        {
            this.Failed = true;
        }

        public void SetData<T>(T actual)
        {            
            foreach (var property in actual.GetType().GetProperties())
            {
                if (!property.Name.Contains("GUI"))
                {
                    PropertyInfo propertyS = this.GetType().GetProperty(property.Name);
                    var value = property.GetValue(actual, null);
                    propertyS.SetValue(this, value, null);                    
                    this.SetIsDone();
                }
            }
        }
    }
}
