using Microsoft.EntityFrameworkCore.Migrations;

namespace WasteManager.Migrations
{
    public partial class adding_lng_and_lng_to_customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "zip",
                table: "Customers",
                newName: "Zip");

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lng",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "Customers",
                newName: "zip");
        }
    }
}
