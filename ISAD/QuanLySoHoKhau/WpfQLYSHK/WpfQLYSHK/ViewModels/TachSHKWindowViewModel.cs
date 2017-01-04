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
    //public class TachSHKWindowViewModel :  BindableBase, IDialog
    //{
    //    private SOHOKHAU _shk;
    //    public SOHOKHAU SHK
    //    {
    //        get { return (_shk ?? (_shk = new SOHOKHAU())); }
    //        set
    //        {
    //            SetProperty(ref _shk, value);
    //        }
    //    }

    //    public SOHOKHAU OrginalSHK { get; set; }


    //    public EventHandler RequestClose;

    //    public DelegateCommand Close => new DelegateCommand(() =>
    //    {
    //        Result = true;
    //        RequestClose(this, null);
    //    });

    //    private bool _result = false;

    //    public bool Result
    //    {
    //        get { return _result; }
    //        private set { _result = value; }
    //    }

    //    private PHIEUTACHHOKHAU _pthk;

    //    public PHIEUTACHHOKHAU PTHK
    //    {
    //        get { return (_pthk ?? (_pthk = new PHIEUTACHHOKHAU())); }
    //        set { SetProperty(ref _pthk, value); }
    //    }


    //    public TachSHKWindowViewModel(SOHOKHAU shk)
    //    {
    //        OrginalSHK = shk;
    //        SHK = new SOHOKHAU();
    //        PTHK.MASHKCU = OrginalSHK.MASHK;            
    //    }

    //    public DelegateCommand TachSHK => new DelegateCommand(() =>
    //    {
    //        try
    //        {
    //            if(SHK.MASHK==string.Empty
    //            ||App.DB.SOHOKHAUs.Any(shk=>shk.MASHK==SHK.MASHK)
    //            ||PTHK.MAPHIEUTACHHOKHAU == string.Empty
    //            ||App.DB.PHIEUTACHHOKHAUs.Any(pthk=> pthk.MAPHIEUTACHHOKHAU == PTHK.MAPHIEUTACHHOKHAU))
    //            {
    //                MessageBox.Show("lỗi");
    //                return;
    //            }

    //            App.DB.SOHOKHAUs.InsertOnSubmit(SHK);

    //            PTHK.MASHKCU = OrginalSHK.MASHK;
    //            PTHK.MASHK = SHK.MASHK;

    //            App.DB.PHIEUTACHHOKHAUs.InsertOnSubmit(PTHK);

    //            App.DB.SubmitChanges();

    //            RequestClose(this, new EventArgs());

    //        }
    //        catch
    //        {
    //            MessageBox.Show("lỗi");
    //        }
    //    });

        
    //}
}
