using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusLine.Data.Migrations
{
    public partial class removeTravelsFromBus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_travels_buses_BusId",
                table: "travels");

            migrationBuilder.AddForeignKey(
                name: "FK_travels_buses_BusId",
                table: "travels",
                column: "BusId",
                principalTable: "buses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_travels_buses_BusId",
                table: "travels");

            migrationBuilder.AddForeignKey(
                name: "FK_travels_buses_BusId",
                table: "travels",
                column: "BusId",
                principalTable: "buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
