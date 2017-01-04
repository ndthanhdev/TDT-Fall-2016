using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQLYSHK.ViewModels
{
    public class PickSHKWindowViewModel : BindableBase, IDialog
    {

        public EventHandler RequestClose;

        public DelegateCommand Close => new DelegateCommand(() =>
        {
            if (SelectedSHK == null)
                return;
            Result = true;
            RequestClose(this, null);
        });

        private bool _result = false;

        public bool Result
        {
            get { return _result; }
            private set { _result = value; }
        }

        private ObservableCollection<SOHOKHAU> _shks;

        public ObservableCollection<SOHOKHAU> SHKS
        {
            get { return _shks; }
            set { _shks = value; }
        }

        public SOHOKHAU SelectedSHK { get; set; }

        public PickSHKWindowViewModel()
        {
            SHKS = new ObservableCollection<SOHOKHAU>(App.DB.SOHOKHAUs);
            SelectedSHK = null;
        }

    }


}
