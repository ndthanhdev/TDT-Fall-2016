using DTO;
using MahApps.Metro.Controls;
using StationGUI.ViewModels;
using System.Threading.Tasks;

namespace StationGUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(StationDTO station)
        {
            InitializeComponent();
            InnerInitializeContext(station);
        }

        private async void InnerInitializeContext(StationDTO station)
        {
            await Task.Yield();
            MainWindowViewModel vm = new MainWindowViewModel(station);
            this.DataContext = vm;
        }
    }
}