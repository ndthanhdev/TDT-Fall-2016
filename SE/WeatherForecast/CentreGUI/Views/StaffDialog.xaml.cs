using CentreGUI.ViewModels;
using DTO;
using MahApps.Metro.Controls;

namespace CentreGUI.Views
{
    /// <summary>
    /// Interaction logic for StaffDialog.xaml
    /// </summary>
    public partial class StaffDialog : MetroWindow
    {
        public StaffDialog(StaffDTO staff = null)
        {
            InitializeComponent();
            this.DataContext = new StaffDialogViewModel(staff);
        }
    }
}