using System;
using System.Collections.Generic;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedDatabase(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedDatabase(ModelBuilder modelBuilder)
        {
            var portfolios = new List<Portfolio>()
            {
                new Portfolio()
                {
                    Id = new Guid("108e7c96-ea21-44f2-9cbe-db4237c2d1dd"),
                    Name = "Portfolio 1",
                    OwnerId = new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9")
                },
                new Portfolio()
                {
                    Id = new Guid("41881c20-df28-43df-8e1c-e42748181ea3"),
                    Name = "Portfolio 2"
                }
            };

            var holdings = new List<Holding>()
            {
                new Holding()
                {
                    Id = new Guid("6865a7fa-6866-4516-9002-53cc8386991e"),
                    Ticker = "AAPL",
                    PortfolioId = new Guid("108e7c96-ea21-44f2-9cbe-db4237c2d1dd"),
                },
                new Holding()
                {
                    Id = new Guid("f5f1e765-a3bb-44bb-89b9-52ab8eab9db4"),
                    Ticker = "MSFT",
                    PortfolioId = new Guid("108e7c96-ea21-44f2-9cbe-db4237c2d1dd"),
                }
            };

            var trades = new List<Trade>()
            {
                new Trade()
                {
                    Id = new Guid("5df7c00b-3cfd-48e7-a674-6aee0f120313"),
                    TradeType = TradeType.Buy,
                    Quantity = 10,
                    UnitPrice = 100,
                    Fees = 0,
                    HoldingId = new Guid("6865a7fa-6866-4516-9002-53cc8386991e"),
                    Date = DateTime.Now
                },
                 new Trade()
                {
                    Id = new Guid("850941ee-b257-4439-b8b6-95a0edc55200"),
                    TradeType = TradeType.Buy,
                    Quantity = 6,
                    UnitPrice = 250,
                    Fees = 1,
                    HoldingId = new Guid("f5f1e765-a3bb-44bb-89b9-52ab8eab9db4"),
                    Date = DateTime.UtcNow
                }
            };

            modelBuilder.Entity<Portfolio>().HasData(portfolios);
            modelBuilder.Entity<Holding>().HasData(holdings);
            modelBuilder.Entity<Trade>().HasData(trades);
        }
    }
}