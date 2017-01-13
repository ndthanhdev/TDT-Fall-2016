using BUS;
using DTO;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace StationGUI.ViewModels
{
    public partial class MainWindowViewModel : BindableBase
    {
        private double _barometer;
        private StationBUS _bus;

        /// weather data zone

        private int _clock;
        private string _description;
        private double _humidity;
        private bool _isBarometerWorking;
        private bool _isHumidityWorking;
        private bool _isRefreshing;
        private bool _isSending;
        private bool _isTemperatureWorking;

        //info zone
        private StationDTO _station;

        private ObservableCollection<StatusDTO> _statuss;
        private double _temperature;

        private int _threshold;

        private DispatcherTimer _timer;

        public MainWindowViewModel(StationDTO station)
        {
            Station = station;
            _bus = new StationBUS(station);
            InnerSetDefaultProperties();

            _timer = new DispatcherTimer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();

            RefreshStatuss();
            InnerRefreshInfo();
        }

        public double Barometer
        {
            get { return _barometer; }
            set { SetProperty(ref _barometer, value); }
        }

        /// time zone
        public int Clock
        {
            get { return _clock; }
            set
            {
                SetProperty(ref _clock, value);
                OnPropertyChanged(nameof(TimeLeft));
            }
        }

        ///status zone
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public double Humidity
        {
            get { return _humidity; }
            set { SetProperty(ref _humidity, value); }
        }

        public bool IsBarometerWorking
        {
            get { return _isBarometerWorking; }
            set { SetProperty(ref _isBarometerWorking, value); }
        }

        public bool IsHumidityWorking
        {
            get { return _isHumidityWorking; }
            set { SetProperty(ref _isHumidityWorking, value); }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        public bool IsSending
        {
            get { return _isSending; }
            set { SetProperty(ref _isSending, value); }
        }

        /// check zone
        public bool IsTemperatureWorking
        {
            get { return _isTemperatureWorking; }
            set { SetProperty(ref _isTemperatureWorking, value); }
        }

        public DelegateCommand RefreshInfo => new DelegateCommand(() =>
        {
            InnerRefreshInfo();
        });

        public DelegateCommand RefreshStatus => new DelegateCommand(() =>
        {
            RefreshStatuss();
        });

        public DelegateCommand SendStatus => new DelegateCommand(async () =>
        {
            var status = InnerCreateStatus();

            Description = string.Empty;

            await _bus.AddStatusAsync(status);

            RefreshStatuss();
        });

        public StationDTO Station
        {
            get { return _station; }
            set { SetProperty(ref _station, value); }
        }

        public ObservableCollection<StatusDTO> Statuss
        {
            get { return _statuss ?? (_statuss = new ObservableCollection<StatusDTO>()); }
            set { SetProperty(ref _statuss, value); }
        }

        public double Temperature
        {
            get { return _temperature; }
            set { SetProperty(ref _temperature, value); }
        }

        public int Threshold
        {
            get { return _threshold; }
            set
            {
                SetProperty(ref _threshold, value);
                //AxisStep = TimeSpan.FromSeconds(value).Ticks;
                //OnPropertyChanged(nameof(AxisStep));
            }
        }

        public int TimeLeft => Threshold - Clock;

        private async void _timer_Tick(object sender, EventArgs e)
        {
            if (Clock < Threshold)
            {
                Clock++;
                return;
            }

            //set clock to maximun value
            Clock++;

            IsSending = true;

            var wd = InnerCreateWeatherData();
            InnerAddWeatherDataToChart(wd);
            await _bus.AddWeatherDataAsync(wd);

            // reset
            IsSending = false;
            Clock = 0;
        }

        private StatusDTO InnerCreateStatus()
        {
            StatusDTO status = new StatusDTO();
            status.Time = DateTime.Now;
            status.IsNew = true;
            status.IsTemperatureWorking = IsTemperatureWorking;
            status.IsHumidityWorking = IsHumidityWorking;
            status.IsBarometerWorking = IsBarometerWorking;
            status.Description = Description;
            return status;
        }

        private WeatherDataDTO InnerCreateWeatherData()
        {
            WeatherDataDTO wd = new WeatherDataDTO();
            wd.Time = DateTime.Now;

            if (IsTemperatureWorking)
                wd.Temperature = Temperature;

            if (IsHumidityWorking)
                wd.Humidity = Humidity;

            if (IsBarometerWorking)
                wd.Barometer = Barometer;

            return wd;
        }

        private async void InnerRefreshInfo()
        {
            var station = await StationBUS.GetStationByIdAsync(Station.StationId);
            Station = station;
        }

        private void InnerSetDefaultProperties()
        {
            /// set default
            Temperature = 25;
            Humidity = 0.7;
            Barometer = 1000;

            IsTemperatureWorking = true;
            IsHumidityWorking = true;
            IsBarometerWorking = true;

            Clock = 0;
            Threshold = 15;
            IsSending = false;

            InnerSetMapperForChart();

            InnerSetFormatter();

            InnerSetAxisLimits();

            TemperatureVisibility = true;
            HumidityVisibility = true;
            BarometerVisibility = true;
        }

        private async void RefreshStatuss()
        {
            await Task.Yield();
            IsRefreshing = true;
            var task = _bus.GetAllStatuss();
            Statuss.Clear();
            foreach (var status in task)
            {
                Statuss.Add(status);
            }
            IsRefreshing = false;
        }
    }
}