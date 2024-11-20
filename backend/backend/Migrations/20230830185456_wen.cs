using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class wen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Group",
                table: "TeckGroups",
                newName: "TGroup");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TeckGroups",
                newName: "tid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TGroup",
                table: "TeckGroups",
                newName: "Group");

            migrationBuilder.RenameColumn(
                name: "tid",
                table: "TeckGroups",
                newName: "id");
        }
    }
}
