using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQLYSHK.Extensions
{
    public static class NHANKHAUExtensions
    {
        public static int? FindChuHoKhau(this NHANKHAU nk)
        {
            var shk = nk.SOHOKHAU;

            if (shk == null)
                return null;

            var ordered = shk.PHIEUTHAYDOICHUHOs.OrderByDescending(p => p.NGAYDOI.Value);

            return ordered.ElementAtOrDefault(0)?.MANHANKHAU;
        }

        public static bool IsTamVang(this NHANKHAU nk)
        {
            if (App.DB.GIAYTAMVANGs.Any(gtv => gtv.MANHANKHAU == nk.MANHANKHAU
                                         && gtv.NGAYVE > DateTime.Now))
            {
                return true;
            }
            return false;
        }


        public static bool IsMatch(this NHANKHAU nk, string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            if (nk.HOTEN != null && nk.HOTEN.ToLower().Contains(input.ToLower()))
                return true;
            if (nk.CMND != null && nk.CMND.ToLower().Contains(input.ToLower()))
                return true;
            return false;
        }

    }
}
