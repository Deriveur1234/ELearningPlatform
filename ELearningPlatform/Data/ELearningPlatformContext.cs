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

        public DbSet<ELearningPlatform.Models.Course> Course { get; set; }

        public DbSet<ELearningPlatform.Models.User> User { get; set; }

        public DbSet<ELearningPlatform.Models.Token> Token { get; set; }
        public DbSet<ELearningPlatform.Models.Role> Role { get; set; }

        public DbSet<ELearningPlatform.Models.Completed> Completed { get; set; }

        public DbSet<ELearningPlatform.Models.Content> Content { get; set; }

        public DbSet<ELearningPlatform.Models.Inscription> Inscription { get; set; }

        public DbSet<ELearningPlatform.Models.Module> Module { get; set; }

        public DbSet<ELearningPlatform.Models.Subject> Subject { get; set; }


    }
}
