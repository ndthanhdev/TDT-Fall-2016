using Microsoft.Practices.Prism.Mvvm;

namespace CentreGUI.Models
{
    public class Station : BindableBase
    {
        private double _barometer;
        private double _distance;
        private double _humidity;
        private string _name;
        private int _numberOfNewStatus;
        private string _stationId;
        private double _temperature;

        public double Barometer
        {
            get { return _barometer; }
            set { SetProperty(ref _barometer, value); }
        }

        public double Distance
        {
            get { return _distance; }
            set { SetProperty(ref _distance, value); }
        }

        public double Humidity
        {
            get { return _humidity; }
            set { SetProperty(ref _humidity, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public int NumberOfNewStatus
        {
            get { return _numberOfNewStatus; }
            set { SetProperty(ref _numberOfNewStatus, value); }
        }

        public string StationId
        {
            get { return _stationId; }
            set { SetProperty(ref _stationId, value); }
        }

        public double Temperature
        {
            get { return _temperature; }
            set { SetProperty(ref _temperature, value); }
        }
    }
}