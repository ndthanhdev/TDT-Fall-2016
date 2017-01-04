using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQLYSHK.ViewModels
{
    public class NhanKhauDetailWindowViewModel : BindableBase
    {
        NHANKHAU _nk;

        public NHANKHAU NhanKhau
        {
            get { return _nk; }
            set { SetProperty(ref _nk, value); }
        }

        public NhanKhauDetailWindowViewModel(NHANKHAU nk)
        {
            _nk = nk;
        }

        public NHANKHAU Mother
        {
            get
            {
                return App.DB.GIAYKHAISINHs
                    .SingleOrDefault(gks => gks.MANHANKHAUBE == _nk.MANHANKHAU)
                    ?.NHANKHAU1;
            }
            set { }
        }

        public IEnumerable<PHIEUTACHKHAU> TachKhaus
        {
            get
            {
                return App.DB.PHIEUTACHKHAUs
                    .Where(ptk => ptk.MANHANKHAU == NhanKhau.MANHANKHAU)
                    .OrderByDescending(ptk => ptk.NGAYCHUYENDI);
            }
        }

        public IEnumerable<NHANKHAU> Cons
        {
            get
            {
                return App.DB.GIAYKHAISINHs
                    .Where(gks => gks.MANHANKHAUME == NhanKhau.MANHANKHAU)
                    .Select(gks => gks.NHANKHAU);
            }
        }
    }
}
