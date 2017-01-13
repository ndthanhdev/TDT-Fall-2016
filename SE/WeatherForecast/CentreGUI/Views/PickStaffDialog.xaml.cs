using CentreGUI.Models;
using CentreGUI.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;

namespace CentreGUI.Views
{
    /// <summary>
    /// Interaction logic for PickStaffDialog.xaml
    /// </summary>
    public partial class PickStaffDialog : MetroWindow
    {
        public PickStaffDialog()
        {
            InitializeComponent();
            this.DataContext = new PickStaffDialogViewModel();
        }

        public Staff SelectedStaff => (DataContext as PickStaffDialogViewModel).SelectedStaff;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}