using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfQLYSHK.ViewModels;

namespace WpfQLYSHK.Views
{
    /// <summary>
    /// Interaction logic for SHKPage.xaml
    /// </summary>
    public partial class SHKPage : Page
    {
        public SHKPage()
        {
            InitializeComponent();
            this.Loaded += SHKPage_Loaded;
        }

        private void SHKPage_Loaded(object sender, RoutedEventArgs e)
        {
            ((SHKPageViewModel)this.DataContext).RefreshData();
        }
    }
}
