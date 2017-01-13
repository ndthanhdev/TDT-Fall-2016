using MahApps.Metro.Controls;
using StationGUI.ViewModels;

namespace StationGUI.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginWindowViewModel(this.Close);
        }
    }
}