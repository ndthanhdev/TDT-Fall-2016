using CentreGUI.ViewModels;
using System.Windows.Controls;

namespace CentreGUI.Views
{
    /// <summary>
    /// Interaction logic for StationPage.xaml
    /// </summary>
    public partial class StationPage : Page
    {
        public StationPage(string regionId)
        {
            InitializeComponent();
            DataContext = new StationPageViewModel(regionId);
        }
    }
}