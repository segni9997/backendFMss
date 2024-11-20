using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class jjsgssff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeckGroupid",
                table: "Technician",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cabingroupId",
                table: "CabinCrew",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cabingroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabingroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeckGroups",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeckGroups", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Technician_TeckGroupid",
                table: "Technician",
                column: "TeckGroupid");

            migrationBuilder.CreateIndex(
                name: "IX_CabinCrew_cabingroupId",
                table: "CabinCrew",
                column: "cabingroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CabinCrew_Cabingroups_cabingroupId",
                table: "CabinCrew",
                column: "cabingroupId",
                principalTable: "Cabingroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Technician_TeckGroups_TeckGroupid",
                table: "Technician",
                column: "TeckGroupid",
                principalTable: "TeckGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CabinCrew_Cabingroups_cabingroupId",
                table: "CabinCrew");

            migrationBuilder.DropForeignKey(
                name: "FK_Technician_TeckGroups_TeckGroupid",
                table: "Technician");

            migrationBuilder.DropTable(
                name: "Cabingroups");

            migrationBuilder.DropTable(
                name: "TeckGroups");

            migrationBuilder.DropIndex(
                name: "IX_Technician_TeckGroupid",
                table: "Technician");

            migrationBuilder.DropIndex(
                name: "IX_CabinCrew_cabingroupId",
                table: "CabinCrew");

            migrationBuilder.DropColumn(
                name: "TeckGroupid",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "cabingroupId",
                table: "CabinCrew");
        }
    }
}
