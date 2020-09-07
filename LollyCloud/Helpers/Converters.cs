using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace LollyCloud
{
    // https://stackoverflow.com/questions/20707160/data-binding-int-property-to-enum-in-wpf
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            int returnValue = 0;
            if (parameter is Type)
            {
                returnValue = (int)Enum.Parse((Type)parameter, value.ToString());
            }
            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            Enum enumValue = default(Enum);
            if (parameter is Type)
            {
                enumValue = (Enum)Enum.Parse((Type)parameter, value.ToString());
            }
            return enumValue;
        }
    }
    // https://stackoverflow.com/questions/397556/how-to-bind-radiobuttons-to-an-enum
    public class ComparisonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.Equals(true) == true ? parameter : Binding.DoNothing;
        }
    }
}
