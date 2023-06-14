using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusLine.Data.Migrations
{
    public partial class addNameToBus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "buses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "buses");
        }
    }
}
