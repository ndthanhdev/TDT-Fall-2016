using CentreGUI.Services;
using System.Windows;

namespace CentreGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigation.Current.Show();
            this.Close();
        }
    }
}