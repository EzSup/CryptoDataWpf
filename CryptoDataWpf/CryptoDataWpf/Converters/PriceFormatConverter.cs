using System.Globalization;
using System.Windows.Data;

namespace CryptoDataWpf.Converters
{
    public class PriceFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal d)
            {
                string format = parameter as string ?? "C";
                int decimals = Math.Max(2, GetSignificantDecimals(d, 8));
                return d.ToString(format + decimals, culture);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static int GetSignificantDecimals(decimal d, int max)
        {
            for (int i = max; i > 2; i--)
            {
                if (Math.Round(d, i) != Math.Round(d, i - 1))
                    return i;
            }
            return 2;
        }
    }
}
