using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Data.Migrations
{
    public partial class noticetablecol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Notice",
                newName: "FileType");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Notice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Notice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Notice");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Notice");

            migrationBuilder.RenameColumn(
                name: "FileType",
                table: "Notice",
                newName: "Details");
        }
    }
}
