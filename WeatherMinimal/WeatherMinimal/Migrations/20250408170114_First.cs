using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherMinimal.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clouds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    all = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clouds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lon = table.Column<double>(type: "float", nullable: false),
                    lat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Main",
                columns: table => new
                {
                    IdKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    temp = table.Column<double>(type: "float", nullable: false),
                    feels_like = table.Column<double>(type: "float", nullable: false),
                    temp_min = table.Column<double>(type: "float", nullable: false),
                    temp_max = table.Column<double>(type: "float", nullable: false),
                    pressure = table.Column<int>(type: "int", nullable: false),
                    humidity = table.Column<int>(type: "int", nullable: false),
                    sea_level = table.Column<int>(type: "int", nullable: false),
                    grnd_level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Main", x => x.IdKey);
                });

            migrationBuilder.CreateTable(
                name: "Rain",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _h = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sunrise = table.Column<int>(type: "int", nullable: false),
                    sunset = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    IdKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    main = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    icon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.IdKey);
                });

            migrationBuilder.CreateTable(
                name: "Wind",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    speed = table.Column<double>(type: "float", nullable: false),
                    deg = table.Column<int>(type: "int", nullable: false),
                    gust = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wind", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    coordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    @base = table.Column<string>(name: "base", type: "nvarchar(max)", nullable: false),
                    mainIdKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    visibility = table.Column<int>(type: "int", nullable: false),
                    windId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cloudsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dt = table.Column<int>(type: "int", nullable: false),
                    sysId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    timezone = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.id);
                    table.ForeignKey(
                        name: "FK_Results_Clouds_cloudsId",
                        column: x => x.cloudsId,
                        principalTable: "Clouds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Coord_coordId",
                        column: x => x.coordId,
                        principalTable: "Coord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Main_mainIdKey",
                        column: x => x.mainIdKey,
                        principalTable: "Main",
                        principalColumn: "IdKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Rain_rainId",
                        column: x => x.rainId,
                        principalTable: "Rain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Sys_sysId",
                        column: x => x.sysId,
                        principalTable: "Sys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Wind_windId",
                        column: x => x.windId,
                        principalTable: "Wind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityWeatherResultWeather",
                columns: table => new
                {
                    Resultsid = table.Column<int>(type: "int", nullable: false),
                    weatherIdKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityWeatherResultWeather", x => new { x.Resultsid, x.weatherIdKey });
                    table.ForeignKey(
                        name: "FK_CityWeatherResultWeather_Results_Resultsid",
                        column: x => x.Resultsid,
                        principalTable: "Results",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityWeatherResultWeather_Weather_weatherIdKey",
                        column: x => x.weatherIdKey,
                        principalTable: "Weather",
                        principalColumn: "IdKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityWeatherResultWeather_weatherIdKey",
                table: "CityWeatherResultWeather",
                column: "weatherIdKey");

            migrationBuilder.CreateIndex(
                name: "IX_Results_cloudsId",
                table: "Results",
                column: "cloudsId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_coordId",
                table: "Results",
                column: "coordId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_mainIdKey",
                table: "Results",
                column: "mainIdKey");

            migrationBuilder.CreateIndex(
                name: "IX_Results_rainId",
                table: "Results",
                column: "rainId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_sysId",
                table: "Results",
                column: "sysId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_windId",
                table: "Results",
                column: "windId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityWeatherResultWeather");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropTable(
                name: "Clouds");

            migrationBuilder.DropTable(
                name: "Coord");

            migrationBuilder.DropTable(
                name: "Main");

            migrationBuilder.DropTable(
                name: "Rain");

            migrationBuilder.DropTable(
                name: "Sys");

            migrationBuilder.DropTable(
                name: "Wind");
        }
    }
}
