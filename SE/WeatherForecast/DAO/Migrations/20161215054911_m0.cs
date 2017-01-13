using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAO.Migrations
{
    public partial class m0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RegionId = table.Column<string>(nullable: false),
                    StaffId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                    table.ForeignKey(
                        name: "FK_Stations_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffId = table.Column<string>(nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    Identity = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    PermanentAddress = table.Column<string>(nullable: false),
                    RecruitmentDate = table.Column<DateTime>(nullable: false),
                    Salary = table.Column<double>(nullable: false),
                    WorkplaceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_Staffs_Stations_WorkplaceId",
                        column: x => x.WorkplaceId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statuss",
                columns: table => new
                {
                    StatusId = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsBarometerWorking = table.Column<bool>(nullable: false),
                    IsHumidityWorking = table.Column<bool>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    IsTemperatureWorking = table.Column<bool>(nullable: false),
                    StationId = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuss", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_Statuss_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WeatherDatas",
                columns: table => new
                {
                    WeatherDataId = table.Column<string>(nullable: false),
                    Barometer = table.Column<double>(nullable: true),
                    Humidity = table.Column<double>(nullable: true),
                    StationId = table.Column<string>(nullable: true),
                    Temperature = table.Column<double>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherDatas", x => x.WeatherDataId);
                    table.ForeignKey(
                        name: "FK_WeatherDatas_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobHistorys",
                columns: table => new
                {
                    JobHistoryId = table.Column<string>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    StaffId = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    WorkplaceId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobHistorys", x => x.JobHistoryId);
                    table.ForeignKey(
                        name: "FK_JobHistorys_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobHistorys_Stations_WorkplaceId",
                        column: x => x.WorkplaceId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobHistorys_StaffId",
                table: "JobHistorys",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHistorys_WorkplaceId",
                table: "JobHistorys",
                column: "WorkplaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_WorkplaceId",
                table: "Staffs",
                column: "WorkplaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stations_RegionId",
                table: "Stations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuss_StationId",
                table: "Statuss",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherDatas_StationId",
                table: "WeatherDatas",
                column: "StationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobHistorys");

            migrationBuilder.DropTable(
                name: "Statuss");

            migrationBuilder.DropTable(
                name: "WeatherDatas");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
