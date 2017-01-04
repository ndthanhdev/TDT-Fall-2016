using MahApps.Metro.Controls;
using Microsoft.Reporting.WinForms;
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

namespace WpfQLYSHK.Views
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : MetroWindow
    {
        SOHOKHAU _shk;
        public ReportWindow(SOHOKHAU shk)
        {
            _shk = shk;
            InitializeComponent();
            _reportViewer.Load += _reportViewer_Load;
        }

        private void _reportViewer_Load(object sender, EventArgs e)
        {
            var datasource = new ReportDataSource("DataSet1", (new DbDataContext())
                .NHANKHAUs.Where(nk => nk.MASHK == _shk.MASHK));
            _reportViewer.LocalReport.DataSources.Add(datasource);
            _reportViewer.LocalReport.ReportEmbeddedResource = "WpfQLYSHK.Reports.ReportNhanKhau.rdlc";
            _reportViewer.RefreshReport();
        }
    }
}
