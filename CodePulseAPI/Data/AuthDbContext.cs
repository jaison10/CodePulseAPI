
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "0a52a33b-9822-42be-877a-b3e637f57945";
            var writerRoleId = "2f8fe331-83b7-4d1a-b8a8-ee28fc6f925e";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };
            //seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            var adminId = "cd4071f1-a46c-41d3-ab72-7680e897225d";
            var admin = new IdentityUser()
            {
                Id = adminId,
                UserName = "Admin",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com",
                NormalizedUserName = "Admin"
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
            //seed user
            builder.Entity<IdentityUser>().HasData(admin);

            //assgning roles to the user admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminId,
                    RoleId = writerRoleId
                }
            };
            //seed the admin with roles
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
