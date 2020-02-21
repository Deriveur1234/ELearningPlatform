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
                if (context.Role.Any())
                {
                    return;   // DB has been seeded
                }

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
        }
    }
}
