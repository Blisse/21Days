using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HappyBetter.Converters
{
    public class EntryItemTextToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var entryString = value as String;

            if (String.IsNullOrWhiteSpace(entryString))
            {
                return Application.Current.Resources["BackgroundBlack"];
            }

            return Application.Current.Resources["BackgroundGreen"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
