using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace MovieManager {
    public class IndexToVisibilityConverter : IValueConverter {
        #region IValueConverter Member

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Visibility res = Visibility.Visible;
            if (value.ToString() == "-1") {
                res = Visibility.Hidden;
            }
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }

        #endregion
    }
}
