using DAO;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUS
{
    public class StationBUS : IDisposable
    {
        private WeatherForecastDataContext _db;
        private StationDTO _station;

        public StationBUS(StationDTO station)
        {
            _db = new WeatherForecastDataContext();
            _station = station;
        }

        public static async Task<StationDTO> GetStationByIdAsync(string stationId, bool isIncludeWeatherDatas = true,
            bool isIncludeStatuss = true,
            bool isIncludeStaff = true,
            bool isIncludeRegion = true)
        {
            using (var db = new WeatherForecastDataContext())
            {
                var entity = db.Stations.SingleOrDefault(station => station.StationId == stationId);

                List<Task> loadTasks = new List<Task>();

                if (isIncludeWeatherDatas)
                    loadTasks.Add(db.Entry(entity).Collection(e => e.WeatherDatas).LoadAsync());

                if (isIncludeStatuss)
                    loadTasks.Add(db.Entry(entity).Collection(e => e.Statuss).LoadAsync());

                if (isIncludeStaff)
                    loadTasks.Add(db.Entry(entity).Reference(e => e.Staff).LoadAsync());

                if (isIncludeRegion)
                    loadTasks.Add(db.Entry(entity).Reference(e => e.Region).LoadAsync());

                await Task.WhenAll(loadTasks);

                return entity;
            }
        }

        public static async Task<StaffDTO> Login(string id, string password)
        {
            using (WeatherForecastDataContext db = new WeatherForecastDataContext())
            {
                return await db.Staffs.Include(s => s.Workplace)
                    .SingleOrDefaultAsync(s => s.StaffId == id && s.Password == password);
            }
        }

        public Task AddStatusAsync(StatusDTO status)
        {
            status.StationId = _station.StationId;
            _db.Add(status);
            return _db.SaveChangesAsync();
        }

        public Task AddWeatherDataAsync(WeatherDataDTO weatherData)
        {
            weatherData.StationId = _station.StationId;
            _db.Add(weatherData);
            return _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<StatusDTO> GetAllStatuss()
        {
            return _db.Statuss.OrderByDescending(status => status.Time);
        }
    }
}