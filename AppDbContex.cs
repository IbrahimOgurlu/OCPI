using Microsoft.EntityFrameworkCore;
using OCPI.Models;

namespace OCPI.Data
{
    public class OcpiDbContext : DbContext
    {
        public OcpiDbContext(DbContextOptions<OcpiDbContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}


