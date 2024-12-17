using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodTruck.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class foodRateIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodRate",
                table: "FoodRate");

            migrationBuilder.AddColumn<int>(
                name: "FoodRateId",
                table: "FoodRate",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodRate",
                table: "FoodRate",
                column: "FoodRateId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodRate_FoodId_RateId",
                table: "FoodRate",
                columns: new[] { "FoodId", "RateId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodRate",
                table: "FoodRate");

            migrationBuilder.DropIndex(
                name: "IX_FoodRate_FoodId_RateId",
                table: "FoodRate");

            migrationBuilder.DropColumn(
                name: "FoodRateId",
                table: "FoodRate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodRate",
                table: "FoodRate",
                columns: new[] { "FoodId", "RateId" });
        }
    }
}
