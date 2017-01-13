using AutoMapper;
using BUS;
using CentreGUI.Models;
using CentreGUI.Services;
using CentreGUI.Views;
using DTO;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CentreGUI.ViewModels
{
    public partial class RegionPageViewModel : BindableBase
    {
        private double _bonus = SettingServices.Current.Bonus;
        private CentreBUS _bus;

        private DispatcherTimer _clock;
        private int _counter;
        private int _duration = SettingServices.Current.Duration;
        private ObservableCollection<Region> _regions;
        private ObservableCollection<Region> _regionsDG;
        private int _threshold = SettingServices.Current.Threshold;

        public RegionPageViewModel()
        {
            _bus = new CentreBUS();

            InnerRefreshRegions();
            InneRefreshStaff();
            _clock = new DispatcherTimer();
            _clock.Tick += _clock_Tick;
            _clock.Interval = TimeSpan.FromSeconds(1);
            _clock.Start();
        }

        public DelegateCommand<AddingNewItemEventArgs> AddingRegion => new DelegateCommand<AddingNewItemEventArgs>(async args =>
        {
            var dto = new RegionDTO();
            dto.Name = "ten khu vuc";
            await _bus.AddRegion(dto);
            InnerRefreshRegions();
        });

        public double Bonus
        {
            get { return _bonus = SettingServices.Current.Bonus; }
            set
            {
                SetProperty(ref _bonus, SettingServices.Current.Bonus = value);
            }
        }

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

        public DelegateCommand<DataGridCellEditEndingEventArgs> EditRegionEnding => new DelegateCommand<DataGridCellEditEndingEventArgs>(async args =>
        {
            try
            {
                var region = Mapper.Map<Region>(args.Row.DataContext);
                region.Name = (args.EditingElement as TextBox).Text;
                await _bus.EditRegion(Mapper.Map<RegionDTO>(region));
            }
            catch { }
            finally
            {
                InnerRefreshRegions();
            }
        });

        public DelegateCommand ExportExcelCommand => new DelegateCommand(() =>
        {
            ExcelServices.CreateFile(Regions);
        });

        public ObservableCollection<Region> Regions
        {
            get { return _regions ?? (_regions = new ObservableCollection<Region>()); }
            set { SetProperty(ref _regions, value); }
        }

        public ObservableCollection<Region> RegionsDG
        {
            get { return _regionsDG ?? (_regionsDG = new ObservableCollection<Region>()); }
            set { SetProperty(ref _regionsDG, value); }
        }

        public DelegateCommand<Region> SelectRegionCommand => new DelegateCommand<Region>(async r =>
        {
            await Task.Yield();
            Navigation.Current.Navigate(new StationPage(r.RegionId));
        });

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
            InnerRefreshRegions();
            InneRefreshStaff();
        }

        private async void InnerRefreshRegions()
        {
            await Task.Yield();
            var rs = _bus.GetAllRegionsWithLastestData(TimeSpan.FromSeconds(Duration));

            RegionsDG = new ObservableCollection<Region>(Regions = Mapper.Map<IEnumerable<RegionDTO>, ObservableCollection<Region>>(rs));
        }
    }
}