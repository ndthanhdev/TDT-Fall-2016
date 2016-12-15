using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuizTest
{
    public class NhomCauHoiTest
    {
        [Fact]
        public void CreateNhomCauHoi()
        {
            Nhom nhom = new Nhom()
            {
                CauHois = new List<CauHoi>(),
                NhomId = 0,
                Ten = string.Empty
            };
            Assert.NotNull(nhom);
        }
    }
}
