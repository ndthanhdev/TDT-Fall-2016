using BUS;
using DTO;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.ObjectModel;

namespace CentreGUI.ViewModels
{
    public class StationDetailPageViewModel : BindableBase
    {
        private CentreBUS _bus;
        private string _stationId;

        private ObservableCollection<StatusDTO> _statuss;

        public StationDetailPageViewModel(string stationId)
        {
            _stationId = stationId;
            _bus = new CentreBUS();
            InnerRefresh();
        }

        public DelegateCommand<StatusDTO> SelectedStatusCommand => new DelegateCommand<StatusDTO>(async s =>
        {
            if (s != null)
            {
                await _bus.MakeAsRead(s.StatusId);
                InnerRefresh();
            }
        });

        public ObservableCollection<StatusDTO> Statuss
        {
            get { return _statuss; }
            set { SetProperty(ref _statuss, value); }
        }

        public async void InnerRefresh()
        {
            Statuss = new ObservableCollection<StatusDTO>(await _bus.GetStatusOf(_stationId));
        }
    }
}