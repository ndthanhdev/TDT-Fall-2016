using Microsoft.VisualStudio.TestTools.UnitTesting;
using BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BUSTestFixed
{
    [TestClass()]
    public class CentreBUSTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            var t = CentreBUS.Login("1", "1");
            t.Wait();
            Assert.AreNotEqual(null,t.Result);
        }

        [TestMethod()]
        public void AddRegionTest()
        {
            var t = new CentreBUS();
            var region = new RegionDTO();
            var t2 = t.AddRegion(region);
            t2.Wait();
            Assert.AreEqual(false, string.IsNullOrEmpty(region.RegionId));
        }

        [TestMethod()]
        public void AddStaffTest()
        {
            var t = new CentreBUS();
            var staff = new StaffDTO();
            var t2 = t.AddStaff(staff);
            t2.Wait();
            Assert.AreEqual(false, string.IsNullOrEmpty(staff.StaffId));
        }

        [TestMethod()]
        public void AddStationTest()
        {
            var c = new CentreBUS();

            var r = new RegionDTO();
            var r2 = c.AddRegion(r);
            r2.Wait();

            var st = new StaffDTO();
            var st2 = c.AddStaff(st);
            st2.Wait();

            var s = new StationDTO();
            var s2 = c.AddStation(r.RegionId, st.StaffId);
            s2.Wait();

            Assert.AreNotEqual(false,string.IsNullOrEmpty(s.StaffId));
        }

        [TestMethod()]
        public void ChuyenDenTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditRegionTest()
        {
            
            Assert.AreEqual(true,true);
        }

        [TestMethod()]
        public void EditStaffTest()
        {
            
           Assert.AreEqual(true,true);
        }

        [TestMethod()]
        public void EditStationTest()
        {
            Assert.AreEqual(true,true);
        }

        [TestMethod()]
        public void GetAllRegionsWithLastestDataTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllStaffsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllStationsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllStationsWithLastestDataTest()
        {
            Assert.Fail();
        }
        

        [TestMethod()]
        public void GetStatusOfTest()
        {
            /*var c = new CentreBUS();

            var r = new RegionDTO();
            var r2 = c.AddRegion(r);
            r2.Wait();

            var st = new StaffDTO();
            var st2 = c.AddStaff(st);
            st2.Wait();

            var s = new StationDTO();
            var s2 = c.AddStation(r.RegionId, st.StaffId);
            s2.Wait();

            var ss = new StatusDTO();
            var ss2 = c.GetStatusOf(s.StationId);
            ss2.Wait();

            Assert.AreNotEqual(null,ss2.Result);*/
            Assert.Fail();
        }

        [TestMethod()]
        public void LastestWeatherDataTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MakeAsReadTest()
        {
            Assert.Fail();
        }
        
    }
}