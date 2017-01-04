using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WpfQLYSHK.Extensions;

namespace WpfQLYSHK.Converters
{
    public class NhaKhauImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var nk = value as NHANKHAU;

            if (nk.IsTamVang())
            {
                return @"\Assets\Images\stt.png";
            }

            var maChuHK = nk.FindChuHoKhau();

            if (maChuHK == nk.MANHANKHAU)
            {
                return @"\Assets\Images\chuhk.png";
            }
            return @"\Assets\Images\nknoi.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
