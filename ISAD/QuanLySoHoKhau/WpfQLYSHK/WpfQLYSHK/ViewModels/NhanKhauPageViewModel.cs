using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfQLYSHK.Views;
using WpfQLYSHK.Extensions;

namespace WpfQLYSHK.ViewModels
{
    public class NhanKhauPageViewModel : BindableBase
    {
        private ObservableCollection<NHANKHAU> _nhankhaus;

        public ObservableCollection<NHANKHAU> NhanKhaus
        {
            get { return _nhankhaus ?? (_nhankhaus = new ObservableCollection<NHANKHAU>()); }
            set { SetProperty(ref _nhankhaus, value); }
        }


        private NHANKHAU _selectedNhanKhau;

        public NHANKHAU SelectedNhanKhau
        {
            get { return _selectedNhanKhau; }
            set { _selectedNhanKhau = value; }
        }


        private ObservableCollection<SOTAMTRU> _soTamTrus;

        public ObservableCollection<SOTAMTRU> SoTamTrus
        {
            get { return _soTamTrus ?? (_soTamTrus = new ObservableCollection<SOTAMTRU>()); }
            set { SetProperty(ref _soTamTrus, value); }
        }



        private SOHOKHAU _shk;

        public SOHOKHAU SoHoKhau
        {
            get { return _shk; }
            set { SetProperty(ref _shk, value); }
        }

        public NhanKhauPageViewModel(SOHOKHAU shk)
        {
            SoHoKhau = shk;
            RefreshData();
        }

        Task RefreshData()
        {
            return Task.WhenAll(RefreshNhanKhaus(), RefreshSoTamTrus());
        }

        Task RefreshNhanKhaus()
        {
            Task.Yield();
            NhanKhaus.Clear();
            foreach (var nk in App.DB.NHANKHAUs.Where(nk => nk.MASHK == SoHoKhau.MASHK).OrderBy(nk => nk.MANHANKHAU))
            {
                Task.Yield();
                NhanKhaus.Add(nk);
            }
            return Task.CompletedTask;
        }

        Task RefreshSoTamTrus()
        {
            Task.Yield();
            SoTamTrus.Clear();
            foreach (var nk in App.DB.SOTAMTRUs.Where(stt => stt.MASHK == SoHoKhau.MASHK).OrderBy(stt => stt.MASOTAMTRU))
            {
                Task.Yield();
                SoTamTrus.Add(nk);
            }
            return Task.CompletedTask;
        }




        public DelegateCommand DatLamChuHo => new DelegateCommand(() =>
        {
            try
            {
                if (SelectedNhanKhau == null)
                    return;
                var window = new PhieuTDChuHoWindow();
                window.ShowDialog();

                var viewmodel = window.DataContext as PhieuTDChuHoWindowViewModel;
                if (!viewmodel.Result)
                    return;

                viewmodel.PhieuThayDoiChuHo.SOHOKHAU = SoHoKhau;
                viewmodel.PhieuThayDoiChuHo.NHANKHAU = SelectedNhanKhau;
                viewmodel.PhieuThayDoiChuHo.NGAYDOI = DateTime.Now;

                App.DB.PHIEUTHAYDOICHUHOs.InsertOnSubmit(viewmodel.PhieuThayDoiChuHo);

                App.DB.SubmitChanges();

                App.DB.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, App.DB.NHANKHAUs);

                RefreshData();

            }
            catch
            {

            }
        });

        public DelegateCommand TachKhau => new DelegateCommand(() =>
        {
            if (SelectedNhanKhau == null)
                return;
            var window = new PhieuTachKhauWindow();
            window.ShowDialog();

            var viewModel = window.DataContext as PhieuTachKhauWindowViewModel;
            if (!viewModel.Result)
            {
                return;
            }

            SelectedNhanKhau.SOHOKHAU = null;

            viewModel.PhieuTachKhau.NGAYCHUYENDI = DateTime.Now;
            viewModel.PhieuTachKhau.NHANKHAU = SelectedNhanKhau;

            App.DB.SubmitChanges();

            RefreshData();
        });

