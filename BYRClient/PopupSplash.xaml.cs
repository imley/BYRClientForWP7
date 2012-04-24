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

namespace BYRClient
{
    public partial class PopupSplash : UserControl
    {
        private Popup gPopupControl;

        public PopupSplash()
        {
            InitializeComponent();            
        }

        public void ShowLoadingStatus()
        {
            gPopupControl = new Popup();
            gPopupControl.Child = this;
            StartLoading();
            gPopupControl.IsOpen = true;            
        }

        public void CloseLoadingStatus()
        {
            if (gPopupControl != null)
                gPopupControl.IsOpen = false;
        }

        public void StartLoading()
        {
            this.progressBar1.IsIndeterminate = true;
        }
    }
}