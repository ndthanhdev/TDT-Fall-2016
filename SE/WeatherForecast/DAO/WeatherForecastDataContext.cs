using DTO;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class WeatherForecastDataContext : DbContext
    {
        private const string CONNECTION_STRING = @"Data Source=C:\Users\thanh\Desktop\ws.db";

        public DbSet<JobHistoryDTO> JobHistorys { get; set; }

        public DbSet<RegionDTO> Regions { get; set; }

        public DbSet<StaffDTO> Staffs { get; set; }

        public DbSet<StationDTO> Stations { get; set; }

        public DbSet<StatusDTO> Statuss { get; set; }

        public DbSet<WeatherDataDTO> WeatherDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(CONNECTION_STRING);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StationDTO>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<StationDTO>("Station")
                .HasValue<CentreDTO>("Centre");
        }
    }
}