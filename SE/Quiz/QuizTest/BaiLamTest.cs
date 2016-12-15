using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace QuizTest
{
    public class BaiLamTest
    {

        [Fact]
        public void CreateBaiLam()
        {
            BaiLam baiLam = new BaiLam()
            {
                BaiLamId = 0,
                ChiTietBaiLams = new List<ChiTietBaiLam>(),
                DeThi= new DeThi(),
                User = new ApplicationUser()
            };
            
            Assert.NotNull(baiLam);
        }
    }

}
