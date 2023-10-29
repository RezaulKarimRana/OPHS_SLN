using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Data.Migrations
{
    public partial class noticedetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Notice");

            migrationBuilder.RenameColumn(
                name: "FileType",
                table: "Notice",
                newName: "Serial");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Notice",
                newName: "Details");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Serial",
                table: "Notice",
                newName: "FileType");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Notice",
                newName: "FilePath");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Notice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
