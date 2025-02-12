using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRecipes.Recipes.Repository.EF.Migrations
{
    /// <inheritdoc />
    public partial class RecipeIngredientChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Ingredient_IngredientId",
                schema: "Recipe",
                table: "RecipeIngredients");

            migrationBuilder.AlterColumn<Guid>(
                name: "IngredientId",
                schema: "Recipe",
                table: "RecipeIngredients",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Ingredient_IngredientId",
                schema: "Recipe",
                table: "RecipeIngredients",
                column: "IngredientId",
                principalSchema: "Recipe",
                principalTable: "Ingredient",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Ingredient_IngredientId",
                schema: "Recipe",
                table: "RecipeIngredients");

            migrationBuilder.AlterColumn<Guid>(
                name: "IngredientId",
                schema: "Recipe",
                table: "RecipeIngredients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Ingredient_IngredientId",
                schema: "Recipe",
                table: "RecipeIngredients",
                column: "IngredientId",
                principalSchema: "Recipe",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
