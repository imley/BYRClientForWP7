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
    public class ApiModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

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

        public void FailOnRequest(String error)
        {
            Console.Write('e');
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
                }
            }
        }
    }
}
