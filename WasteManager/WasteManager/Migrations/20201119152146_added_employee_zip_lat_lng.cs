using Microsoft.EntityFrameworkCore.Migrations;

namespace WasteManager.Migrations
{
    public partial class added_employee_zip_lat_lng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lng",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Employees");
        }
    }
}
