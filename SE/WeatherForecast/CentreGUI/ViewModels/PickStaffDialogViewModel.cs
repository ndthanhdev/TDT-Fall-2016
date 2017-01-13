using AutoMapper;
using BUS;
using CentreGUI.Models;
using DTO;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CentreGUI.ViewModels
{
    public class PickStaffDialogViewModel : BindableBase
    {
        private CentreBUS _bus = new CentreBUS();

        private ObservableCollection<Staff> _staffs;

        public PickStaffDialogViewModel()
        {
            InnerRefresh();
        }

        public Staff SelectedStaff { get; set; }

        public ObservableCollection<Staff> Staffs
        {
            get { return _staffs; }
            set { SetProperty(ref _staffs, value); }
        }

        private async void InnerRefresh()
        {
            await Task.Yield();
            var value = _bus.GetAllStaffs(true);
            Staffs = Mapper.Map<IEnumerable<StaffDTO>, ObservableCollection<Staff>>(value);
        }
    }
}