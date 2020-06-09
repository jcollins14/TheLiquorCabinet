﻿// <auto-generated />
using CocktailDbTools.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CocktailDbTools.Migrations
{
    [DbContext(typeof(CocktailDbContext))]
    [Migration("20200609000500_cocktailtestDB")]
    partial class cocktailtestDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CocktailDbTools.Models.DrinkDb", b =>
                {
                    b.Property<string>("idDrink")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("strDrink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient11")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient12")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient13")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient14")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient15")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strIngredient9")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idDrink");

                    b.ToTable("DrinkDb");
                });
#pragma warning restore 612, 618
        }
    }
}
