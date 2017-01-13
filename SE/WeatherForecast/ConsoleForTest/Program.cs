using AutoMapper;
using CentreGUI.Models;
using CentreGUI.Services;
using DAO;
using DTO;
using System.Collections.Generic;

namespace ConsoleForTest
{
    internal class Program
    {
        private static void add()
        {
            WeatherForecastDataContext db = new WeatherForecastDataContext();
            var st = new StationDTO();
            st.Distance = 99;
            db.Add(st);
            db.SaveChanges();
        }

        private static void Main(string[] args)
        {
            //add();
            //var task = StationBUS.GetStationByIdAsync("dc07fb43-b075-4291-8929-7c438fb11b7e");
            //while (!task.IsCompleted) { };
            //var station = task.Result;

            //StationBUS bus = new StationBUS(station);
            //WeatherData weatherData = new WeatherData();
            //weatherData.Temperature = 24;
            //weatherData.Barometer = 1;
            //weatherData.Humidity = 0.7f;
            //weatherData.Time = DateTime.Now;
            //bus.AddWeatherDataAsync(weatherData).Wait();

            //load();
            //bus();
            testResolve();
        }

        private static void testResolve()
        {
            MappingServices.RegisterMappings();
            var r = new RegionDTO()
            {
                Name = "clgt",
                RegionId = "lol",
                Stations = new List<StationDTO>()
            };
            var v = Mapper.Map<Region>(r);
        }

        //private static void bus()
        //{
        //    CentreBUS cb = new CentreBUS();
        //    var v = cb.GetAllRegionsWithLastestData().ToList();
        //    cb.RefreshRegion(v[0]).Wait();
        //    cb.RefreshRegion(v[0]).Wait();
        //}
        //private static void load()
        //{
        //    CentreBUS centre = new CentreBUS();
        //    var station = centre.GetAllStations().SingleOrDefault();
        //    centre.FirstWeatherData(station);
        //    centre.FirstWeatherData(station);
        //}
    }
}