using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet
{
    public partial class LiquorDBContext : DbContext
    {
        public LiquorDBContext()
        {
        }

        public LiquorDBContext(DbContextOptions<LiquorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IngredOnHand> Cabinet { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<DrinkDb> DrinkDb { get; set; }
        public virtual DbSet<IngredDb> IngredDb { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = tcp:theliquorcabinetdb.database.windows.net, 1433; Initial Catalog = TheLiquorCabinetDB; Persist Security Info = False; User ID = liquordevs; Password =Electricwafflebar4; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredOnHand>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.IngredID).HasColumnName("IngredID");

                entity.Property(e => e.UserID).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cabinet)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cabinet__UserID__5441852A");
                entity.ToTable("Cabinet");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.DrinkID).HasColumnName("DrinkID");

                entity.Property(e => e.UserID).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorites__UserID__571DF1D5");

                entity.ToTable("Favorites");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserID)
                    .HasName("PK__Users__1788CCACD3079F3C");

                entity.Property(e => e.UserID)
                    .HasColumnName("UserID")
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                entity.Property(e => e.Username).HasMaxLength(40);
                
                entity.Property(e => e.Birthday)
                    .HasColumnName("Birthday")
                    .IsRequired();

                entity.ToTable("Users");
            });

            modelBuilder.Entity<DrinkDb>(entity =>
            {
                entity.Property(e => e.IdDrink)
                    .HasColumnName("DrinkID")
                    .IsRequired();

                entity.Property(e => e.StrDrink)
                    .HasColumnName("Drink_Name")
                    .IsRequired();

                entity.Property(e => e.StrIngredient1)
                    .HasColumnName("Ingred1");
                entity.Property(e => e.StrIngredient2)
                    .HasColumnName("Ingred2");
                entity.Property(e => e.StrIngredient3)
                    .HasColumnName("Ingred3");
                entity.Property(e => e.StrIngredient4)
                    .HasColumnName("Ingred4");
                entity.Property(e => e.StrIngredient5)
                    .HasColumnName("Ingred5");
                entity.Property(e => e.StrIngredient6)
                    .HasColumnName("Ingred6");
                entity.Property(e => e.StrIngredient7)
                    .HasColumnName("Ingred7");
                entity.Property(e => e.StrIngredient8)
                    .HasColumnName("Ingred8");
                entity.Property(e => e.StrIngredient9)
                    .HasColumnName("Ingred9");
                entity.Property(e => e.StrIngredient10)
                    .HasColumnName("Ingred10");
                entity.Property(e => e.StrIngredient11)
                    .HasColumnName("Ingred11");
                entity.Property(e => e.StrIngredient12)
                    .HasColumnName("Ingred12");
                entity.Property(e => e.StrIngredient13)
                    .HasColumnName("Ingred13");
                entity.Property(e => e.StrIngredient14)
                    .HasColumnName("Ingred14");
                entity.Property(e => e.StrIngredient15)
                    .HasColumnName("Ingred15");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
