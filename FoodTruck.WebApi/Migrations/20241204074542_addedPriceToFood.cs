using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTruck.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class addedPriceToFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "Foods",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "Foods");
        }
    }
}
