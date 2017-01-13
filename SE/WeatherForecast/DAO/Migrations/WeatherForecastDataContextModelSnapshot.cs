using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DAO;

namespace DAO.Migrations
{
    [DbContext(typeof(WeatherForecastDataContext))]
    partial class WeatherForecastDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-preview1-22509");

            modelBuilder.Entity("DTO.JobHistoryDTO", b =>
                {
                    b.Property<string>("JobHistoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("StaffId")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("WorkplaceId")
                        .IsRequired();

                    b.HasKey("JobHistoryId");

                    b.HasIndex("StaffId");

                    b.HasIndex("WorkplaceId");

                    b.ToTable("JobHistorys");
                });

            modelBuilder.Entity("DTO.RegionDTO", b =>
                {
                    b.Property<string>("RegionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("RegionId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("DTO.StaffDTO", b =>
                {
                    b.Property<string>("StaffId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("Identity")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PermanentAddress")
                        .IsRequired();

                    b.Property<DateTime>("RecruitmentDate");

                    b.Property<double>("Salary");

                    b.Property<string>("WorkplaceId");

                    b.HasKey("StaffId");

                    b.HasIndex("WorkplaceId")
                        .IsUnique();

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("DTO.StationDTO", b =>
                {
                    b.Property<string>("StationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<double>("Distance");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("RegionId")
                        .IsRequired();

                    b.Property<string>("StaffId");

                    b.HasKey("StationId");

                    b.HasIndex("RegionId");

                    b.ToTable("Stations");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Station");
                });

            modelBuilder.Entity("DTO.StatusDTO", b =>
                {
                    b.Property<string>("StatusId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsBarometerWorking");

                    b.Property<bool>("IsHumidityWorking");

                    b.Property<bool>("IsNew");

                    b.Property<bool>("IsTemperatureWorking");

                    b.Property<string>("StationId");

                    b.Property<DateTime>("Time");

                    b.HasKey("StatusId");

                    b.HasIndex("StationId");

                    b.ToTable("Statuss");
                });

            modelBuilder.Entity("DTO.WeatherDataDTO", b =>
                {
                    b.Property<string>("WeatherDataId")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Barometer");

                    b.Property<double?>("Humidity");

                    b.Property<string>("StationId");

                    b.Property<double?>("Temperature");

                    b.Property<DateTime>("Time");

                    b.HasKey("WeatherDataId");

                    b.HasIndex("StationId");

                    b.ToTable("WeatherDatas");
                });

            modelBuilder.Entity("DTO.CentreDTO", b =>
                {
                    b.HasBaseType("DTO.StationDTO");


                    b.ToTable("CentreDTO");

                    b.HasDiscriminator().HasValue("Centre");
                });

            modelBuilder.Entity("DTO.JobHistoryDTO", b =>
                {
                    b.HasOne("DTO.StaffDTO", "Staff")
                        .WithMany("JobHistorys")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.StationDTO", "Workplace")
                        .WithMany("JobHistorys")
                        .HasForeignKey("WorkplaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.StaffDTO", b =>
                {
                    b.HasOne("DTO.StationDTO", "Workplace")
                        .WithOne("Staff")
                        .HasForeignKey("DTO.StaffDTO", "WorkplaceId");
                });

            modelBuilder.Entity("DTO.StationDTO", b =>
                {
                    b.HasOne("DTO.RegionDTO", "Region")
                        .WithMany("Stations")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.StatusDTO", b =>
                {
                    b.HasOne("DTO.StationDTO", "Station")
                        .WithMany("Statuss")
                        .HasForeignKey("StationId");
                });

            modelBuilder.Entity("DTO.WeatherDataDTO", b =>
                {
                    b.HasOne("DTO.StationDTO", "Station")
                        .WithMany("WeatherDatas")
                        .HasForeignKey("StationId");
                });
        }
    }
}
