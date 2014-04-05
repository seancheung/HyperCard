using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace HyperCard.Styles
{
    public class StarWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ListView listview = value as ListView;
            double width = listview.ActualWidth;
            GridView gv = listview.View as GridView;
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                if (!Double.IsNaN(gv.Columns[i].Width))
                    width -= gv.Columns[i].ActualWidth;
            }
            return width - 5;// this is to take care of margin/padding
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
