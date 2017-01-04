using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfQLYSHK.Extensions;
using WpfQLYSHK.Views;

namespace WpfQLYSHK.ViewModels
{
    public class SHKPageViewModel : BindableBase
    {
        private ObservableCollection<SOHOKHAU> _shks;
        public ObservableCollection<SOHOKHAU> SHKS
        {
            get { return (_shks ?? (_shks = new ObservableCollection<SOHOKHAU>())); }
            set { SetProperty(ref _shks, value); }
        }

        private SOHOKHAU _selectedSHK;

        public SOHOKHAU SelectedSHK
        {
            get { return _selectedSHK; }
            set { SetProperty(ref _selectedSHK, value); }
        }

        private ObservableCollection<NHANKHAU> _nhanKhauNgoais;

        public ObservableCollection<NHANKHAU> NhanKhauNgoais
        {
            get { return (_nhanKhauNgoais ?? (_nhanKhauNgoais = new ObservableCollection<NHANKHAU>())); }
            set { SetProperty(ref _nhanKhauNgoais, value); }
        }

        private NHANKHAU _selectedNK;

        public NHANKHAU SelectedNK
        {
            get { return _selectedNK; }
            set { SetProperty(ref _selectedNK, value); }
        }


        public SHKPageViewModel()
        {
        }

        public Task RefreshSoHoKhaus()
        {
            Task.Yield();
            SHKS.Clear();
            foreach (var shk in App.DB.SOHOKHAUs.OrderBy(shk => shk.MASHK))
            {
                Task.Yield();
                SHKS.Add(shk);
            }
            return Task.CompletedTask;
        }

        public Task RefreshNhanKhauNgoais()
        {
            Task.Yield();
            NhanKhauNgoais.Clear();
            foreach (var nk in App.DB.NHANKHAUs.Where(nk => nk.MASHK == null).OrderBy(nk => nk.MANHANKHAU))
            {
                Task.Yield();
                NhanKhauNgoais.Add(nk);
            }
            return Task.CompletedTask;
        }

        public Task RefreshData()
        {
            Task.WhenAll(RefreshSoHoKhaus(), RefreshNhanKhauNgoais());
            return Task.CompletedTask;
        }


        public Task RefreshData(SOHOKHAU shk)
        {
            RefreshData();
            SelectedSHK = shk;
            return Task.CompletedTask;
        }


        public DelegateCommand<SOHOKHAU> SelectSHK => new DelegateCommand<SOHOKHAU>((shk) =>
        {
            MainWindow.RootFrame.Navigate(new NhanKhauPage(shk));
        });

