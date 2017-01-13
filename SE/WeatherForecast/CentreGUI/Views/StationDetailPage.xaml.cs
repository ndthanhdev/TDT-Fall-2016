using CentreGUI.ViewModels;
using System.Windows.Controls;

namespace CentreGUI.Views
{
    /// <summary>
    /// Interaction logic for StaionDetailPage.xaml
    /// </summary>
    public partial class StationDetailPage : Page
    {
        public StationDetailPage(string stationId)
        {
            InitializeComponent();
            this.DataContext = new StationDetailPageViewModel(stationId);
        }
    }
}