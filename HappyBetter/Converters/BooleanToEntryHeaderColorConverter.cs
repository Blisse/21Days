using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace HappyBetter.Converters
{
    public class BooleanToEntryHeaderColorConverter : IValueConverter
    {
        private static readonly BooleanToEntryHeaderColorConverter Converter = new BooleanToEntryHeaderColorConverter();

        public static SolidColorBrush Convert(Boolean isCompleted)
        {
            return (SolidColorBrush)Converter.Convert(isCompleted, null, null, null);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Boolean)
            {
                if ((Boolean)value)
                {
                    return Application.Current.Resources["ForegroundGreen"];
                }
            }

            return Application.Current.Resources["ForegroundWhite"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
