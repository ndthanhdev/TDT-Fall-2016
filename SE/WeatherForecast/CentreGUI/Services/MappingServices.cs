using AutoMapper;
using CentreGUI.Compute;
using CentreGUI.Models;
using DTO;
using System.Collections.Generic;
using System.Linq;

namespace CentreGUI.Services
{
    public class MappingServices
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<RegionDTO, Region>().ConvertUsing<RegionDTO2RegionConverter>();
                cfg.CreateMap<Region, RegionDTO>().ConvertUsing<Region2RegionDTOConverter>();
                cfg.CreateMap<StationDTO, Station>().ConvertUsing<StationDTO2Station>();
                cfg.CreateMap<Station, StationDTO>().ConvertUsing<Station2StationDTO>();
                cfg.CreateMap<StaffDTO, Staff>().ConvertUsing<StaffDTO2StaffConverter>();
                cfg.CreateMap<Staff, StaffDTO>().ConvertUsing<Staff2StaffDTOConverter>();
            });
            Mapper.AssertConfigurationIsValid();
        }

        private class Region2RegionDTOConverter : ITypeConverter<Region, RegionDTO>
        {
            public RegionDTO Convert(Region source, RegionDTO destination, ResolutionContext context)
            {
                destination = new RegionDTO();
                destination.Name = source.Name;
                destination.RegionId = source.RegionId;
                return destination;
            }
        }

        private class RegionDTO2RegionConverter : ITypeConverter<RegionDTO, Region>
        {
            public Region Convert(RegionDTO source, Region destination, ResolutionContext context)
            {
                destination = new Region();
                destination.Name = source.Name;
                destination.RegionId = source.RegionId;
                destination.NumberOfStation = source.Stations.Count;

                List<WeatherDataDTO> weatherdatas = new List<WeatherDataDTO>();
                int numberOfNewStatus = 0;
                foreach (var s in source.Stations)
                {
                    if (s.WeatherDatas != null && s.WeatherDatas.Count > 0)
                    {
                        weatherdatas.AddRange(s.WeatherDatas);
                    }
                    numberOfNewStatus += s.Statuss?.Count(st => st.IsNew) ?? 0;
                }
                destination.Temperature = ComputeData.AverageWeatherData(weatherdatas, WeatherDataKind.Temperature);
                destination.Humidity = ComputeData.AverageWeatherData(weatherdatas, WeatherDataKind.Humidity);
                destination.Barometer = ComputeData.AverageWeatherData(weatherdatas, WeatherDataKind.Barometer);

                destination.NumberOfNewStatus = numberOfNewStatus;

                return destination;
            }
        }

        private class Staff2StaffDTOConverter : ITypeConverter<Staff, StaffDTO>
        {
            public StaffDTO Convert(Staff source, StaffDTO destination, ResolutionContext context)
            {
                destination = new StaffDTO();
                destination.StaffId = source.StaffId;
                destination.Password = source.Password;
                destination.FullName = source.FullName;
                destination.Birthdate = source.Birthdate;
                destination.PermanentAddress = source.PermanentAddress;
                destination.Identity = source.Identity;
                destination.Salary = source.Salary;
                destination.RecruitmentDate = source.RecruitmentDate;
                return destination;
            }
        }

        private class StaffDTO2StaffConverter : ITypeConverter<StaffDTO, Staff>
        {
            public Staff Convert(StaffDTO source, Staff destination, ResolutionContext context)
            {
                destination = new Staff();
                destination.StaffId = source.StaffId;
                destination.Password = source.Password;
                destination.FullName = source.FullName;
                destination.Birthdate = source.Birthdate;
                destination.PermanentAddress = source.PermanentAddress;
                destination.Identity = source.Identity;
                destination.Salary = source.Salary;
                destination.RecruitmentDate = source.RecruitmentDate;

                if (source.Workplace != null)
                {
                    destination.Income = destination.Salary + source.Workplace.Distance * SettingServices.Current.Bonus;
                    destination.WorkplaceId = source.WorkplaceId;
                }

                return destination;
            }
        }

        private class Station2StationDTO : ITypeConverter<Station, StationDTO>
        {
            public StationDTO Convert(Station source, StationDTO destination, ResolutionContext context)
            {
                destination = new StationDTO();
                destination.StationId = source.StationId;
                destination.Name = source.Name;
                destination.Distance = source.Distance;
                return destination;
            }
        }

        private class StationDTO2Station : ITypeConverter<StationDTO, Station>
        {
            public Station Convert(StationDTO source, Station destination, ResolutionContext context)
            {
                destination = new Station();
                destination.StationId = source.StationId;
                destination.Name = source.Name;
                destination.Distance = source.Distance;
                destination.Temperature = (source.WeatherDatas?.FirstOrDefault()?.Temperature).ToDouble();
                destination.Humidity = (source.WeatherDatas?.FirstOrDefault()?.Humidity).ToDouble();
                destination.Barometer = (source.WeatherDatas?.FirstOrDefault()?.Barometer).ToDouble();

                destination.NumberOfNewStatus = source.Statuss?.Count(s => s.IsNew) ?? 0;

                return destination;
            }
        }
    }
}