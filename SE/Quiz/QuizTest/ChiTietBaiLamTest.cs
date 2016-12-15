using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuizTest
{
    public class ChiTietBaiLamTest
    {
        [Fact]
        public void CreateChiTietBaiLam()
        {
            ChiTietBaiLam chiTietBaiLam = new ChiTietBaiLam()
            {
                ChiTietBaiLamId = 0,
                DapAn = new DapAn(),
                DapAnId = 0
            };
            Assert.NotNull(chiTietBaiLam);
        }
    }
}
