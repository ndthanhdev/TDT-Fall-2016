using BUS;
using DTO;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using StationGUI.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StationGUI.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        private string _id;

        private bool _isBusy;

        private string _status;

        private Action closeAction;

        public LoginWindowViewModel(Action close)
        {
            closeAction += close;
        }

        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public DelegateCommand<PasswordBox> LoginCommand => new DelegateCommand<PasswordBox>(InnerLogin);

        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        public async void InnerLogin(PasswordBox passwordBox)
        {
            try
            {
                IsBusy = true;
                Status = "đang đăng nhập";
                await Task.Delay(2000);
                StaffDTO staff = await StationBUS.Login(Id, passwordBox.Password);
                if (staff == null)
                {
                    //fail
                    Status = "sai id hoặc mật khẩu";
                    return;
                }
                if (staff.Workplace == null)
                {
                    //no station
                    Status = "không có trạm";
                    return;
                }
                Status = "đăng nhập thành công đang chuyển...";

                MainWindow window = new MainWindow(staff.Workplace);
                await Task.Delay(2000);
                closeAction();
                window.Show();
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
    }
}