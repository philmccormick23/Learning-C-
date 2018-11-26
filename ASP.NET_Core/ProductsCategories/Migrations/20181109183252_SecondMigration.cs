using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsCategories.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryDescription",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryPrice",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryDescription",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CategoryPrice",
                table: "Categories",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
