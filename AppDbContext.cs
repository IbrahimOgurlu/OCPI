using Microsoft.EntityFrameworkCore;
using WattReise.OCPI.Models;

namespace WattReise.OCPI.Data
{
    public class OcpiDbContext : DbContext
    {
        public OcpiDbContext(DbContextOptions<OcpiDbContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Evse> Evses { get; set; }
        public DbSet<Connector> Connectors { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .HasMany(l => l.Evses)
                .WithOne()
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Evse>()
                .HasMany(e => e.Connectors)
                .WithOne()
                .HasForeignKey(c => c.EvseUid)
                .OnDelete(DeleteBehavior.Cascade);

            // GeoLocation Owned Entity olarak tanımlandığı için burada ekstra bir konfigürasyona genellikle gerek yoktur.
            // Ancak, isterseniz manuel olarak da konfigüre edebilirsiniz:
            // modelBuilder.Entity<Location>().OwnsOne(l => l.Coordinates);

            base.OnModelCreating(modelBuilder); // Taban sınıfın OnModelCreating metodu çağrılmalı
        }
    }
}