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
        private Message()
        {
            InitializeComponent();
        }

        public static void Show(string msg)
        {

        }

        public static void Show(string msg, string title, MessageBoxButton btn)
        {
            switch (btn)
            {
                case MessageBoxButton.OK:
                    break;
                case MessageBoxButton.OKCancel:
                    break;
                case MessageBoxButton.YesNo:
                    break;
                case MessageBoxButton.YesNoCancel:
                    break;
                default:
                    break;
            }
        }
        private void Window_DragMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
