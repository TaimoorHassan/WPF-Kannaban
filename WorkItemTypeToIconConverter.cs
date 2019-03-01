using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace Kanaban
{
    public class WorkItemTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ListType t)
            {
                if (t == ListType.TODO)
                {
                    return PackIconKind.CheckboxBlankOutline;
                }
                if (t == ListType.Working)
                {
                    return PackIconKind.CheckboxBlankCircleOutline;
                }
                if (t == ListType.Done)
                {
                    return PackIconKind.CheckboxMarkedOutline;
                }

                return null;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }

    public class WorkItemTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ListType t)
            {
                if (t == ListType.TODO)
                {
                    return Brushes.DarkSlateBlue;
                }
                if (t == ListType.Working)
                {
                    return Brushes.DarkSlateGray;
                }
                if (t == ListType.Done)
                {
                    return Brushes.DarkGreen;
                }

                return null;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    
}