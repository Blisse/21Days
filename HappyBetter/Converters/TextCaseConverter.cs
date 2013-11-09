using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HappyBetter.Converters
{
    public class TextCaseConverter : IValueConverter
    {
        private const String UpperCase = "upper";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as String;
            var para = parameter as String ?? UpperCase;

            if (!String.IsNullOrWhiteSpace(text))
            {
                if (para == UpperCase)
                {
                    return text.ToLower();
                }
                return text.ToUpper();
            }
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
