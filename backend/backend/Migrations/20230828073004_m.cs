using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class m : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CabinCrew_Cabingroups_cabingroupId",
                table: "CabinCrew");

            migrationBuilder.DropForeignKey(
                name: "FK_Technician_TeckGroups_TeckGroupid",
                table: "Technician");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
