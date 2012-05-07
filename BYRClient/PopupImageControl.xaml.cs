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

namespace BYRClient
{
    public partial class PopupImageControl : UserControl
    {
        private Popup gPopupControl;
        public event EventHandler closeEventHander;

        private double initialAngle = 0;
        private double initialScale = 1;

        private double initialCenterX = 0;
        private double initialCenterY = 0;

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
                    QuitReply();
                }
            }
        }

        public PopupImageControl()
        {
            InitializeComponent();
        }

        public void ShowImage(string imgUrl)
        {
            getImageAsync(imgUrl, ImageControl);
            ImageControl.RenderTransformOrigin = new Point(0.5, 0.5);
            ImageControl.Margin = new Thickness(0, 100, 0, 0);

            gPopupControl = new Popup();
            gPopupControl.Child = this;
            gPopupControl.IsOpen = true;
        }

        public void QuitReply()
        {
            gPopupControl.IsOpen = false;
            gPopupControl.Child = null;
            this.gPopupControl = null;
            EventHandler tHandler = this.closeEventHander;
            if (tHandler != null)
                tHandler.Invoke(this, null);
        }

        private void OnPinchStarted(object sender, PinchStartedGestureEventArgs e)
        {
            initialAngle = transform.Rotation;
            initialScale = transform.ScaleX;
        }

        private void OnPinchDelta(object sender, PinchGestureEventArgs e)
        {
            transform.Rotation = initialAngle + e.TotalAngleDelta;
            transform.ScaleX = initialScale * e.DistanceRatio;
            transform.ScaleY = initialScale * e.DistanceRatio;
        }

        private void OnDragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            transform.CenterX = transform.CenterX - e.HorizontalChange;
            transform.CenterY = transform.CenterY - e.VerticalChange;
        }

        private void OnDragStarted(object sender, DragStartedGestureEventArgs e)
        {
            initialCenterX = transform.CenterX;
            initialCenterY = transform.CenterY;
        }

        private void getImageAsync(string Url, Image _Img)
        {
            ProgressBar.IsIndeterminate = true;
            WebClient client = new WebClient();
            NetworkCredential cred = new NetworkCredential(App.api.getUserId(), App.api.getPassword());
            client.Credentials = cred;

            client.OpenReadAsync(new Uri(Url, UriKind.Absolute), _Img);

            client.OpenReadCompleted += new OpenReadCompletedEventHandler(Client_OpenReadCompleted);
        }

        delegate void ShowDownloadCompleted(Stream _Stream, Image _Img);

        void Client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ShowDownloadCompleted(OpenReadCompleted), e.Result, (Image)e.UserState);
        }

        void OpenReadCompleted(Stream _Stream, Image _Img)
        {
            if (!_Stream.Equals(null))
            {
                try
                {
                    BitmapImage Bit = new BitmapImage();
                    Bit.SetSource(_Stream);
                    _Img.Source = Bit;
                }
                catch (Exception e)
                {
                    MessageBox.Show("图片类型不支持！");
                }
            }
            ProgressBar.IsIndeterminate = false;
        }
    }
}