﻿using System;
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

namespace BYRClient
{
    public partial class SectionPage : PhoneApplicationPage
    {
        public SectionPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string sectionName = this.NavigationContext.QueryString["section"];

            Models.Section section = new Models.Section();
            section.GetSectionInfo(sectionName);
            DataContext = section;

            sectionList.ItemsSource = section.GUISub_section;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            String parent = ((Models.Section)DataContext).Parent;
            if (parent != "now_exit")
            {                
                this.NavigationService.Navigate(new Uri("/SectionPage.xaml?section=" + parent, UriKind.Relative));
                e.Cancel = true;
            }
            else
            {                
                this.NavigationService.Navigate(new Uri("/Index.xaml?username=" + App.api.getUserId(), UriKind.Relative));
                e.Cancel = true;
            }
        }

        private void sectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSection;
            selectedSection = (string)sectionList.SelectedItem;
            this.NavigationService.Navigate(new Uri("/SectionPage.xaml?section=" + selectedSection, UriKind.Relative));
        }
    }
}