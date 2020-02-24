using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ELearningPlatform.Data;

namespace ELearningPlatform.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ELearningPlatformContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ELearningPlatformContext>>()))
            {
                // Look for any tokens.
                if (!context.Role.Any())
                {
                    context.Role.AddRange(
                        new Role
                        {
                            Label = "Guest"
                        },
                        new Role
                        {
                            Label = "Student"
                        },
                        new Role
                        {
                            Label = "Instructor"
                        }
                    );
                    context.SaveChanges();
                }
                if(!context.User.Any())
                {
                    context.User.AddRange(
                        new User
                        {
                            Email = "ELearningDefaultUser@protonmail.com", //account specially created as default user
                            IdCode = 3,
                            IsConfirmed = true,
                            Password = "a68181b5a222ee9db77baa62ce1e9f43e4fcf643"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
