using DAO;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BUS
{
    public class CentreBUS
    {
        private WeatherForecastDataContext _db;

        public CentreBUS()
        {
            _db = new WeatherForecastDataContext();
        }

        public static async Task<bool> Login(string id, string password)
        {
            using (WeatherForecastDataContext db = new WeatherForecastDataContext())
            {
                var staff = await db.Staffs.Include(s => s.Workplace)
                    .SingleOrDefaultAsync(s => s.StaffId == id && s.Password == password);
                return staff != null && staff.Workplace is CentreDTO;
            }
        }

        public async Task<bool> AddRegion(RegionDTO region)
        {
            try
            {
                using (WeatherForecastDataContext db = new WeatherForecastDataContext())
                {
                    await db.Regions.AddAsync(region);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddStaff(StaffDTO staff)
        {
            try
            {
                await _db.Staffs.AddAsync(staff);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddStation(string regionId, string staffId)
        {
            try
            {
                using (WeatherForecastDataContext db = new WeatherForecastDataContext())
                {
                    StationDTO station = new StationDTO();

                    station.RegionId = regionId;
                    station.Name = "name";

                    await db.Stations.AddAsync(station);

                    await db.SaveChangesAsync();

                    await ChuyenDen(station.StationId, staffId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> ChuyenDen(string stationId, string staffId)
        {
            try
            {
                using (WeatherForecastDataContext db = new WeatherForecastDataContext())
                {
                    if (!db.Staffs.Any(s => s.StaffId == staffId))
                        return false;

                    if (!db.Stations.Any(s => s.StationId == stationId))
                        return false;

                    foreach (var s in db.Staffs.Where(s => s.WorkplaceId == stationId).ToList() ?? new List<StaffDTO>())
                        s.Workplace = null;

                    foreach (var jh in db.JobHistorys.Where(jh => jh.WorkplaceId == stationId && jh.EndDate == null).ToList() ?? new List<JobHistoryDTO>())
                        jh.EndDate = DateTime.Now;

                    (await db.Staffs.Include(s => s.Workplace).SingleAsync(s => s.StaffId == staffId)).WorkplaceId = stationId;

                    (await db.Stations.Include(s => s.Staff).SingleAsync(s => s.StationId == stationId)).StaffId = staffId;

                    JobHistoryDTO dto = new JobHistoryDTO();
                    dto.StaffId = staffId;
                    dto.WorkplaceId = stationId;
                    dto.StartDate = DateTime.Now;
                    db.JobHistorys.Add(dto);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> EditRegion(RegionDTO region)
        {
            try
            {
                using (WeatherForecastDataContext db = new WeatherForecastDataContext())
                {
                    var inner = db.Regions.SingleOrDefault(r => r.RegionId == region.RegionId);
                    inner.Name = region.Name;
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public Task EditStaff(StaffDTO staff)
        {
            using (WeatherForecastDataContext db = new WeatherForecastDataContext())
            {
                var innerStaff = db.Staffs.SingleOrDefault(s => s.StaffId == staff.StaffId);
                if (innerStaff == null)
                    return Task.CompletedTask;
                innerStaff.Password = staff.Password;
                innerStaff.FullName = staff.FullName;
                innerStaff.Birthdate = staff.Birthdate;
                innerStaff.PermanentAddress = staff.PermanentAddress;
                innerStaff.Identity = staff.Identity;
                innerStaff.Salary = staff.Salary;
                innerStaff.RecruitmentDate = staff.RecruitmentDate;
                return db.SaveChangesAsync();
            }
        }

        public Task EditStation(StationDTO station)
        {
            using (WeatherForecastDataContext db = new WeatherForecastDataContext())
            {
                var inner = db.Stations.SingleOrDefault(s => s.StationId == station.StationId);
                if (inner == null)
                    return Task.CompletedTask;
                inner.Name = station.Name;
                inner.Distance = station.Distance;
                return db.SaveChangesAsync();
            }
        }

        public IEnumerable<RegionDTO> GetAllRegionsWithLastestData(TimeSpan duration)
        {
            using (WeatherForecastDataContext db = new WeatherForecastDataContext())
            {
                var regions = db.Regions.Include(r => r.Stations);

                foreach (var r in regions)
                {
                    foreach (var s in r.Stations)
                    {
                        LastestWeatherData(s, duration, db);
                        GetNewStatus(s, db);
                    }
                    //_db.Entry(r).State = EntityState.Detached;
                }
                return regions.ToList();
            }
        }

        public IEnumerable<StaffDTO> GetAllStaffs(bool isFree = false)
        {
            using (WeatherForecastDataContext db = new WeatherForecastDataContext())
            {
                if (isFree)
                    return db.Staffs.Where(s => s.Workplace == null).Include(s => s.Workplace).ToList();
                return db.Staffs.Include(s => s.Workplace).ToList();
            }
        }

        public Task<List<StationDTO>> GetAllStations()
        {
            using (WeatherForecastDataContext db = new WeatherForecastDataContext())
            {
                return db.Stations.ToListAsync();
            }
        }

        public IEnumerable<StationDTO> GetAllStationsWithLastestData(string regionId, TimeSpan duration)
        {
            using (WeatherForecastDataContext db = new WeatherForecastDataContext())
            {
                var stations = db.Stations.Where(s => s.RegionId == regionId);
                foreach (var s in stations)
                {
                    LastestWeatherData(s, duration, db);
                    GetNewStatus(s, db);
                }
                return stations.ToList();
            }
        }

        //public void LastestWeatherData(StationDTO station, TimeSpan duration)
        //{
        //    station.WeatherDatas?.ForEach(wd => _db.Entry(wd).State = EntityState.Detached);
        //    station.WeatherDatas?.Clear();
        //    _db.Entry(station)
        //           .Collection(s => s.WeatherDatas)
        //           .Query()
        //           .Where(wd => wd.Time.Add(duration) >= DateTime.Now)
        //           .OrderByDescending(wd => wd.Time)
        //           .Take(1)
        //           .Load();
        //}

        public void GetNewStatus(StationDTO station, WeatherForecastDataContext db)
        {
            station.Statuss?.ForEach(wd => db.Entry(wd).State = EntityState.Detached);
            station.Statuss?.Clear();
            db.Entry(station)
                   .Collection(s => s.Statuss)
                   .Query()
                   .Where(st => st.IsNew)
                   .OrderByDescending(st => st.Time)
                   .Load();
        }

        public async Task<IEnumerable<StatusDTO>> GetStatusOf(string stationId)
        {
            using (WeatherForecastDataContext db = new WeatherForecastDataContext())
            {
                var station = await db.Stations.Include(s => s.Statuss).SingleOrDefaultAsync(s => s.StationId == stationId);
                return station?.Statuss.OrderByDescending(st => st.Time).ToList();
            }
        }

        public void LastestWeatherData(StationDTO station, TimeSpan duration, WeatherForecastDataContext db)
        {
            station.WeatherDatas?.ForEach(wd => db.Entry(wd).State = EntityState.Detached);
            station.WeatherDatas?.Clear();
            db.Entry(station)
                   .Collection(s => s.WeatherDatas)
                   .Query()
                   .Where(wd => wd.Time.Add(duration) >= DateTime.Now)
                   .OrderByDescending(wd => wd.Time)
                   .Take(1)
                   .Load();
        }

        public Task MakeAsRead(string statusId)
        {
            using (WeatherForecastDataContext db = new WeatherForecastDataContext())
            {
                var s = db.Statuss.SingleOrDefault(st => st.StatusId == statusId);
                if (s != null)
                {
                    s.IsNew = false;
                    return db.SaveChangesAsync();
                }
                return Task.CompletedTask;
            }
        }

        public async Task RefreshRegion(RegionDTO region)
        {
            await Task.Yield();
            await _db.Entry(region).ReloadAsync();
            await _db.Entry(region).Collection(r => r.Stations)
                .Query()
                .LoadAsync();

            //if (region.Stations != null)
            //{
            //    foreach (var s in region.Stations ?? new List<Station>())
            //        _db.Entry(s).State = EntityState.Detached;
            //    region.Stations.Clear();
            //}
        }
    }
}