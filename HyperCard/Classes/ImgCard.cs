using CONVERTER;
using MODEL;
using System;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HyperCard
{
    public class ImgCard : Card
    {
        public ImgCard(Card card)
        {
            if (card != null)
            {
                PropertyInfo[] properties = typeof(Card).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo propertyInfo = properties[i];
                    propertyInfo.SetValue(this, typeof(Card).GetProperty(propertyInfo.Name).GetValue(card, null), null);
                }
            }
        }

        public ImageSource Image
        {
            get
            {
                var uri = Compressor.GetImagePath(this as Card, MainWindow.lang);
                return new BitmapImage(new Uri(uri));
            }
        }

        public ImageSource ImageB
        {
            get
            {
                var uri = Compressor.GetImagePath(this as Card, MainWindow.lang, false);
                return new BitmapImage(new Uri(uri));
            }
        }
    }
}
