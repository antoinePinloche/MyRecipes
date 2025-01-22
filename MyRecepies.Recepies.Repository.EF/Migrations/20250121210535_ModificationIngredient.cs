using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRecipes.Recipes.Repository.EF.Migrations
{
    /// <inheritdoc />
    public partial class ModificationIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngredientCategory",
                schema: "Recipe",
                table: "Ingredient");

            migrationBuilder.AddColumn<Guid>(
                name: "FoodTypeId",
                schema: "Recipe",
                table: "Ingredient",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FoodTypes",
                schema: "Recipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_FoodTypeId",
                schema: "Recipe",
                table: "Ingredient",
                column: "FoodTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_FoodTypes_FoodTypeId",
                schema: "Recipe",
                table: "Ingredient",
                column: "FoodTypeId",
                principalSchema: "Recipe",
                principalTable: "FoodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_FoodTypes_FoodTypeId",
                schema: "Recipe",
                table: "Ingredient");

            migrationBuilder.DropTable(
                name: "FoodTypes",
                schema: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_FoodTypeId",
                schema: "Recipe",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "FoodTypeId",
                schema: "Recipe",
                table: "Ingredient");

            migrationBuilder.AddColumn<int>(
                name: "IngredientCategory",
                schema: "Recipe",
                table: "Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
