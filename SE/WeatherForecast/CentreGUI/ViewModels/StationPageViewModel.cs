using AutoMapper;
using BUS;
using CentreGUI.Models;
using CentreGUI.Services;
using CentreGUI.Views;
using DTO;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CentreGUI.ViewModels
{
    public class StationPageViewModel : BindableBase
    {
        private CentreBUS _bus;
        private DispatcherTimer _clock;
        private int _counter;
        private int _duration = SettingServices.Current.Duration;
        private string _regionId;
        private ObservableCollection<Station> _stations;
        private ObservableCollection<Station> _stationsDG;
        private int _threshold = SettingServices.Current.Threshold;

        public StationPageViewModel(string regionId)
        {
            _regionId = regionId;
            _bus = new CentreBUS();
            InnerRefresh();
            _clock = new DispatcherTimer();
            _clock.Tick += _clock_Tick;
            _clock.Interval = TimeSpan.FromSeconds(1);
            _clock.Start();
        }

        public DelegateCommand AddStationCommand => new DelegateCommand(async () =>
        {
            PickStaffDialog dialog = new PickStaffDialog();
            if (dialog.ShowDialog() != true)
                return;
            await _bus.AddStation(_regionId, dialog.SelectedStaff.StaffId);
            InnerRefresh();
        });

        public int Counter
        {
            get { return _counter; }
            set
            {
                SetProperty(ref _counter, value);
                OnPropertyChanged(nameof(TimeLeft));
            }
        }

        public int Duration
        {
            get { return _duration; }
            set
            {
                SetProperty(ref _duration, value);
                SettingServices.Current.Duration = value;
            }
        }

        public DelegateCommand<DataGridCellEditEndingEventArgs> EditStationEnding => new DelegateCommand<DataGridCellEditEndingEventArgs>(async args =>
        {
            try
            {
                var staff = Mapper.Map<Station>(args.Row.DataContext);
                string text = (args.EditingElement as TextBox).Text;
                object value = text;
                double outDouble;
                if (double.TryParse(text, out outDouble))
                {
                    value = outDouble;
                }
                staff.GetType().GetProperty(args.Column.SortMemberPath).SetValue(staff, value);
                var dto = Mapper.Map<StationDTO>(staff);
                await _bus.EditStation(dto);
                InnerRefresh();
            }
            catch 
            {
            }
        });

        public DelegateCommand<Station> ReplaceStaffCommand => new DelegateCommand<Station>(async s =>
         {
             if (s == null)
                 return;
             PickStaffDialog dialog = new PickStaffDialog();
             if (dialog.ShowDialog() != true)
                 return;
             await _bus.ChuyenDen(s.StationId, dialog.SelectedStaff.StaffId);
             InnerRefresh();
         });

        public DelegateCommand<string> SelectedStationCommand => new DelegateCommand<string>(s =>
        {
            Navigation.Current.Navigate(new StationDetailPage(s));
        });

        public ObservableCollection<Station> Stations
        {
            get { return _stations; }
            set { SetProperty(ref _stations, value); }
        }

        public ObservableCollection<Station> StationsDG
        {
            get { return _stationsDG; }
            set { SetProperty(ref _stationsDG, value); }
        }

        public int Threshold
        {
            get { return _threshold; }
            set
            {
                SetProperty(ref _threshold, value);
                SettingServices.Current.Threshold = value;
            }
        }

        public int TimeLeft => Threshold - Counter;

        private void _clock_Tick(object sender, EventArgs e)
        {
            if (Counter < Threshold)
            {
                Counter++;
                return;
            }

            //set clock to maximun value
            Counter++;
            InnerRefresh();
            Counter = 0;
        }

        private async void InnerRefresh()
        {
            await Task.Yield();
            StationsDG = new ObservableCollection<Station>(Stations = Mapper.Map<ObservableCollection<Station>>(_bus.GetAllStationsWithLastestData(_regionId, TimeSpan.FromSeconds(SettingServices.Current.Duration))));
        }
    }
}