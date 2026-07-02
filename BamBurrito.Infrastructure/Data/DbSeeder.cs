using BamBurrito.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BamBurrito.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider, ILogger logger)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await context.Database.MigrateAsync();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            logger.LogInformation("Admin-roll skapad.");
        }

        var adminEmail = "owner@bamburrito.se";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(newAdmin, "RullatMedPerfektion2026!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
                logger.LogInformation("Ägarkontot skapades och tilldelades Admin-roll.");
            }
        }
        else if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        if (!await context.LocationEvents.AnyAsync())
        {
            // Uppdaterat för StartTime och EndTime (Exempel: 16:00 till 20:00)
            var today = DateTime.Now.Date.AddDays(7);
            var testEvent = new LocationEvent
            {
                Title = "Premiär vid Sollentuna Centrum",
                StartTime = today.AddHours(16),
                EndTime = today.AddHours(20),
                Address = "Sollentunavägen 163",
                Description = "Vi kickar igång veckan med färska burritos!"
            };

            context.LocationEvents.Add(testEvent);
            await context.SaveChangesAsync();
            logger.LogInformation("Test-event skapat i databasen.");
        }
    }
}