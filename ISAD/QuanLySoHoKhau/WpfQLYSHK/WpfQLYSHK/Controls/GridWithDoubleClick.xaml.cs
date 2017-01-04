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

namespace WpfQLYSHK.Controls
{
    /// <summary>
    /// Interaction logic for GridWithDoubleClick.xaml
    /// </summary>
    public partial class GridWithDoubleClick : Grid
    {
        public event EventHandler<MouseButtonEventArgs> MouseLeftDoubleClick;

        public GridWithDoubleClick()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseLeftDoubleClick != null && e.ClickCount >= 2)
                MouseLeftDoubleClick.Invoke(this, e);
        }
    }
}