        public DelegateCommand AddSHK => new DelegateCommand(() =>
        {
            try
            {
                var window = new SHKWindow(null);

                window.ShowDialog();

                var viewModel = ((SHKWindowViewModel)window.DataContext);
                var shk = viewModel.SHK;

                if (!viewModel.Result)
                {
                    return;
                }

                if (App.DB.SOHOKHAUs.Any(__shk => __shk.MASHK == shk.MASHK))
                {
                    MessageBox.Show("trùng mã shk");
                    return;
                }

                App.DB.SOHOKHAUs.InsertOnSubmit(shk);
                App.DB.SubmitChanges();

                RefreshData(shk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public DelegateCommand EditSHK => new DelegateCommand(() =>
        {

            try
            {
                if (SelectedSHK == null)
                    return;

                var window = new SHKWindow(SelectedSHK);

                window.ShowDialog();

                var viewModel = ((SHKWindowViewModel)window.DataContext);

                var shk = viewModel.SHK;

                if (!viewModel.Result)
                {
                    return;
                }
                viewModel.OrginalSHK.SONHA = shk.SONHA;
                viewModel.OrginalSHK.DUONG = shk.DUONG;
                App.DB.SubmitChanges();
                RefreshData(shk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });


        public DelegateCommand AddNhanKhau => new DelegateCommand(() =>
        {
            try
            {
                var window = new NhanKhauWindow(null);
                window.ShowDialog();
                var viewModel = window.DataContext as NhanKhauWindowViewModel;

                if (!viewModel.Result)
                    return;

                App.DB.NHANKHAUs.InsertOnSubmit(viewModel.NhanKhau);
                App.DB.SubmitChanges();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        });

        public DelegateCommand NhapKhau => new DelegateCommand(() =>
        {
            try
            {
                if (SelectedNK == null)
                    return;

                PickSHKWindow window = new PickSHKWindow();
                window.ShowDialog();

                var viewModel = window.DataContext as PickSHKWindowViewModel;
                if (!viewModel.Result)
                    return;

                var window2 = new PhieuNhapKhauWindow();
                window2.ShowDialog();

                var viewModel2 = window2.DataContext as PhieuNhapKhauWindowViewModel;
                if (!viewModel2.Result)
                    return;

                //chuyen nk vao shk
                SelectedNK.QUANHEVOICHUHO = viewModel2.QuanHeVoiChuHo;      //quan he voi chu ho
                SelectedNK.SOHOKHAU = viewModel.SelectedSHK;

                //tạo phiếu nhập khẩu
                viewModel2.PhieuNhapKhau.NGAYCHUYENDEN = DateTime.Now;
                viewModel2.PhieuNhapKhau.NHANKHAU = SelectedNK;
                viewModel2.PhieuNhapKhau.SOHOKHAU = viewModel.SelectedSHK;

                App.DB.PHIEUNHAPKHAUs.InsertOnSubmit(viewModel2.PhieuNhapKhau);

                App.DB.SubmitChanges();


                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        });








        private ObservableCollection<NHANKHAU> _searchNhanKhaus;

        public ObservableCollection<NHANKHAU> NhanKhaus
        {
            get { return _searchNhanKhaus ?? (_searchNhanKhaus = new ObservableCollection<NHANKHAU>()); }
            set { SetProperty(ref _searchNhanKhaus, value); }
        }

        private ObservableCollection<SOTAMTRU> _searchTamTrus;

        public ObservableCollection<SOTAMTRU> TamTrus
        {
            get { return _searchTamTrus ?? (_searchTamTrus = new ObservableCollection<SOTAMTRU>()); }
            set { SetProperty(ref _searchTamTrus, value); }
        }




        Task RefreshNhanKhaus()
        {
            NhanKhaus.Clear();
            foreach (var nk in App.DB.NHANKHAUs.OrderBy(nk => nk.MANHANKHAU))
            {
                NhanKhaus.Add(nk);
            }
            return Task.CompletedTask;
        }

        Task RefreshTamTrus()
        {
            TamTrus.Clear();
            foreach (var tt in App.DB.SOTAMTRUs)
            {
                TamTrus.Add(tt);
            }
            return Task.CompletedTask;
        }


        public DelegateCommand<string> SearchNhanKhau => new DelegateCommand<string>(async (s) =>
        {
            await RefreshNhanKhaus();
            if (string.IsNullOrEmpty(s))
                return;
            for (int i = 0; i < NhanKhaus.Count; i++)
            {
                if (!NhanKhaus[i].IsMatch(s))
                {
                    NhanKhaus.RemoveAt(i);
                    i--;
                }
            }
        });

        public DelegateCommand<string> SearchTamTru => new DelegateCommand<string>(async (s) =>
        {
            await RefreshTamTrus();
            if (string.IsNullOrEmpty(s))
                return;
            for (int i = 0; i < TamTrus.Count; i++)
            {
                if (!TamTrus[i].IsMatch(s))
                {
                    TamTrus.RemoveAt(i);
                    i--;
                }
            }
        });

        public DelegateCommand<NHANKHAU> SelectNKNgoai => new DelegateCommand<NHANKHAU>((nk) =>
        {
            NhanKhauDetailWindow window = new NhanKhauDetailWindow(nk);
            window.ShowDialog();
        });


    }
}
