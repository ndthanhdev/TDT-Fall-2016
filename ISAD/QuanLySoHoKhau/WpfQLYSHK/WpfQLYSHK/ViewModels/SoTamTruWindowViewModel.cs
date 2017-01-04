using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQLYSHK.ViewModels
{
    public class SoTamTruWindowViewModel : BindableBase, IDialog
    {
        public EventHandler RequestClose;

        public DelegateCommand Close => new DelegateCommand(() =>
        {
            Result = true;
            RequestClose(this, null);
        });

        private bool _result = false;

        public bool Result
        {
            get { return _result; }
            private set { _result = value; }
        }

        private SOTAMTRU _soTamTru;

        public SOTAMTRU SoTamTru
        {
            get { return _soTamTru ?? (_soTamTru = new SOTAMTRU()); }
            set { SetProperty(ref _soTamTru, value); }
        }

    }
}
