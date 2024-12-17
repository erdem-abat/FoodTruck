using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTruck.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class foodRateIndexChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FoodRate_FoodId_RateId",
                table: "FoodRate");

            migrationBuilder.CreateIndex(
                name: "IX_FoodRate_FoodId_RateId_UserId",
                table: "FoodRate",
                columns: new[] { "FoodId", "RateId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FoodRate_FoodId_RateId_UserId",
                table: "FoodRate");

            migrationBuilder.CreateIndex(
                name: "IX_FoodRate_FoodId_RateId",
                table: "FoodRate",
                columns: new[] { "FoodId", "RateId" },
                unique: true);
        }
    }
}
