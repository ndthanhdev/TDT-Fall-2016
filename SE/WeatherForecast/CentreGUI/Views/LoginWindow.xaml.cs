using CentreGUI.ViewModels;
using MahApps.Metro.Controls;

namespace CentreGUI.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            var vm = new LoginWindowViewModel(this.Close);
            this.DataContext = vm;
        }
    }
}