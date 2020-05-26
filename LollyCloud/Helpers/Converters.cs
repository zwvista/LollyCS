using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace LollyCloud
{
    // https://stackoverflow.com/questions/47871745/wpf-change-datagrid-cell-background-color-using-a-converter
    public class LevelToBackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var vmSettings = values[0] as SettingsViewModel;
            var level = values[1] as int? ?? 0;
            if (level == 0) return SystemColors.ControlLightColor;
            var color = (Color)ColorConverter.ConvertFromString("#" + vmSettings.USLEVELCOLORS[level][0]);
            return new SolidColorBrush(color);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class LevelToForegroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var vmSettings = values[0] as SettingsViewModel;
            var level = values[1] as int? ?? 0;
            if (level == 0) return SystemColors.ControlTextColor;
            var color = (Color)ColorConverter.ConvertFromString("#" + vmSettings.USLEVELCOLORS[level][1]);
            return new SolidColorBrush(color);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
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
