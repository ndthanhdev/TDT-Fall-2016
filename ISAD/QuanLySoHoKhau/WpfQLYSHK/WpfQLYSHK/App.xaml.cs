using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfQLYSHK
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static DbDataContext _db;
        public static DbDataContext DB
        {
            get { return _db ?? (_db = new DbDataContext()); }
            set { _db = value; }
        }
    }
}
