using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQLYSHK.ViewModels
{
    public class PhieuNhapKhauWindowViewModel : BindableBase, IDialog
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

        private PHIEUNHAPKHAU _phieuNhapKhau;

        public PHIEUNHAPKHAU PhieuNhapKhau
        {
            get { return (_phieuNhapKhau ?? (_phieuNhapKhau = new PHIEUNHAPKHAU())); }
            set { SetProperty(ref _phieuNhapKhau, value); }
        }

        private string _quanHeVoiChuHo;

        public string QuanHeVoiChuHo
        {
            get { return _quanHeVoiChuHo; }
            set { _quanHeVoiChuHo = value; }
        }



    }
}
