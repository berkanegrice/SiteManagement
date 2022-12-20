using System;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Identity;
using PermissionManagement.MVC.Constants;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using PermissionManagement.MVC.Data;
using PermissionManagement.MVC.Models;

namespace PermissionManagement.MVC.Seeds
{
    public static class DefaultUsers
    {
        // public static void SeedAllBasicUserAsync(UserManager<IdentityUser> userManager,
        //     RoleManager<IdentityRole> roleManager)
        // {
        //     var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        //     {
        //         HeaderValidated = null,
        //         MissingFieldFound = null
        //     };
        //     using var reader = new StreamReader("/home/berkan/Downloads/persons.csv");
        //     using var csv = new CsvReader(reader, config);
        //     csv.Read();
        //     var records = csv.GetRecords<UserModel>();
        //     foreach (var record in records)
        //     {
        //         Console.WriteLine(record);
        //     }
        // }
        
        
        // public static void SeedUserCurrencyAsync()
        // {
        //     var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        //     {
        //         HeaderValidated = null,
        //         MissingFieldFound = null
        //     };
        //     using var reader = new StreamReader("/home/berkan/Downloads/yeni/Mizan_2022_110.csv");
        //     using var csv = new CsvReader(reader, config);
        //     csv.Read();
        //     var records = csv.GetRecords<DuesInformation>();
        //     foreach (var record in records)
        //     {
        //         Console.WriteLine(record);
        //     }
        // }

        // public static void SeedDetailedDuesInformation()
        // {
        //     var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        //     {
        //         HeaderValidated = null,
        //         MissingFieldFound = null
        //     };
        //     using var reader = new StreamReader("/home/berkan/Downloads/yeni/Muavin_2022_10_final.csv");
        //     using var csv = new CsvReader(reader, config);
        //     // csv.Read();
        //     var records = csv.GetRecords<DuesDetailedInformation>();
        //     foreach (var record in records)
        //     {
        //         Console.WriteLine(record);
        //     }
        // }

        public static async Task SeedAllBasicUserAsync(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            // var userModels = dbContext.UsersModel.GroupBy(s => s.Email)
            //     .Select(grp => grp.FirstOrDefault())
            //     .OrderBy(s => s.Email)
            //     .AsEnumerable();

            var userModels = dbContext.UsersModel.ToList();
            foreach (var userModel in userModels)
            {
                var defaultUser = new IdentityUser
                {
                    UserName = userModel.Email,
                    Email = userModel.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                if (userManager.Users.All(u => u.Id != defaultUser.Id))
                {
                    var user = await userManager.FindByEmailAsync(defaultUser.Email);
                    
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                        await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    }
                }
            }
        }
        
        public static async Task SeedBasicUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new IdentityUser
            {
                UserName = "basicuser@gmail.com",
                Email = "basicuser@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
                // else
                // {   
                //     Console.WriteLine("or basic here");
                //     await  userManager.DeleteAsync(user);
                // }
            }
        }

        public static async Task SeedSuperAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new IdentityUser
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
                // else
                // {
                //     Console.WriteLine("or superadmin here");
                //     await  userManager.DeleteAsync(user);
                // }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }

        // private static async Task SeedClaimsForBasicUser(this RoleManager<IdentityRole> roleManager)
        // {
        //     var basicRole = await roleManager.FindByNameAsync("Basic");
        //     await roleManager.AddPermissionClaim(basicRole, "Dues");
        // }
        
        private static async Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "LeaseHolder");
        }
        
        private static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions.Where(permission => !allClaims.Any(a => a.Type == "Permission" && a.Value == permission)))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}