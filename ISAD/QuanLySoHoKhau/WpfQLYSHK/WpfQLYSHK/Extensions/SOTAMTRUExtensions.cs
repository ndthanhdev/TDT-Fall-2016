using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQLYSHK.Extensions
{
    public static class SOTAMTRUExtensions
    {
        public static bool IsMatch(this SOTAMTRU stt, string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;
            if (stt.HOTEN != null && stt.HOTEN.ToLower().Contains(s.ToLower()))
                return true;
            if (stt.CMND != null && stt.CMND.ToLower().Contains(s.ToLower()))
                return true;
            return false;
        }
    }
}
