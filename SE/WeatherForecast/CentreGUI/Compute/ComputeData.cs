using DTO;
using System.Collections.Generic;
using System.Linq;

namespace CentreGUI.Compute
{
    public enum WeatherDataKind
    {
        Temperature,
        Humidity,
        Barometer
    }

    public static class ComputeData
    {
        public static double AverageNullable(IEnumerable<double?> doubles)
        {
            if (doubles == null || doubles.Count(d => d.HasValue) < 1)
                return double.NaN;
            double sum = 0;
            foreach (var d in doubles)
                sum += d.HasValue ? d.Value : 0;
            return sum / doubles.Count(d => d.HasValue);
        }

        public static double AverageWeatherData(IEnumerable<WeatherDataDTO> datas, WeatherDataKind kind)
        {
            if (datas == null || datas.Count() < 1)
                return double.NaN;
            switch (kind)
            {
                case WeatherDataKind.Temperature:
                    var temperatures = datas.Select(d => d.Temperature);
                    return AverageNullable(temperatures);

                case WeatherDataKind.Humidity:
                    var humiditys = datas.Select(d => d.Humidity);
                    return AverageNullable(humiditys);

                case WeatherDataKind.Barometer:
                    var barometers = datas.Select(d => d.Barometer);
                    return AverageNullable(barometers);
            }
            return double.NaN;
        }

        public static double First(this IEnumerable<WeatherDataDTO> weatherdatas, WeatherDataKind dataKind)
        {
            var first = weatherdatas?.FirstOrDefault();
            if (first == null)
                return double.NaN;
            switch (dataKind)
            {
                case WeatherDataKind.Temperature: return first.Temperature.ToDouble();
                case WeatherDataKind.Humidity: return first.Humidity.ToDouble();
                case WeatherDataKind.Barometer: return first.Barometer.ToDouble();
            }
            return double.NaN;
        }

        public static bool IsFirstValueNotNull(IEnumerable<double?> doubles)
        {
            if (doubles == null || doubles.Count() < 1)
                return false;
            return doubles.First().HasValue;
        }

        public static double ToDouble(this double? value)
        {
            if (value.HasValue)
                return value.Value;
            return double.NaN;
        }
    }
}