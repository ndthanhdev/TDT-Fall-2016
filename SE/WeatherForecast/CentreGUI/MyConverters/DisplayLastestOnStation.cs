using CentreGUI.Compute;
using DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace CentreGUI.MyConverters
{
    public class DisplayLastestOnStation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var datas = (IEnumerable<WeatherDataDTO>)value;
            var kind = (WeatherDataKind)parameter;
            return datas.First(kind);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}