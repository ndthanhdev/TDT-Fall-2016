using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQLYSHK.ViewModels
{
    public class PhieuTuWindowViewModel : BindableBase, IDialog
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

        private PHIEUTU _phieuTu;

        public PHIEUTU PhieuTu
        {
            get { return _phieuTu ?? (_phieuTu = new PHIEUTU()); }
            set { SetProperty(ref _phieuTu, value); }
        }

    }
}
