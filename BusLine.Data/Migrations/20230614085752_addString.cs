using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusLine.Data.Migrations
{
    public partial class addString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArrivalTime",
                table: "schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartureTime",
                table: "schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "schedules");
        }
    }
}