        public DelegateCommand ChungTu => new DelegateCommand(() =>
        {
            if (SelectedNhanKhau == null)
                return;
            var window = new PhieuTuWindow();
            window.ShowDialog();
            var viewModel = window.DataContext as PhieuTuWindowViewModel;
            if (!viewModel.Result)
            {
                return;
            }

            SelectedNhanKhau.SOHOKHAU = null;

            viewModel.PhieuTu.NGAYMAT = DateTime.Now;
            viewModel.PhieuTu.NHANKHAU = SelectedNhanKhau;

            App.DB.SubmitChanges();

            RefreshData();
        });

        public DelegateCommand TamVang => new DelegateCommand(() =>
        {
            if (SelectedNhanKhau == null)
                return;
            var window = new GiayTamVangWindow();
            window.ShowDialog();

            var viewModel = window.DataContext as GiayTamVangWindowViewModel;
            if (!viewModel.Result)
            {
                return;
            }

            viewModel.GiayTamVang.NHANKHAU = SelectedNhanKhau;

            App.DB.GIAYTAMVANGs.InsertOnSubmit(viewModel.GiayTamVang);
            App.DB.SubmitChanges();

            RefreshData();
        });

        public DelegateCommand KhaiSinh => new DelegateCommand(() =>
        {
            try
            {

                if (SelectedNhanKhau == null)
                    return;

                var window = new NhanKhauWindow(null);
                window.ShowDialog();
                var viewModel = window.DataContext as NhanKhauWindowViewModel;

                if (!viewModel.Result)
                    return;

                //thêm nhân khẩu    
                App.DB.NHANKHAUs.InsertOnSubmit(viewModel.NhanKhau);

                App.DB.SubmitChanges();

                //cho nk vào hộ khẩu
                viewModel.NhanKhau.SOHOKHAU = SoHoKhau;

                //thêm giấy khai sinh
                GIAYKHAISINH gks = new GIAYKHAISINH();
                gks.MANHANKHAUBE = viewModel.NhanKhau.MANHANKHAU;
                gks.MANHANKHAUME = SelectedNhanKhau.MANHANKHAU;
                App.DB.GIAYKHAISINHs.InsertOnSubmit(gks);

                App.DB.SubmitChanges();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });



        public DelegateCommand ThemSoTamTru => new DelegateCommand(() =>
        {
            var window = new SoTamTruWindow();
            window.ShowDialog();
            var viewModel = window.DataContext as SoTamTruWindowViewModel;
            if (!viewModel.Result)
            {
                return;
            }


            viewModel.SoTamTru.SOHOKHAU = SoHoKhau;

            App.DB.SOTAMTRUs.InsertOnSubmit(viewModel.SoTamTru);
            App.DB.SubmitChanges();

            RefreshData();
        });


        public DelegateCommand Report => new DelegateCommand(() =>
        {
            ReportWindow rw = new ReportWindow(SoHoKhau);
            rw.ShowDialog();
        });

        public DelegateCommand<NHANKHAU> Detail => new DelegateCommand<NHANKHAU>((nk) =>
        {
            NhanKhauDetailWindow window = new NhanKhauDetailWindow(nk);
            window.ShowDialog();
        });

        public IEnumerable<PHIEUTHAYDOICHUHO> PhieuThayDoiChuHos
        {
            get
            {
                return App.DB.PHIEUTHAYDOICHUHOs
                    .Where(ptdch => ptdch.MASHK == SoHoKhau.MASHK)
                    .OrderByDescending(ptdch => ptdch.NGAYDOI);
            }
        }

        public IEnumerable<PHIEUNHAPKHAU> PhieuNhapKhaus
        {
            get
            {
                return App.DB.PHIEUNHAPKHAUs
                    .Where(pnk => pnk.MASHK == SoHoKhau.MASHK)
                    .OrderByDescending(pnk => pnk.NGAYCHUYENDEN);
            }
        }

    }
}
