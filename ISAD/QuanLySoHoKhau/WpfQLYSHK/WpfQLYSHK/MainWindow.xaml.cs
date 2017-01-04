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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfQLYSHK.Views;

namespace WpfQLYSHK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static Frame RootFrame;

        public MainWindow()
        {
            //Random rd = new Random(10000);
            //using (DbDataContext db = new DbDataContext())
            //{
            //    for (int i = 1001; i < 1500; i++)
            //        db.SOHOKHAUs.InsertOnSubmit(new SOHOKHAU()
            //        {
            //            MASHK = i.ToString()
            //            ,
            //            DUONG = rd.Next().ToString()
            //            ,
            //            SONHA = rd.Next()
            //        });
            //    db.SubmitChanges();
            //}

            InitializeComponent();
            _frameRoot.Navigate(new LoginPage());
            RootFrame = _frameRoot;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.GoBack();
        }
    }
}
