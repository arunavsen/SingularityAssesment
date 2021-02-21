using Microsoft.EntityFrameworkCore.Migrations;

namespace SingularityAssesment.Migrations
{
    public partial class ChangeProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 101,
                column: "Description",
                value: "Product 101");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 202,
                column: "Description",
                value: "Product 102");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 303,
                column: "Description",
                value: "Product 103");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 101,
                column: "Description",
                value: "Product 101 des");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 202,
                column: "Description",
                value: "Product 102 des");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 303,
                column: "Description",
                value: "Product 103 des");
        }
    }
}
