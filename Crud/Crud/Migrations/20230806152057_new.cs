using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crud.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aircrafts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirCraftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirCraftModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AircraftNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AircrftCapacity = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircrafts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CabinCrew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinCrew", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contactus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CoPilot",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoPilot", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRequests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRequests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pilot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestAirCraft",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AircraftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capaciy = table.Column<int>(type: "int", nullable: false),
                    AmountNeeded = table.Column<int>(type: "int", nullable: false),
                    ReqDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAirCraft", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Technician",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technician", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Joinddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoggedInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoggedOutTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PilotId = table.Column<int>(type: "int", nullable: true),
                    CoPilotid = table.Column<int>(type: "int", nullable: true),
                    Aircraftid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.id);
                    table.ForeignKey(
                        name: "FK_Flights_Aircrafts_Aircraftid",
                        column: x => x.Aircraftid,
                        principalTable: "Aircrafts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Flights_CoPilot_CoPilotid",
                        column: x => x.CoPilotid,
                        principalTable: "CoPilot",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Flights_Pilot_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Pilot",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CabinCrewFlight",
                columns: table => new
                {
                    CabinCrewsId = table.Column<int>(type: "int", nullable: false),
                    Flightsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinCrewFlight", x => new { x.CabinCrewsId, x.Flightsid });
                    table.ForeignKey(
                        name: "FK_CabinCrewFlight_CabinCrew_CabinCrewsId",
                        column: x => x.CabinCrewsId,
                        principalTable: "CabinCrew",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinCrewFlight_Flights_Flightsid",
                        column: x => x.Flightsid,
                        principalTable: "Flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightTechnician",
                columns: table => new
                {
                    Flightsid = table.Column<int>(type: "int", nullable: false),
                    TechniciansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightTechnician", x => new { x.Flightsid, x.TechniciansId });
                    table.ForeignKey(
                        name: "FK_FlightTechnician_Flights_Flightsid",
                        column: x => x.Flightsid,
                        principalTable: "Flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightTechnician_Technician_TechniciansId",
                        column: x => x.TechniciansId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CabinCrewFlight_Flightsid",
                table: "CabinCrewFlight",
                column: "Flightsid");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Aircraftid",
                table: "Flights",
                column: "Aircraftid");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_CoPilotid",
                table: "Flights",
                column: "CoPilotid");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PilotId",
                table: "Flights",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightTechnician_TechniciansId",
                table: "FlightTechnician",
                column: "TechniciansId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabinCrewFlight");

            migrationBuilder.DropTable(
                name: "Contactus");

            migrationBuilder.DropTable(
                name: "FlightTechnician");

            migrationBuilder.DropTable(
                name: "MedicalRequests");

            migrationBuilder.DropTable(
                name: "RequestAirCraft");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CabinCrew");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Technician");

            migrationBuilder.DropTable(
                name: "Aircrafts");

            migrationBuilder.DropTable(
                name: "CoPilot");

            migrationBuilder.DropTable(
                name: "Pilot");
        }
    }
}
