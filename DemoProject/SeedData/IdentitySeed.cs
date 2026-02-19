using DemoProject.Models;
using Microsoft.AspNetCore.Identity;

namespace DemoProject.SeedData
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<User>>();

            // 1️⃣ Admin rolü yoksa oluştur
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // 2️⃣ Admin kullanıcı var mı kontrol et
            var adminUser = await userManager.FindByEmailAsync("admin@demo.com");

            if (adminUser == null)
            {
                var user = new User
                {
                    UserName = "admin@demo.com",
                    Email = "admin@demo.com",
                    FullName = "Admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
