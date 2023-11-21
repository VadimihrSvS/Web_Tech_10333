using Lab_WT_Data.Data;
using Lab_WT_Data.Entities;
using Microsoft.AspNetCore.Identity;

public class DbInitializer
{
    public static async Task Seed(WebApplication app)
    {

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider
            .GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
        var userManager = scope.ServiceProvider
            .GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;
        var roleManager = scope.ServiceProvider
            .GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;


        context.Database.EnsureCreated();

        if (!context.Roles.Any())
        {
            var roleAdmin = new IdentityRole
            {
                Name = "admin",
                NormalizedName = "admin"
            };
            await roleManager.CreateAsync(roleAdmin);
        }

        if (!context.Users.Any())
        {
            var user = new ApplicationUser
            {
                Email = "user@mail.ru",
                UserName = "user@mail.ru"
            };

            await userManager.CreateAsync(user, "123456");

            var admin = new ApplicationUser
            {
                Email = "admin@mail.ru",
                UserName = "admin@mail.ru"
            };

            await userManager.CreateAsync(admin, "123456");

            admin = await userManager.FindByEmailAsync("admin@mail.ru");
            await userManager.AddToRoleAsync(admin, "admin");
        }
    }
}