using Microsoft.EntityFrameworkCore.Migrations;

namespace Palangan.DataLayer.Migrations
{
    public partial class AddTableSee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "See",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "See",
                table: "Products");
        }
    }
}
