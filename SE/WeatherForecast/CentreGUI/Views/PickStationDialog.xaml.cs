using AutoMapper;
using BUS;
using CentreGUI.Models;
using DTO;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace CentreGUI.Views
{
    /// <summary>
    /// Interaction logic for PickStationDialog.xaml
    /// </summary>
    public partial class PickStationDialog : Window
    {
        public PickStationDialog()
        {
            InitializeComponent();
            this.DataContext = new PickStationDialogViewModel();
        }

        public Station SelectedStation => (DataContext as PickStationDialogViewModel).SelectedStation;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }

    public class PickStationDialogViewModel : BindableBase
    {
        private CentreBUS _bus = new CentreBUS();

        private ObservableCollection<Station> _stations;

        public PickStationDialogViewModel()
        {
            InnerRefresh();
        }

        public Station SelectedStation { get; set; }

        public ObservableCollection<Station> Stations
        {
            get { return _stations; }
            set { SetProperty(ref _stations, value); }
        }

        private async void InnerRefresh()
        {
            await Task.Yield();
            var value = await _bus.GetAllStations();
            Stations = Mapper.Map<IEnumerable<StationDTO>, ObservableCollection<Station>>((IEnumerable<StationDTO>)value);
        }
    }
}