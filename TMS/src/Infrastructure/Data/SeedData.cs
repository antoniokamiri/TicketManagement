using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.PermissionSet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Security.Claims;

namespace Infrastructure.Data;

public class SeedData
{
    private readonly AppDBContext _context;
    private readonly ILogger<SeedData> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public SeedData(ILogger<SeedData> logger, AppDBContext context, RoleManager<ApplicationRole> roleManager, UserManager<User> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsRelational())
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }
    public async Task SeedAsync()
    {
        try
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedDataAsync();
            _context.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }
    private static IEnumerable<string> GetAllPermissions()
    {
        var allPermissions = new List<string>();
        var modules = typeof(Permissions).GetNestedTypes();

        foreach (var module in modules)
        {
            var moduleName = string.Empty;
            var moduleDescription = string.Empty;

            var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            foreach (var fi in fields)
            {
                var propertyValue = fi.GetValue(null);

                if (propertyValue is not null)
                    allPermissions.Add((string)propertyValue);
            }
        }

        return allPermissions;
    }
    private async Task SeedRolesAsync()
    {
        var adminRoleName = RoleName.Admin;
        var userRoleName = RoleName.Basic;

        if (await _roleManager.RoleExistsAsync(adminRoleName)) return;

        _logger.LogInformation("Seeding roles...");
        var administratorRole = new ApplicationRole(adminRoleName)
        {
            Description = "Admin Group",
        };
        var userRole = new ApplicationRole(userRoleName)
        {
            Description = "Basic Group",
        };

        await _roleManager.CreateAsync(administratorRole);
        await _roleManager.CreateAsync(userRole);

        var permissions = GetAllPermissions();

        foreach (var permission in permissions)
        {
            var claim = new Claim(ApplicationClaimTypes.Permission, permission);
            await _roleManager.AddClaimAsync(administratorRole, claim);

            if (permission.StartsWith("Permissions.Discussions"))
            {
                await _roleManager.AddClaimAsync(userRole, claim);
            }
        }
    }
    private async Task SeedUsersAsync()
    {
        string USER_EMAIL = "admin@edged.com";
        string USER_EMAIL2 = "user@edged.com";

        if (await _userManager.Users.AnyAsync()) return;

        _logger.LogInformation("Seeding users...");
        var adminUser = new User
        {
            UserName = USER_EMAIL,
            AccountConfirmed = false,
            DisplayName = "Administrator",
            Email = USER_EMAIL,
            EmailConfirmed = true,
            ProfilePictureDataUrl = "https://s.gravatar.com/avatar/78be68221020124c23c665ac54e07074?s=80",
            LanguageCode = "en-US",
            TimeZoneId = "Asia/Shanghai",
            TwoFactorEnabled = false
        };

        var demoUser = new User
        {
            UserName = USER_EMAIL2,
            AccountConfirmed = false,
            DisplayName = USER_EMAIL2,
            Email = "demo@example.com",
            EmailConfirmed = true,
            LanguageCode = "de-DE",
            TimeZoneId = "Europe/Berlin",
            ProfilePictureDataUrl = "https://s.gravatar.com/avatar/ea753b0b0f357a41491408307ade445e?s=80"
        };

        await _userManager.CreateAsync(adminUser, Constants.DEFAULT_PASSWORD);
        await _userManager.AddToRoleAsync(adminUser, RoleName.Admin);

        await _userManager.CreateAsync(demoUser, Constants.DEFAULT_PASSWORD);
        await _userManager.AddToRoleAsync(demoUser, RoleName.Basic);
    }
    public async Task SeedDataAsync()
    {
        if (!await _context.Categories.AnyAsync())
        {
            _logger.LogInformation("Seeding categories...");

            var catogories = new[]
            {
                new Category
                {
                    Name = "Application Bug"
                },
                new Category
                {
                    Name = "Network Issue"
                },
                new Category
                {
                    Name = "User Issue"
                }
            };
            await _context.Categories.AddRangeAsync(catogories);
            await _context.SaveChangesAsync();
        }

        if (!await _context.Products.AnyAsync())
        {
            _logger.LogInformation("Seeding products...");

            var products = new[]
            {
                new Product
                {
                    Name = "Product 1"
                },
                new Product
                {
                    Name = "Product 2"
                },                
                new Product
                {
                    Name = "Product 3"
                }
            };
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }

        if (!await _context.Priorities.AnyAsync())
        {
            _logger.LogInformation("Seeding priorities...");

            var priorities = new[]
            {
                new Priority
                {
                    Name = "Low",
                    Expected_Days = 14
                },
                new Priority
                {
                    Name = "Medium",
                    Expected_Days = 7
                },
                new Priority
                {
                    Name = "High",
                    Expected_Days = 1
                }
            };
            await _context.Priorities.AddRangeAsync(priorities);
            await _context.SaveChangesAsync();
        }

        if (!await _context.Tickets.AnyAsync())
        {
            _logger.LogInformation("Seeding tickets...");
            var user1 = await _userManager.Users.Where(u => u.UserName == "user@edged.com").Select(i => i.Id).FirstOrDefaultAsync();
            var tickets = new[]
            {

                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 1, Priority_Id = 1, Status = "NEW", Summary = "Sample ticket 1", Description = "Description for ticket 1", Raised_Date = new DateTime(2024, 9, 1), Expected_Date = new DateTime(2024, 9, 9) }, // 8 days
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 2, Priority_Id = 2, Status = "OPEN", Summary = "Sample ticket 2", Description = "Description for ticket 2", Raised_Date = new DateTime(2024, 9, 2), Expected_Date = new DateTime(2024, 9, 7) }, // 5 days
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 3, Status = "NEW", Summary = "Sample ticket 3", Description = "Description for ticket 3", Raised_Date = new DateTime(2024, 9, 3), Expected_Date = new DateTime(2024, 9, 3, 8, 0, 0) }, // 8 hours
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 1, Status = "OPEN", Summary = "Sample ticket 4", Description = "Description for ticket 4", Raised_Date = new DateTime(2024, 9, 4), Expected_Date = new DateTime(2024, 9, 12) }, // 8 days
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 3, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 5", Description = "Description for ticket 5", Raised_Date = new DateTime(2024, 9, 5), Expected_Date = new DateTime(2024, 9, 10) }, // 5 days
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 3, Status = "NEW", Summary = "Sample ticket 6", Description = "Description for ticket 6", Raised_Date = new DateTime(2024, 9, 6), Expected_Date = new DateTime(2024, 9, 6, 8, 0, 0) }, // 8 hours
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 1, Status = "OPEN", Summary = "Sample ticket 7", Description = "Description for ticket 7", Raised_Date = new DateTime(2024, 1, 7), Expected_Date = new DateTime(2024, 1, 15) }, // 8 days
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 3, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 8", Description = "Description for ticket 8", Raised_Date = new DateTime(2024, 2, 8), Expected_Date = new DateTime(2024, 2, 13) }, // 5 days
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 3, Status = "NEW", Summary = "Sample ticket 9", Description = "Description for ticket 9", Raised_Date = new DateTime(2024, 3, 9), Expected_Date = new DateTime(2024, 3, 9, 8, 0, 0) }, // 8 hours
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 1, Status = "OPEN", Summary = "Sample ticket 10", Description = "Description for ticket 10", Raised_Date = new DateTime(2024, 4, 10), Expected_Date = new DateTime(2024, 4, 18) }, // 8 days
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 3, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 11", Description = "Description for ticket 11", Raised_Date = new DateTime(2024, 5, 11), Expected_Date = new DateTime(2024, 5, 16) }, // 5 days
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 3, Status = "NEW", Summary = "Sample ticket 12", Description = "Description for ticket 12", Raised_Date = new DateTime(2024, 6, 12), Expected_Date = new DateTime(2024, 6, 12, 8, 0, 0) }, // 8 hours
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 1, Status = "OPEN", Summary = "Sample ticket 13", Description = "Description for ticket 13", Raised_Date = new DateTime(2024, 7, 13), Expected_Date = new DateTime(2024, 7, 21) }, // 8 days
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 3, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 14", Description = "Description for ticket 14", Raised_Date = new DateTime(2024, 8, 14), Expected_Date = new DateTime(2024, 8, 19) }, // 5 days
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 3, Status = "NEW", Summary = "Sample ticket 15", Description = "Description for ticket 15", Raised_Date = new DateTime(2024, 9, 15), Expected_Date = new DateTime(2024, 9, 15, 8, 0, 0) }, // 8 hours
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 1, Status = "OPEN", Summary = "Sample ticket 16", Description = "Description for ticket 16", Raised_Date = new DateTime(2024, 1, 16), Expected_Date = new DateTime(2024, 1, 24) }, // 8 days
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 3, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 17", Description = "Description for ticket 17", Raised_Date = new DateTime(2024, 2, 17), Expected_Date = new DateTime(2024, 2, 22) }, // 5 days
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 3, Status = "NEW", Summary = "Sample ticket 18", Description = "Description for ticket 18", Raised_Date = new DateTime(2024, 3, 18), Expected_Date = new DateTime(2024, 3, 18, 8, 0, 0) }, // 8 hours
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 1, Status = "OPEN", Summary = "Sample ticket 19", Description = "Description for ticket 19", Raised_Date = new DateTime(2024, 4, 19), Expected_Date = new DateTime(2024, 4, 27) }, // 8 days
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 3, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 20", Description = "Description for ticket 20", Raised_Date = new DateTime(2024, 9, 20), Expected_Date = new DateTime(2024, 9, 25) }, // 5 days
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 2, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 21", Description = "Description for ticket 21", Raised_Date = new DateTime(2024, 6, 5, 7, 40, 49), Expected_Date = new DateTime(2024, 6, 10, 7, 40, 49) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 2, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 22", Description = "Description for ticket 22", Raised_Date = new DateTime(2023, 11, 6, 15, 56, 10), Expected_Date = new DateTime(2023, 11, 6, 23, 56, 10) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 3, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 23", Description = "Description for ticket 23", Raised_Date = new DateTime(2023, 10, 1, 16, 10, 57), Expected_Date = new DateTime(2023, 10, 9, 16, 10, 57) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 24", Description = "Description for ticket 24", Raised_Date = new DateTime(2023, 10, 23, 19, 16, 57), Expected_Date = new DateTime(2023, 10, 24, 3, 16, 57) },
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 25", Description = "Description for ticket 25", Raised_Date = new DateTime(2023, 11, 22, 4, 18, 47), Expected_Date = new DateTime(2023, 11, 27, 4, 18, 47) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 1, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 26", Description = "Description for ticket 26", Raised_Date = new DateTime(2024, 5, 30, 0, 28, 17), Expected_Date = new DateTime(2024, 6, 7, 0, 28, 17) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 2, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 27", Description = "Description for ticket 27", Raised_Date = new DateTime(2024, 5, 2, 20, 13, 4), Expected_Date = new DateTime(2024, 5, 10, 20, 13, 4) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 3, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 28", Description = "Description for ticket 28", Raised_Date = new DateTime(2024, 7, 28, 13, 1, 34), Expected_Date = new DateTime(2024, 7, 28, 21, 1, 34) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 29", Description = "Description for ticket 29", Raised_Date = new DateTime(2023, 10, 1, 9, 3, 20), Expected_Date = new DateTime(2023, 10, 6, 9, 3, 20) },
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 2, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 30", Description = "Description for ticket 30", Raised_Date = new DateTime(2024, 4, 21, 18, 56, 12), Expected_Date = new DateTime(2024, 4, 26, 18, 56, 12) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 2, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 31", Description = "Description for ticket 31", Raised_Date = new DateTime(2023, 12, 5, 3, 40, 49), Expected_Date = new DateTime(2023, 12, 5, 11, 40, 49) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 1, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 32", Description = "Description for ticket 32", Raised_Date = new DateTime(2023, 8, 10, 12, 50, 18), Expected_Date = new DateTime(2023, 8, 15, 12, 50, 18) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 3, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 33", Description = "Description for ticket 33", Raised_Date = new DateTime(2023, 7, 29, 5, 16, 30), Expected_Date = new DateTime(2023, 8, 6, 5, 16, 30) },
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 34", Description = "Description for ticket 34", Raised_Date = new DateTime(2023, 12, 25, 6, 40, 25), Expected_Date = new DateTime(2023, 12, 25, 14, 40, 25) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 35", Description = "Description for ticket 35", Raised_Date = new DateTime(2023, 7, 30, 11, 10, 42), Expected_Date = new DateTime(2023, 8, 7, 11, 10, 42) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 3, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 36", Description = "Description for ticket 36", Raised_Date = new DateTime(2023, 12, 31, 20, 36, 12), Expected_Date = new DateTime(2024, 1, 1, 4, 36, 12) },
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 37", Description = "Description for ticket 37", Raised_Date = new DateTime(2023, 9, 1, 12, 11, 15), Expected_Date = new DateTime(2023, 9, 6, 12, 11, 15) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 38", Description = "Description for ticket 38", Raised_Date = new DateTime(2023, 10, 12, 10, 43, 29), Expected_Date = new DateTime(2023, 10, 20, 10, 43, 29) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 3, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 39", Description = "Description for ticket 39", Raised_Date = new DateTime(2024, 7, 15, 8, 50, 11), Expected_Date = new DateTime(2024, 7, 20, 8, 50, 11) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 40", Description = "Description for ticket 40", Raised_Date = new DateTime(2024, 4, 22, 9, 28, 30), Expected_Date = new DateTime(2024, 4, 22, 17, 28, 30) },
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 41", Description = "Description for ticket 41", Raised_Date = new DateTime(2023, 9, 14, 14, 15, 30), Expected_Date = new DateTime(2023, 9, 22, 14, 15, 30) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 3, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 42", Description = "Description for ticket 42", Raised_Date = new DateTime(2023, 12, 3, 11, 23, 18), Expected_Date = new DateTime(2023, 12, 8, 11, 23, 18) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 43", Description = "Description for ticket 43", Raised_Date = new DateTime(2023, 10, 22, 16, 19, 44), Expected_Date = new DateTime(2023, 10, 27, 22, 19, 44) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 1, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 44", Description = "Description for ticket 44", Raised_Date = new DateTime(2023, 11, 7, 9, 7, 55), Expected_Date = new DateTime(2023, 11, 15, 9, 7, 55) },
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 2, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 45", Description = "Description for ticket 45", Raised_Date = new DateTime(2023, 7, 18, 12, 41, 29), Expected_Date = new DateTime(2023, 7, 23, 12, 41, 29) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 3, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 46", Description = "Description for ticket 46", Raised_Date = new DateTime(2023, 9, 5, 10, 56, 11), Expected_Date = new DateTime(2023, 9, 5, 18, 56, 11) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 1, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 47", Description = "Description for ticket 47", Raised_Date = new DateTime(2024, 4, 14, 13, 13, 30), Expected_Date = new DateTime(2024, 4, 22, 13, 13, 30) },
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 3, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 48", Description = "Description for ticket 48", Raised_Date = new DateTime(2024, 2, 8, 17, 12, 15), Expected_Date = new DateTime(2024, 2, 13, 17, 12, 15) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 49", Description = "Description for ticket 49", Raised_Date = new DateTime(2024, 3, 16, 7, 45, 48), Expected_Date = new DateTime(2024, 3, 16, 15, 45, 48) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 1, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 50", Description = "Description for ticket 50", Raised_Date = new DateTime(2023, 11, 25, 16, 19, 33), Expected_Date = new DateTime(2023, 12, 3, 16, 19, 33) },
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 3, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 51", Description = "Description for ticket 51", Raised_Date = new DateTime(2023, 10, 1, 15, 26, 21), Expected_Date = new DateTime(2023, 10, 9, 15, 26, 21) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 2, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 52", Description = "Description for ticket 52", Raised_Date = new DateTime(2023, 9, 29, 8, 30, 17), Expected_Date = new DateTime(2023, 10, 4, 8, 30, 17) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 3, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 53", Description = "Description for ticket 53", Raised_Date = new DateTime(2024, 1, 13, 14, 36, 48), Expected_Date = new DateTime(2024, 1, 13, 22, 36, 48) },
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 54", Description = "Description for ticket 54", Raised_Date = new DateTime(2023, 8, 21, 10, 17, 29), Expected_Date = new DateTime(2023, 8, 26, 10, 17, 29) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 3, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 55", Description = "Description for ticket 55", Raised_Date = new DateTime(2024, 5, 20, 5, 59, 36), Expected_Date = new DateTime(2024, 5, 20, 13, 59, 36) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 2, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 56", Description = "Description for ticket 56", Raised_Date = new DateTime(2023, 11, 19, 16, 23, 9), Expected_Date = new DateTime(2023, 11, 27, 16, 23, 9) },
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 57", Description = "Description for ticket 57", Raised_Date = new DateTime(2023, 8, 10, 9, 40, 13), Expected_Date = new DateTime(2023, 8, 15, 9, 40, 13) },
                new Ticket { Raised_By_Id = user1, Product_Id = 1, Category_Id = 3, Priority_Id = 1, Status = "CLOSED", Summary = "Sample ticket 58", Description = "Description for ticket 58", Raised_Date = new DateTime(2023, 10, 30, 11, 7, 25), Expected_Date = new DateTime(2023, 11, 7, 11, 7, 25) },
                new Ticket { Raised_By_Id = user1, Product_Id = 2, Category_Id = 2, Priority_Id = 3, Status = "CLOSED", Summary = "Sample ticket 59", Description = "Description for ticket 59", Raised_Date = new DateTime(2024, 6, 15, 12, 19, 44), Expected_Date = new DateTime(2024, 6, 15, 20, 19, 44) },
                new Ticket { Raised_By_Id = user1, Product_Id = 3, Category_Id = 1, Priority_Id = 2, Status = "CLOSED", Summary = "Sample ticket 60", Description = "Description for ticket 60", Raised_Date = new DateTime(2023, 9, 2, 18, 24, 59), Expected_Date = new DateTime(2023, 9, 7, 18, 24, 59) }
            };
            await _context.Tickets.AddRangeAsync(tickets);
            await _context.SaveChangesAsync();
        }
    }
}
