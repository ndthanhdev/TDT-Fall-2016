using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuizTest
{
    public class DapAnTest
    {
        [Fact]
        public void CreateDapAn()
        {
            DapAn dapAn = new DapAn()
            {
                CauHoi = new CauHoi(),
                CauHoiId = 0,
                DapAnId = 0,
                IsTrue = false,
                NoiDung = string.Empty
            };
            Assert.NotNull(dapAn);
        }
    }
}
