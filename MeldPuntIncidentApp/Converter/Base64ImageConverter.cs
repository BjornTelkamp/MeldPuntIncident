using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeldPuntIncidentApp.Converter;

public class Base64ImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string base64Image)
        {
            byte[] imageData = System.Convert.FromBase64String(base64Image);
            return ImageSource.FromStream(() => new MemoryStream(imageData));
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}