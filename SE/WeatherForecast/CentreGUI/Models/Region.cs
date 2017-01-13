using Microsoft.Practices.Prism.Mvvm;

namespace CentreGUI.Models
{
    public class Region : BindableBase
    {
        private double _barometer;
        private double _humidity;
        private string _name;
        private int _numberOfStation;
        private string _regionId;
        private double _temperature;

        public double Barometer
        {
            get { return _barometer; }
            set { SetProperty(ref _barometer, value); }
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

        public int NumberOfNewStatus { get; set; }

        public int NumberOfStation
        {
            get { return _numberOfStation; }
            set { SetProperty(ref _numberOfStation, value); }
        }

        public string RegionId
        {
            get { return _regionId; }
            set { SetProperty(ref _regionId, value); }
        }

        public double Temperature
        {
            get { return _temperature; }
            set { SetProperty(ref _temperature, value); }
        }
    }
}