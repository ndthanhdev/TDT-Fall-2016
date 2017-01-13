using DTO;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using System;

namespace StationGUI.ViewModels
{
    public partial class MainWindowViewModel
    {
        private double _axisMax;

        private double _axisMin;

        private ChartValues<DateTimePoint> _barometerChartValues;

        private bool _barometerVisibility;

        private ChartValues<DateTimePoint> _humidityChartValues;

        private bool _humidityVisibility;

        private ChartValues<DateTimePoint> _temperatureChartValues;

        private bool _temperatureVisibility;

        public double AxisMax
        {
            get { return _axisMax; }
            set { SetProperty(ref _axisMax, value); }
        }

        public double AxisMin
        {
            get { return _axisMin; }
            set { SetProperty(ref _axisMin, value); }
        }

        //chart zone
        public double AxisStep => TimeSpan.FromMinutes(1).Ticks;

        public ChartValues<DateTimePoint> BarometerChartValues
        {
            get
            {
                return _barometerChartValues ?? (_barometerChartValues = new ChartValues<DateTimePoint>());
            }
            set { SetProperty(ref _barometerChartValues, value); }
        }

        public bool BarometerVisibility
        {
            get { return _barometerVisibility; }
            set { SetProperty(ref _barometerVisibility, value); }
        }

        public Func<double, string> DateTimeFormatter { get; private set; }

        public ChartValues<DateTimePoint> HumidityChartValues
        {
            get
            {
                return _humidityChartValues ?? (_humidityChartValues = new ChartValues<DateTimePoint>());
            }
            set { SetProperty(ref _humidityChartValues, value); }
        }

        public bool HumidityVisibility
        {
            get { return _humidityVisibility; }
            set { SetProperty(ref _humidityVisibility, value); }
        }

        public ChartValues<DateTimePoint> TemperatureChartValues
        {
            get
            {
                return _temperatureChartValues ?? (_temperatureChartValues = new ChartValues<DateTimePoint>());
            }
            set { SetProperty(ref _temperatureChartValues, value); }
        }

        public bool TemperatureVisibility
        {
            get { return _temperatureVisibility; }
            set { SetProperty(ref _temperatureVisibility, value); }
        }

        private void InnerAddValueToChartValues(ChartValues<DateTimePoint> chartValues, double? value)
        {
            DateTimePoint dtp = new DateTimePoint(DateTime.Now, value.HasValue ? value.Value : double.NaN);
            chartValues.Add(dtp);
            while (chartValues.Count > 60)
                chartValues.RemoveAt(0);
        }

        private void InnerAddWeatherDataToChart(WeatherDataDTO wd)
        {
            InnerAddValueToChartValues(TemperatureChartValues, wd.Temperature);
            InnerAddValueToChartValues(HumidityChartValues, wd.Humidity);
            InnerAddValueToChartValues(BarometerChartValues, wd.Barometer);

            InnerSetAxisLimits();
        }

        private void InnerSetAxisLimits()
        {
            AxisMax = DateTime.Now.Ticks + TimeSpan.FromSeconds(5).Ticks; // lets force the axis to be 5s ahead
            AxisMin = DateTime.Now.Ticks - TimeSpan.FromMinutes(3).Ticks; //we only care about the last 3m
        }

        private void InnerSetFormatter()
        {
            DateTimeFormatter = value =>
            {
                var s = new DateTime((long)value).ToString("hh:mm:ss");
                return s;
            };
        }

        private void InnerSetMapperForChart()
        {
            var mapper = Mappers.Xy<DateTimePoint>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<DateTimePoint>(mapper);
        }
    }
}