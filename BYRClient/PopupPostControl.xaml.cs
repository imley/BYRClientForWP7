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

namespace BYRClient
{
    public partial class PopupPostControl : UserControl
    {
        private Popup gPopupControl;
        private string board;
        private int reid = -1;
        public string Title { get; set; }
        public event EventHandler closeEventHander;

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

        public PopupPostControl()
        {
            InitializeComponent();
        }

        public void ShowReply(string boardName, Article article)
        {
            this.board = boardName;
            titleTextBox.Focus();

            this.Title = "发表文章：" + boardName;

            if (article != null)
            {
                titleTextBox.Text = "Re: " + article.Title;
                contentTextBox.Text = "\n【 在 " + article.Id + " 的大作中提到: 】\n: " + GetReplyContent(article.Content);
                reid = article.Id;
                contentTextBox.Focus();
                this.Title = "回复：" + article.Title;
            }

            this.DataContext = this;

            gPopupControl = new Popup();
            gPopupControl.Child = this;
            gPopupControl.IsOpen = true;
        }

        private string GetReplyContent(string content)
        {
            string newContent = "";

            if (content.Length > 100)
                newContent = content.Substring(0, 100) + "... ...";
            else
                newContent = content;

            newContent = newContent.Replace("\n", "\n: ");

            return newContent;
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            QuitReply();
        }

        private void OnClickReply(object sender, RoutedEventArgs e)
        {
            string title = titleTextBox.Text;
            string content = contentTextBox.Text;

            Article.PostArticle(board, title, content, reid, ReplySuccess, ReplyFailure);
        }

        public void ReplySuccess(Board b)
        {
            MessageBox.Show("发布成功！");
            QuitReply();
        }

        public void ReplyFailure(string f)
        {
            MessageBox.Show("发布失败！");
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
    }
}