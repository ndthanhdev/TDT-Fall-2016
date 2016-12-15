using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuizTest
{
    public class CauHoiTest
    {
        [Fact]
        public void CreateCauHoi()
        {
            CauHoi cauHoi = new CauHoi()
            {
                DapAns = new List<DapAn>(),
                Nhom = new Nhom(),
                NoiDung = string.Empty
            };
            Assert.NotNull(cauHoi);
        }
    }
}
