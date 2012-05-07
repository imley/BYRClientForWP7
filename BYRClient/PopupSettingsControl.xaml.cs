using System;
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
using System.Windows.Controls.Primitives;
using BYRClient.Models;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using System.IO;
using System.IO.IsolatedStorage;

namespace BYRClient
{
    public partial class PopupSettingsControl : UserControl
    {
        private Popup gPopupControl;
        public event EventHandler closeEventHander;

        public static readonly string SETTINGS_SHOWBIG = "show_big_image";
        private const string SETTINGS_SHOWBIG_ON = "原图查看：打开";
        private const string SETTINGS_SHOWBIG_OFF = "原图查看：关闭";

        public IsolatedStorageSettings settings;

        public bool IsOpen
        {
            get
            {
                if (gPopupControl != null)
                {
                    return gPopupControl.IsOpen;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value == true)
                {
                    //pass
                }
                else
                {
                    QuitControl();
                }
            }
        }

        public PopupSettingsControl()
        {
            InitializeComponent();
        }

        public void ShowSettings()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;

            bool showBig;

            showBig = Utils.Settings.GetAppSettings().GetSetting(SETTINGS_SHOWBIG);

            if (showBig)
            {
                ShowUrlSwitch.Content = SETTINGS_SHOWBIG_ON;
                ShowUrlSwitch.IsChecked = true;
            }
            else
            {
                ShowUrlSwitch.Content = SETTINGS_SHOWBIG_OFF;
                ShowUrlSwitch.IsChecked = false;
            }

            ShowUrlSwitch.IsChecked = showBig;

            gPopupControl = new Popup();
            gPopupControl.Child = this;
            gPopupControl.IsOpen = true;
        }

        public void QuitControl()
        {
            gPopupControl.IsOpen = false;
            gPopupControl.Child = null;
            this.gPopupControl = null;
            EventHandler tHandler = this.closeEventHander;
            if (tHandler != null)
                tHandler.Invoke(this, null);
        }

        private void ShowUrlSwitch_Checked(object sender, RoutedEventArgs e)
        {
            ShowUrlSwitch.Content = SETTINGS_SHOWBIG_ON;
            Utils.Settings.GetAppSettings().SetSetting(SETTINGS_SHOWBIG, true);
        }

        private void ShowUrlSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            ShowUrlSwitch.Content = SETTINGS_SHOWBIG_OFF;
            Utils.Settings.GetAppSettings().SetSetting(SETTINGS_SHOWBIG, false);
        }
    }
}