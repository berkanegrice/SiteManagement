using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Models;

namespace PermissionManagement.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DuesInformation> DuesInformations { get; set; }
        public DbSet<UserModel> UsersModel { get; set; }
        
        public DbSet<DuesDetailedInformation> DuesDetailedInformations { get; set; }
    }
}