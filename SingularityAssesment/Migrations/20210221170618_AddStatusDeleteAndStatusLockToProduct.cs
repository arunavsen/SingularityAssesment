using Microsoft.EntityFrameworkCore.Migrations;

namespace SingularityAssesment.Migrations
{
    public partial class AddStatusDeleteAndStatusLockToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StatusDelete",
                table: "Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StatusLock",
                table: "Product",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusDelete",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "StatusLock",
                table: "Product");
        }
    }
}
