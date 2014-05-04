using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HyperCard
{
    /// <summary>
    /// Message.xaml 的交互逻辑
    /// </summary>
    public partial class Message : Window
    {
        private static MessageBoxResult result;
        private string title
        {
            get
            {
                return txtTitle.Text;
            }
            set
            {
                txtTitle.Text = value;
            }
        }
        private string message
        {
            get
            {
                return txtMsg.Text;
            }
            set
            {
                txtMsg.Text = value;
            }
        }

        private Message()
        {
            InitializeComponent();
        }

        public static MessageBoxResult Show(string msg)
        {
            new Message()
            {
                title = string.Empty,
                message = msg,
                btnNo = { Visibility = Visibility.Collapsed },
                btnYes = { Visibility = Visibility.Collapsed },
                btnCancel = { Visibility = Visibility.Collapsed },
                btnOK = { Visibility = Visibility.Visible }
            }.ShowDialog();

            return result;
        }

        public static MessageBoxResult Show(string title, string msg, MessageBoxButton btn)
        {
            bool yesNo = btn.ToString().Contains("Yes");
            bool cancel = btn.ToString().Contains("Cancel");
            bool ok = btn.ToString().Contains("OK");

            new Message()
            {
                title = title,
                message = msg,
                btnNo = { Visibility = yesNo ? Visibility.Visible : Visibility.Collapsed },
                btnYes = { Visibility = yesNo ? Visibility.Visible : Visibility.Collapsed },
                btnCancel = { Visibility = cancel && yesNo ? Visibility.Visible : Visibility.Collapsed },
                btnOK = { Visibility = ok ? Visibility.Visible : Visibility.Collapsed }

            }.ShowDialog();

            return result;
        }

        private void Window_DragMove(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.None;
            Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.Yes;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.OK;
            Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.No;
            Close();
        }
    }
}
