﻿using CialExamMVC_Trial.Exceptions;
using CialExamMVC_Trial.Helpers.Enums;
using CialExamMVC_Trial.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CialExamMVC_Trial.Helpers
{
    public static class Seed
    {
        public static IApplicationBuilder UseSeedData(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                using var scope = context.RequestServices.CreateScope();
                var userManager = context.RequestServices.GetRequiredService<UserManager<AppUser>>();
                var roleManager = context.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.Roles.AnyAsync())
                    await CreateRolesAsync(roleManager);
                if (await userManager.FindByNameAsync(app.Configuration["Admin:Username"]) == null)
                    await CreateAdminUser(userManager, app);

                await next();
            });

            return app;
        }

        public static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {

            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded) 
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var error in result.Errors)
                    {
                        sb.Append(error.Description + " ");
                    }
                    throw new RolesCreationFailedException(sb.ToString().TrimEnd());
                }
            }
        }

        public static async Task CreateAdminUser(UserManager<AppUser> userManager, WebApplication app)
        {
            var user = new AppUser
            {
                UserName = app.Configuration["Admin:Username"]
            };
            var result = await userManager.CreateAsync(user, app.Configuration["Admin:Password"]);
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description + " ");
                }
                throw new AdminUserCreationFailedException(sb.ToString().TrimEnd());
            }
            var roleResult = await userManager.AddToRoleAsync(user, nameof(Roles.Admin));
            if (!roleResult.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description + " ");
                }
                throw new AdminUserCreationFailedException(sb.ToString().TrimEnd());
            }

        }
    }
}
