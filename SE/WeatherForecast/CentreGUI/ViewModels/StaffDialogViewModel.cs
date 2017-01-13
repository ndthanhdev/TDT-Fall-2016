using DTO;
using Microsoft.Practices.Prism.Mvvm;

namespace CentreGUI.ViewModels
{
    public class StaffDialogViewModel : BindableBase
    {
        private StaffDTO _staff;

        public StaffDialogViewModel(StaffDTO staff = null)
        {
            Staff = staff;
        }

        public StaffDTO Staff
        {
            get { return _staff ?? (_staff = new StaffDTO()); }
            set { SetProperty(ref _staff, value); }
        }
    }
}