using Farsight.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Farsight.Backend.Persistence
{
    public class FarsightBackendDbContext : DbContext
    {
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Holding> Holdings { get; set; }
        public DbSet<Trade> Trades { get; set; }

        public FarsightBackendDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}