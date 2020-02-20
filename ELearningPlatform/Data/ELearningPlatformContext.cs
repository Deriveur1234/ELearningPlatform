using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ELearningPlatform.Models;

namespace ELearningPlatform.Data
{
    public class ELearningPlatformContext : DbContext
    {
        public ELearningPlatformContext (DbContextOptions<ELearningPlatformContext> options)
            : base(options)
        {
        }

        public DbSet<ELearningPlatform.Models.User> User { get; set; }

        public DbSet<ELearningPlatform.Models.Course> Course { get; set; }

        public DbSet<ELearningPlatform.Models.CompletedCourse> CompletedCourse { get; set; }

        public DbSet<ELearningPlatform.Models.Role> Role { get; set; }

        public DbSet<ELearningPlatform.Models.Participation> Participation { get; set; }

        public DbSet<ELearningPlatform.Models.Token> Token { get; set; }

        public DbSet<ELearningPlatform.Models.PageCourse> PageCourse { get; set; }
    }
}
