using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet
{
    public class CabinetDBContext : DbContext
    {
        public CabinetDBContext()
        {
        }

        public CabinetDBContext(DbContextOptions<CabinetDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = tcp:theliquorcabinetdb.database.windows.net, 1433; Initial Catalog = TheLiquorCabinetDB; Persist Security Info = False; User ID = liquordevs; Password =Electricwafflebar4; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            }
        }
        //protected override void OnModelCreating(ModelBuilder modelbuilder )
        //{
        //    modelbuilder.Entity<User>()
        //        .HasKey(e => e.ID)
        //        .HasName("UserID");
            //modelbuilder.Entity<User>()
            //    .HasMany<Favorite>(e => e.Favorites)
            //    .WithOne(e => e.DrinkID);
            //modelbuilder.Entity<Favorite>()
            //    .HasOne<User>(e => e.UserID)
            //    .WithMany(e => e.)
        //}
    }
}