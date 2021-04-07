using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Windows.UI.Xaml.Data;
using Xamarin.Forms;

namespace Agencia_Pil.Helpers
{
    class Base64StringToImageSourceConverter: Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var base64 = (string)value;
            return ImageSource.FromStream(
            () => new MemoryStream(System.Convert.FromBase64String(base64)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
