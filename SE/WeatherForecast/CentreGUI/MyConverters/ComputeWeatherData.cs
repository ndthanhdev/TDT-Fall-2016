using CentreGUI.Compute;
using DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace CentreGUI.MyConverters
{
    public class ComputeWeatherData : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var datas = (IEnumerable<WeatherDataDTO>)value;
            var kind = (WeatherDataKind)parameter;
            var result = ComputeData.AverageWeatherData(datas, kind);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}