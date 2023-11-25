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

        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (!context.Groups.Any())
        {
            context.Groups.AddRange(
            new List<DishGroup>
            {
                    new DishGroup {GroupName="Салаты"},
                    new DishGroup {GroupName="Стартеры"},
                    new DishGroup {GroupName="Супы"},
                    new DishGroup {GroupName="Основные блюда"},
                    new DishGroup {GroupName="Напитки"},
                    new DishGroup {GroupName="Десерты"}
            });
            await context.SaveChangesAsync();
        }
        // проверка наличия объектов
        if (!context.Dishes.Any())
        {
            context.Dishes.AddRange(
            new List<Dish>
            {
                    new Dish {DishName="Суп-харчо", Description="Очень острый, невкусный", Calories =200, DishGroupId=3, Image="1.jpeg" },
                    new Dish {DishName="Борщ", Description="Много сала, без сметаны", Calories =330, DishGroupId=3, Image="2.jpeg" },
                    new Dish {DishName="Котлета пожарская", Description="Хлеб - 80%, Морковь - 20%", Calories =635, DishGroupId=4, Image="3.jpeg" },
                    new Dish {DishName="Макароны по-флотски", Description="С охотничьей колбаской", Calories =524, DishGroupId=4, Image="4.jpeg" },
                    new Dish {DishName="Компот", Description="Быстро растворимый, 2 литра", Calories =180, DishGroupId=5, Image="5.jpeg" },
                    new Dish {DishName="Оливье", Description="Много мазика, очень много", Calories =175, DishGroupId=1, Image="6.jpeg" }

            });
            await context.SaveChangesAsync();
        }

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
                UserName = "user@mail.ru",
            };

            user.AvatarImage = new byte[0];  //!!! NotNullable Avatar Image!!!

            await userManager.CreateAsync(user, "123456");

            var admin = new ApplicationUser
            {
                Email = "admin@mail.ru",
                UserName = "admin@mail.ru",
            };
            admin.AvatarImage = new byte[0]; //!!! NotNullable Avatar Image!!!

            await userManager.CreateAsync(admin, "123456");

            admin = await userManager.FindByEmailAsync("admin@mail.ru");
            await userManager.AddToRoleAsync(admin, "admin");
        }
    }
}