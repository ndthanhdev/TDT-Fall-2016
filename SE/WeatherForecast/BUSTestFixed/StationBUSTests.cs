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
    public class StationBUSTests
    {
        [TestMethod()]
        public void GetStationByIdAsyncTest()
        {
            var t = StationBUS.GetStationByIdAsync("1");
            t.Wait();
            Assert.AreNotEqual(null, t.Result);
        }

        [TestMethod()]
        public void LoginTest()
        {
            var t = StationBUS.Login("1", "1");
            t.Wait();
            Assert.AreNotEqual(null, t.Result);
        }

        [TestMethod()]
        public void AddStatusAsyncTest()
        {
            var t = StationBUS.GetStationByIdAsync("1");
            t.Wait();

            StationBUS bus = new StationBUS(t.Result);
            var status = new StatusDTO();

            var t2 = bus.AddStatusAsync(status);
            t2.Wait();

            Assert.AreEqual(false, string.IsNullOrEmpty(status.StatusId));
        }

        [TestMethod()]
        public void AddWeatherDataAsyncTest()
        {
            var t = StationBUS.GetStationByIdAsync("1");
            t.Wait();

            StationBUS bus = new StationBUS(t.Result);
            var weatherdata = new WeatherDataDTO();

            var t2 = bus.AddWeatherDataAsync(weatherdata);
            t2.Wait();

            Assert.AreEqual(false, string.IsNullOrEmpty(weatherdata.WeatherDataId));
        }

        [TestMethod()]
        public void GetAllStatussTest()
        {
            Assert.Fail();
        }
    }
}