using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuizTest
{
    public class CauHoiDeThiTest
    {
        [Fact]
        public void CreateCauHoiDeThi()
        {
            CauHoiDeThi cauHoiDeThi = new CauHoiDeThi()
            {
                CauHoi = new CauHoi(),
                CauHoiDeThiId = 0,
                DeThi = new DeThi()
            };
            Assert.NotNull(cauHoiDeThi);
        }
    }
}
