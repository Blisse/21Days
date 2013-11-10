using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HappyBetter.Converters
{
    public class MainPageDateTimeToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                var entryKey = (DateTime) value;
                var dict = App.ViewModelLocator.EntryPage.Dictionary;
                if (dict != null && dict.ContainsKey(entryKey))
                {
                    var entry = dict[entryKey];
                    if (entry.IsCompleted)
                    {
                        return Application.Current.Resources["BackgroundGreen"];
                    }
                }

            }

            return Application.Current.Resources["BackgroundBlue"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
