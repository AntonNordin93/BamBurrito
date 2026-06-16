using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BamBurrito.Core.Entities;

namespace BamBurrito.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider, ILogger logger)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var adminEmail = "owner@bamburrito.se"; 

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true // Slipper verifierings-mail
            };

            var result = await userManager.CreateAsync(newAdmin, "RullatMedPerfektion2026!");

            if (result.Succeeded)
            {
                logger.LogInformation("Ägarkontot skapades framgångsrikt under uppstart.");
            }
            else
            {
                logger.LogError("Kunde inte skapa ägarkontot: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
        else
        {
            logger.LogInformation("Ägarkontot finns redan, skippar seeding.");
        }
    }
}
