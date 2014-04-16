using CONVERTER;
using MODEL;
using System;
using System.Globalization;
using System.Windows.Data;

namespace HyperCard.Styles
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //string uri = null;

            //if (value is Card)
            //{
            //    string id = MainWindow.lang == LANGUAGE.English || (value as Card).zID == string.Empty ? (value as Card).ID : (value as Card).zID;

            //    Compressor.Unzip((value as Card), MainWindow.lang);

            //    if (id.Contains("|")) id = id.Remove(id.IndexOf("|"));
            //    uri = string.Format("{0}{1}.jpg", Compressor.TempPath, id);
            //}

            //return uri;
            return Compressor.GetImagePath(value as Card, MainWindow.lang);

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
            //string uri = @"Resources\frame_back.jpg";

            //if (value is Card)
            //{
            //    string id = MainWindow.lang == LANGUAGE.English || (value as Card).zID == string.Empty ? (value as Card).ID : (value as Card).zID;
            //    if (id.Contains("|"))
            //    {
            //        Compressor.Unzip((value as Card), MainWindow.lang);

            //        uri = string.Format("{0}{1}.jpg", Compressor.TempPath, id.Substring(id.IndexOf("|") + 1));
            //    }
            //}

            //return uri;
            return Compressor.GetImagePath(value as Card, MainWindow.lang, false);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
