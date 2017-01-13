using AutoMapper;
using CentreGUI.Models;
using CentreGUI.Views;
using DTO;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CentreGUI.ViewModels
{
    public partial class RegionPageViewModel
    {
        private ObservableCollection<Staff> _staffs;

        public DelegateCommand<AddingNewItemEventArgs> AddingStaffCommand => new DelegateCommand<AddingNewItemEventArgs>(async args =>
        {
            var staff = new StaffDTO();
            staff.FullName = "name";
            staff.Password = "password";
            staff.Birthdate = DateTime.Now;
            staff.PermanentAddress = "address";
            staff.Identity = "cmnd";
            staff.Salary = 0;
            staff.RecruitmentDate = DateTime.Now;
            await _bus.AddStaff(staff);
            args.NewItem = Mapper.Map<StaffDTO, Staff>(staff);
        });

        public DelegateCommand<Staff> ChuyenDenCommand => new DelegateCommand<Staff>(s =>
         {
             PickStationDialog window = new PickStationDialog();
             if (window.ShowDialog() != true)
             {
                 return;
             };
         });

        public DelegateCommand<DataGridCellEditEndingEventArgs> EditStaffEndingCommand => new DelegateCommand<DataGridCellEditEndingEventArgs>(async args =>
        {
            try
            {
                var staff = Mapper.Map<Staff>(args.Row.DataContext);
                string text = (args.EditingElement as TextBox).Text;
                object value = null;
                switch (args.Column.SortMemberPath)
                {
                    case nameof(Staff.FullName):
                    case nameof(Staff.Identity):
                    case nameof(Staff.Password):
                    case nameof(Staff.PermanentAddress):
                        value = text;
                        break;

                    case nameof(Staff.Salary):
                        value = double.Parse(text);
                        break;

                    case nameof(Staff.Birthdate):
                    case nameof(Staff.RecruitmentDate):
                        value = DateTime.Parse(text);
                        break;

                    default:
                        break;
                }
                staff.GetType().GetProperty(args.Column.SortMemberPath).SetValue(staff, value);
                var staffDTO = Mapper.Map<StaffDTO>(staff);
                await _bus.EditStaff(staffDTO);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                InneRefreshStaff();
            }
        });

        public ObservableCollection<Staff> Staffs
        {
            get { return _staffs ?? (_staffs = new ObservableCollection<Staff>()); }
            set { SetProperty(ref _staffs, value); }
        }

        private async void InneRefreshStaff()
        {
            await Task.Yield();
            Staffs = Mapper.Map<IEnumerable<StaffDTO>, ObservableCollection<Staff>>(_bus.GetAllStaffs());
        }
    }
}