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
using System.Runtime.Serialization;

namespace BYRClient.Models
{
    [DataContract]
    public class Section : ApiModel
    {
        public static Dictionary<string, Models.Section> cache = new Dictionary<string, Models.Section>();

        private string name;
        private string description;
        private bool is_root;
        private string parent;
        private List<string> sub_section;
        private List<Board> board;

        //public ObservableCollection<string> GUISub_section{set;get;}
        [IgnoreDataMember]
        public ObservableCollection<UISectionItem> GUISub_section { set; get; }

        #region accessor

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

                    if (GUISub_section == null)
                    {
                        GUISub_section = new ObservableCollection<UISectionItem>();
                    }
                    GUISub_section.Clear();
                    foreach (string section in sub_section)
                    {
                        if (section != null)
                        {
                            UISectionItem e = new UISectionItem();
                            if (cache.ContainsKey(section))
                            {
                                e.DisplayName = cache[section].description;
                            }
                            else
                            {
                                e.DisplayName = section;
                            }                            
                            e.Id = section;
                            e.Type = "section";
                            GUISub_section.Add(e);
                            //GUISub_section.Add(section);
                        }
                    }                            
                }
            }
        }

        [DataMember]
        public List<Board> Board
        {
            get
            {
                return board;
            }
            set
            {
                if (value != board)
                {
                    board = value;
                    NotifyPropertyChanged("Board");

                    //GUISub_section.Clear();
                    if (GUISub_section == null)
                    {
                        GUISub_section = new ObservableCollection<UISectionItem>();
                    }
                    foreach (Board b in board)
                    {
                        if (b != null)
                        {
                            UISectionItem e = new UISectionItem();
                            e.DisplayName = b.Description;
                            e.Id = b.Name;
                            e.Type = "board";
                            GUISub_section.Add(e);
                            //GUISub_section.Add(b.Description);
                        }
                    }
                }
            }

        }

        #endregion

        public Section() {
            GUISub_section = new ObservableCollection<UISectionItem>();
            sub_section = new List<string>();
        }

        public void GetSectionInfo(string name)
        {
            // If the section was requested, return it directly;
            if (cache.ContainsKey(name)) {
                this.SetData(cache[name]);
                return;
            }

            // Else, request it via API
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

                cache.Add(name, this);
            }
            else
            {

                var request = new RestRequest();
                request.Resource = "section/{sectionName}.json";

                request.AddParameter("sectionName", name, ParameterType.UrlSegment);
                App.api.Execute<Section>(request, SetData, FailOnRequest);
                cache.Add(name, this);
            }
        }
    }
}
