

namespace BlogEngine.Repository.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminId = await EnsureUser(serviceProvider, testUserPw, "admin@blogengine.com");
                await EnsureRole(serviceProvider, adminId, Constants.AdminRole);

                // allowed user can create and edit contacts that they create
                var writerId = await EnsureUser(serviceProvider, testUserPw, "writer@blogengine.com");
                await EnsureRole(serviceProvider, writerId, Constants.WriterRole);
            }
        }

        private static async Task<string> EnsureUser(
            IServiceProvider serviceProvider,
            string userPassword,
            string userName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new IdentityUser { UserName = userName };
                await userManager.CreateAsync(user, userPassword);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(
            IServiceProvider serviceProvider,
            string uid,
            string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            return await userManager.AddToRoleAsync(user, role);
        }
    }
}
