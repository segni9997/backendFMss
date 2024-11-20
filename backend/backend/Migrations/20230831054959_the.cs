using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class the : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Group",
                table: "Cabingroups",
                newName: "CGroup");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cabingroups",
                newName: "cId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CGroup",
                table: "Cabingroups",
                newName: "Group");

            migrationBuilder.RenameColumn(
                name: "cId",
                table: "Cabingroups",
                newName: "Id");
        }
    }
}
