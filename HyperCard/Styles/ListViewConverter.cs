using MODEL;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace HyperCard.Styles
{
    public class ListViewConverter : IMultiValueConverter
    {
        #region IMultiValueConverter SelectedItem
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            foreach (var item in values)
            {
                if ((item as ListView).IsFocused)
                {
                    return ((item as ListView).SelectedItem as ListViewItem).Content;
                }
            }

            return null;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { };
        }
        #endregion
    }
}
