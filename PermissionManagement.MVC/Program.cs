using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Data;

namespace PermissionManagement.MVC
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("app");
                try
                {
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    // var dbContext = services.GetRequiredService<ApplicationDbContext>();
                    
                    await Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                    await Seeds.DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
                    await Seeds.DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);


                    // Seeds.DefaultUsers.SeedDetailedDuesInformation();
                    // Seeds.DefaultUsers.SeedUserCurrencyAsync();
                    // Seeds.DefaultUsers.SeedAllBasicUserAsync(userManager, roleManager);
                    // await Seeds.DefaultUsers.SeedAllBasicUserAsync(userManager, roleManager, dbContext);
                    
                    logger.LogInformation("Finished Seeding Default Data");
                    logger.LogInformation("Application Starting");
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "An error occurred seeding the DB");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}