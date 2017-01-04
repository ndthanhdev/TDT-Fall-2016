using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfQLYSHK.ViewModels
{
    public class SHKWindowViewModel : BindableBase, IDialog
    {
        private SOHOKHAU _shk;
        public SOHOKHAU SHK
        {
            get { return (_shk ?? (_shk = new SOHOKHAU())); }
            set
            {
                SetProperty(ref _shk, value);
            }
        }

        public SOHOKHAU OrginalSHK { get; set; }

        public EventHandler RequestClose;

        public SHKWindowViewModel(SOHOKHAU shk)
        {
            OrginalSHK = shk;
            if (OrginalSHK != null)
            {
                SHK = new SOHOKHAU()
                {
                    MASHK = OrginalSHK.MASHK,
                    DUONG = OrginalSHK.DUONG,
                    SONHA = OrginalSHK.SONHA
                };
            }
        }
        

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

    }
}
