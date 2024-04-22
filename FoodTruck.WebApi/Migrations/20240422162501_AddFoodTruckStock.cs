﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTruck.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFoodTruckStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "FoodTrucks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "FoodTrucks");
        }
    }
}
