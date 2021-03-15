using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Itk.Tweaker.Ui.Components
{
    public sealed class ToggleVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Visibility visibility) return Visibility.Collapsed;
            return visibility == Visibility.Visible ? Visibility.Collapsed: Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Visibility visibility) return Visibility.Visible;
            return visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}