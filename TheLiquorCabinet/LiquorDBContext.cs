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

        public virtual DbSet<Cabinet> Cabinet { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<User> Users { get; set; }

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
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CCACD3079F3C");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Username).HasMaxLength(40);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
