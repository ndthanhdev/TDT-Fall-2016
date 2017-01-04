using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfQLYSHK;

namespace WpfQLYSHK.ViewModels
{
    public class NhanKhauWindowViewModel : BindableBase, IDialog
    {
        private NHANKHAU _nhanKhau;

        public NHANKHAU NhanKhau
        {
            get { return (_nhanKhau ?? (_nhanKhau = new NHANKHAU())); }
            set { SetProperty(ref _nhanKhau, value); }
        }

        public EventHandler RequestClose;

        public NhanKhauWindowViewModel(NHANKHAU nk)
        {
            NhanKhau = nk;
        }

        public DelegateCommand Close => new DelegateCommand(() =>
        {
            Result = true;
            RequestClose(this, new EventArgs());
        });

        private bool _result = false;

        public bool Result
        {
            get { return _result; }
            private set { _result = value; }
        }

    }
}
