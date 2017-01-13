using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace CentreGUI.MyControls
{
    public class GridEx : Grid
    {
        public GridEx()
        {
            base.MouseLeftButtonDown += GridEx_MouseLeftButtonDown;
        }

        public event EventHandler<MouseButtonEventArgs> MouseLeftDoubleClick;

        private void GridEx_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseLeftDoubleClick != null && e.ClickCount >= 2)
                MouseLeftDoubleClick.Invoke(this, e);
        }
    }
}