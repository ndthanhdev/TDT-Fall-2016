using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuizTest
{
    public class DeThiTest
    {
        [Fact]
        public void CreateDeThi()
        {
            DeThi deThi = new DeThi()
            {
                BaiLams = new List<BaiLam>(),
                CauHoiDeThis = new List<CauHoiDeThi>(),
                DeThiId = 0,
                Ten = string.Empty,
                ThoiGian = TimeSpan.FromDays(1)
            };
            Assert.NotNull(deThi);
        }
    }
}
