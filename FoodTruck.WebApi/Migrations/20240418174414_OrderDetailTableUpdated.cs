using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTruck.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class OrderDetailTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderDetails",
                newName: "Count");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "OrderDetails",
                newName: "Quantity");
        }
    }
}
