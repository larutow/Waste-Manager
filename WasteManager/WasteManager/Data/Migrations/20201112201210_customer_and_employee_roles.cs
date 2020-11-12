using Microsoft.EntityFrameworkCore.Migrations;

namespace WasteManager.Data.Migrations
{
    public partial class customer_and_employee_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a006b5d3-c077-4951-8eb7-0c889655e285");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "76581d25-8cf8-4aaf-b3e6-bdf0a3398818", "48ba8d9c-fed9-4589-8957-9d75548cb46a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c49b93d5-9048-41fe-9c8e-320ba7d4a22b", "01011548-7b6b-4954-ba41-6ab45a70d2bd", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f058582e-9d03-429e-bfb6-3560a1622ddc", "4c118462-f0ce-4189-828e-2909bf3b7585", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76581d25-8cf8-4aaf-b3e6-bdf0a3398818");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c49b93d5-9048-41fe-9c8e-320ba7d4a22b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f058582e-9d03-429e-bfb6-3560a1622ddc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a006b5d3-c077-4951-8eb7-0c889655e285", "0e71c296-d260-4e01-af90-78087b7ad810", "Admin", "ADMIN" });
        }
    }
}
