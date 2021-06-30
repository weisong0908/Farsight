using Farsight.IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Farsight.IdentityService.Persistence
{
    public class FarsightIdentityServiceDbContext : IdentityDbContext<FarsightUser>
    {
        public FarsightIdentityServiceDbContext(DbContextOptions<FarsightIdentityServiceDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // SeedUser(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedUser(ModelBuilder modelBuilder)
        {
            var passwordHasher = new PasswordHasher<FarsightUser>();

            var adminUser = new FarsightUser()
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                UserName = "weisong.teng",
                NormalizedUserName = "weisong.teng".ToUpper(),
                Email = "weisong.teng.work@gmail.com",
                NormalizedEmail = "weisong.teng.work@gmail.com".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null, "password")
            };

            var adminRole = new IdentityRole() { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Admin", NormalizedName = "admin".ToUpper() };

            var adminUserRole = new IdentityUserRole<string>()
            {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            };

            modelBuilder.Entity<FarsightUser>().HasData(adminUser);
            modelBuilder.Entity<IdentityRole>().HasData(adminRole);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);
        }
    }
}