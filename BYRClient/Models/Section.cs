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
    public class Section : ApiModel
    {
        private string name;
        private string description;
        private bool is_root;
        private string parent;
        private List<string> sub_section;
        private List<string> board;

        public ObservableCollection<string> GUISub_section{set;get;}

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
            get {
                return description;
            }
            set {
                if (value != description)
                {
                    description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        public string Parent
        {
            get
            {
                return parent;
            }
            set
            {
                if (value != parent)
                {
                    parent = value;
                    NotifyPropertyChanged("Parent");
                }
            }

        }

        public bool Is_root
        {
            get
            {
                return is_root;
            }
            set
            {
                if (value != is_root)
                {
                    is_root = value;
                    NotifyPropertyChanged("Is_root");
                }
            }

        }

        public List<string> Sub_section
        {
            get
            {
                return sub_section;
            }
            set
            {
                if (value != sub_section)
                {
                    sub_section = value;
                    NotifyPropertyChanged("Sub_section");
                   
                    GUISub_section.Clear();
                    foreach (string section in sub_section)
                    {
                        if (section != null)
                        {
                            GUISub_section.Add(section);
                        }
                    }                            
                }
            }

        }

        #endregion

        public Section() {
            GUISub_section = new ObservableCollection<string>();
            sub_section = new List<string>();
        }

        public void GetSectionInfo(string name)
        {
            if (name == "this_can_never_be_it" || name == "")
            {
                Section s = new Section();
                s.description = "分区列表";
                s.name = "this_can_never_be_it";
                s.parent = "now_exit";
                for (int i=0; i<9; i++) {
                    s.sub_section.Add(i.ToString());
                }

                this.SetData(s);
            }
            else
            {

                var request = new RestRequest();
                request.Resource = "section/{sectionName}.json";

                request.AddParameter("sectionName", name, ParameterType.UrlSegment);
                App.api.Execute<Section>(request, SetData, FailOnRequest);
            }
        }
    }
}
