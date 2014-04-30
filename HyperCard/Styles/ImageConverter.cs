using CONVERTER;
using MODEL;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;

namespace HyperCard.Styles
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return Compressor.GetImagePath(value as Card, MainWindow.lang);
            var card = value as Card;
            string[] ids = MainWindow.lang == LANGUAGE.English || card.zID == string.Empty ? card.IDs : card.zIDs;
            return string.Format("{0}{1}.jpg", Compressor.TempPath, ids[0]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

    public class ImageConverterB : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return Compressor.GetImagePath(value as Card, MainWindow.lang, false);
            var card = value as Card;
            string[] ids = MainWindow.lang == LANGUAGE.English || card.zID == string.Empty ? card.IDs : card.zIDs;
            return card.IsDoubleFaced ? string.Format("{0}{1}.jpg", Compressor.TempPath, ids[1]) : @"\Resources\frame_back.jpg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

}
