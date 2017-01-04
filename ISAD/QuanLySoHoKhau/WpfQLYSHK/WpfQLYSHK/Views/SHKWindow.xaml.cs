using MahApps.Metro.Controls;
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
using System.Windows.Shapes;
using WpfQLYSHK.ViewModels;

namespace WpfQLYSHK.Views
{
    /// <summary>
    /// Interaction logic for SHKWindow.xaml
    /// </summary>
    public partial class SHKWindow : MetroWindow
    {
        public SHKWindow(SOHOKHAU shk)
        {
            InitializeComponent();
            var context = new SHKWindowViewModel(shk);
            context.RequestClose += (sender, e) => this.Close();
            this.DataContext = context;
        }

    }
}
