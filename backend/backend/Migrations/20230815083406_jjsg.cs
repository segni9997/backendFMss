using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class jjsg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirCraftRequest",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AircraftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capaciy = table.Column<int>(type: "int", nullable: false),
                    AmountNeeded = table.Column<int>(type: "int", nullable: false),
                    Added = table.Column<int>(type: "int", nullable: false),
                    ReqDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReqStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalSatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirCraftRequest", x => x.id);
                });

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
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AirCraftLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Technicianresult = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircrafts", x => x.id);
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
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Roleid = table.Column<int>(type: "int", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Joinddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoggedInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoggedOutTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Roleid",
                        column: x => x.Roleid,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Userid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.id);
                    table.ForeignKey(
                        name: "FK_Admins_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabinCrew",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cabinLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    medRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cabingroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Userid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinCrew", x => x.id);
                    table.ForeignKey(
                        name: "FK_CabinCrew_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoPilot",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    copilotLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    medRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Userid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoPilot", x => x.id);
                    table.ForeignKey(
                        name: "FK_CoPilot_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pilot",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pilotLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    medRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Userid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilot", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pilot_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technician",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicianLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    medRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnicianGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Userid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technician", x => x.id);
                    table.ForeignKey(
                        name: "FK_Technician_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Treasuries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Userid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treasuries", x => x.id);
                    table.ForeignKey(
                        name: "FK_Treasuries_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pilotid = table.Column<int>(type: "int", nullable: false),
                    CoPilotid = table.Column<int>(type: "int", nullable: false),
                    cabingroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    technicainGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aircraftid = table.Column<int>(type: "int", nullable: false),
                    Cabincrewid = table.Column<int>(type: "int", nullable: true),
                    Technicianid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.id);
                    table.ForeignKey(
                        name: "FK_Flights_Aircrafts_Aircraftid",
                        column: x => x.Aircraftid,
                        principalTable: "Aircrafts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Flights_CabinCrew_Cabincrewid",
                        column: x => x.Cabincrewid,
                        principalTable: "CabinCrew",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Flights_CoPilot_CoPilotid",
                        column: x => x.CoPilotid,
                        principalTable: "CoPilot",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Flights_Pilot_Pilotid",
                        column: x => x.Pilotid,
                        principalTable: "Pilot",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Flights_Technician_Technicianid",
                        column: x => x.Technicianid,
                        principalTable: "Technician",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Userid",
                table: "Admins",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_CabinCrew_Userid",
                table: "CabinCrew",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_CoPilot_Userid",
                table: "CoPilot",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Aircraftid",
                table: "Flights",
                column: "Aircraftid");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Cabincrewid",
                table: "Flights",
                column: "Cabincrewid");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_CoPilotid",
                table: "Flights",
                column: "CoPilotid");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Pilotid",
                table: "Flights",
                column: "Pilotid");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Technicianid",
                table: "Flights",
                column: "Technicianid");

            migrationBuilder.CreateIndex(
                name: "IX_Pilot_Userid",
                table: "Pilot",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Technician_Userid",
                table: "Technician",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Treasuries_Userid",
                table: "Treasuries",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Roleid",
                table: "Users",
                column: "Roleid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AirCraftRequest");

            migrationBuilder.DropTable(
                name: "Contactus");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "MedicalRequests");

            migrationBuilder.DropTable(
                name: "Treasuries");

            migrationBuilder.DropTable(
                name: "Aircrafts");

            migrationBuilder.DropTable(
                name: "CabinCrew");

            migrationBuilder.DropTable(
                name: "CoPilot");

            migrationBuilder.DropTable(
                name: "Pilot");

            migrationBuilder.DropTable(
                name: "Technician");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
