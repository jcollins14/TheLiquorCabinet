﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheLiquorCabinet;

namespace TheLiquorCabinet.Migrations
{
    [DbContext(typeof(LiquorDBContext))]
    [Migration("20200610230200_ThirdMig")]
    partial class ThirdMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheLiquorCabinet.Cabinet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IngredId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cabinet");
                });

            modelBuilder.Entity("TheLiquorCabinet.Favorites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrinkId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites1");
                });

            modelBuilder.Entity("TheLiquorCabinet.Models.Favorite", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrinkID")
                        .HasColumnName("DrinkID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnName("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("TheLiquorCabinet.Models.IngredOnHand", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IngredID")
                        .HasColumnName("IngredID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnName("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("IngredOnHand");
                });

            modelBuilder.Entity("TheLiquorCabinet.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TheLiquorCabinet.Users", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnName("UserID")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CCACD3079F3C");

                    b.ToTable("Users1");
                });

            modelBuilder.Entity("TheLiquorCabinet.Cabinet", b =>
                {
                    b.HasOne("TheLiquorCabinet.Users", "User")
                        .WithMany("Cabinet")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheLiquorCabinet.Favorites", b =>
                {
                    b.HasOne("TheLiquorCabinet.Users", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheLiquorCabinet.Models.Favorite", b =>
                {
                    b.HasOne("TheLiquorCabinet.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserID")
                        .HasConstraintName("FK__Favorites__UserID__571DF1D5")
                        .IsRequired();
                });

            modelBuilder.Entity("TheLiquorCabinet.Models.IngredOnHand", b =>
                {
                    b.HasOne("TheLiquorCabinet.Models.User", "User")
                        .WithMany("Cabinet")
                        .HasForeignKey("UserID")
                        .HasConstraintName("FK__Cabinet__UserID__5441852A")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
