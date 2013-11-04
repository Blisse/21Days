using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HappyBetter.Converters
{
    public class TextToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var entryString = value as String;

            if (String.IsNullOrWhiteSpace(entryString) || entryString.Length < 6)
            {
                return Application.Current.Resources["BackgroundStarted"];
            }

            return Application.Current.Resources["BackgroundCompleted"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
