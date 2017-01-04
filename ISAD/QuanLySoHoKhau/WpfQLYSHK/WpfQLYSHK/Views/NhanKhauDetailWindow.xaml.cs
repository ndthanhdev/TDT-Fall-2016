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
    /// Interaction logic for NhanKhauDetailWindow.xaml
    /// </summary>
    public partial class NhanKhauDetailWindow : MetroWindow
    {
        public NhanKhauDetailWindow(NHANKHAU nk)
        {
            InitializeComponent();
            NhanKhauDetailWindowViewModel vm = new NhanKhauDetailWindowViewModel(nk);
            this.DataContext = vm;
        }
    }
}
