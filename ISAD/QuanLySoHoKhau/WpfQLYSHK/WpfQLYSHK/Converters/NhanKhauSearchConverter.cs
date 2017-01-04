using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WpfQLYSHK.Extensions;

namespace WpfQLYSHK.Converters
{
    public class NhanKhauSearchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nk = value as NHANKHAU;
            if (string.IsNullOrEmpty(parameter.ToString()))
                return true;
            return nk.IsMatch(parameter.ToString()) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
