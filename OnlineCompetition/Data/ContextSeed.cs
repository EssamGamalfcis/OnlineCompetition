using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Models;
using OnlineCompetition.MVC;

namespace AdminPanel
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(OnlineCompetition.MVC.Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(OnlineCompetition.MVC.Enums.Roles.Teacher.ToString()));
            await roleManager.CreateAsync(new IdentityRole(OnlineCompetition.MVC.Enums.Roles.Corrector.ToString()));
            await roleManager.CreateAsync(new IdentityRole(OnlineCompetition.MVC.Enums.Roles.Student.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FirstName = "adimn",
                LastName = "",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "P@ssw0rd");
                    await userManager.AddToRoleAsync(defaultUser, OnlineCompetition.MVC.Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, OnlineCompetition.MVC.Enums.Roles.Teacher.ToString());
                    await userManager.AddToRoleAsync(defaultUser, OnlineCompetition.MVC.Enums.Roles.Corrector.ToString());
                    await userManager.AddToRoleAsync(defaultUser, OnlineCompetition.MVC.Enums.Roles.Student.ToString());
                }

            }
        }
    }
}
