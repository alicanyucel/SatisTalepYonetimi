using Microsoft.AspNetCore.Identity;
using SatisTalepYonetimi.Domain.Entities;

namespace SatisTalepYonetimi.WebAPI.Middlewares
{
    public static class ExtensionsMiddleware
    {
        public static void CreateFirstUser(WebApplication app)
        {
            using (var scoped = app.Services.CreateScope())
            {
                var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                if (!userManager.Users.Any(p => p.UserName == "admin"))
                {
                    AppUser user = new()
                    {
                        UserName = "admin",
                        Email = "admin@admin.com",
                        FirstName = "Mudbey",
                        LastName = "Yazilim",
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(user, "MUDBEY1235").Wait();
                }
            }
        }
    }
}
