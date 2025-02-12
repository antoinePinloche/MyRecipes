﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyRecipes.Recipes.Repository.EF.DbContext;

#nullable disable

namespace MyRecipes.Recipes.Repository.EF.Migrations
{
    [DbContext(typeof(RecipeDbContext))]
    [Migration("20250211142453_RecipeIngredientDeleteRecipLink")]
    partial class RecipeIngredientDeleteRecipLink
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Recipe")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyRecipes.Recipes.Domain.Entity.FoodType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FoodTypes", "Recipe");
                });

            modelBuilder.Entity("MyRecipes.Recipes.Domain.Entity.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FoodTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FoodTypeId");

                    b.ToTable("Ingredient", "Recipe");
                });

            modelBuilder.Entity("MyRecipes.Recipes.Domain.Entity.Instruction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Step")
                        .HasColumnType("int");

                    b.Property<string>("StepInstruction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StepName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Instructions", "Recipe");
                });

            modelBuilder.Entity("MyRecipes.Recipes.Domain.Entity.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbGuest")
                        .HasColumnType("int");

                    b.Property<int>("RecipyDifficulty")
                        .HasColumnType("int");

                    b.Property<int>("TimeToPrepareRecipe")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Recipes", "Recipe");
                });

            modelBuilder.Entity("MyRecipes.Recipes.Domain.Entity.RecipeIngredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IngredientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredients", "Recipe");
                });

            modelBuilder.Entity("MyRecipes.Recipes.Domain.Entity.Ingredient", b =>
                {
                    b.HasOne("MyRecipes.Recipes.Domain.Entity.FoodType", "FoodType")
                        .WithMany()
                        .HasForeignKey("FoodTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodType");
                });

            modelBuilder.Entity("MyRecipes.Recipes.Domain.Entity.Instruction", b =>
                {
                    b.HasOne("MyRecipes.Recipes.Domain.Entity.Recipe", "Recipe")
                        .WithMany("Instructions")
                        .HasForeignKey("RecipeId");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("MyRecipes.Recipes.Domain.Entity.RecipeIngredient", b =>
                {
                    b.HasOne("MyRecipes.Recipes.Domain.Entity.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId");

                    b.HasOne("MyRecipes.Recipes.Domain.Entity.Recipe", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("MyRecipes.Recipes.Domain.Entity.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Instructions");
                });
#pragma warning restore 612, 618
        }
    }
}
