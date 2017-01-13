using CentreGUI.ViewModels;
using System.Windows.Controls;

namespace CentreGUI.Views
{
    /// <summary>
    /// Interaction logic for RegionPage.xaml
    /// </summary>
    public partial class RegionPage : Page
    {
        public RegionPage()
        {
            InitializeComponent();
            this.DataContext = new RegionPageViewModel();
        }
    }
}