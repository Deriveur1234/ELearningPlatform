using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ELearningPlatform;
using ELearningPlatform.Models;
using ELearningPlatform.Data;
using Microsoft.Extensions.Configuration;

namespace ELearningPlatform.Data
{
    public static class UsersData
    {
        private static ELearningPlatformContext _context;
        public static List<User> GetAllUser(ELearningPlatformContext context)
        {
            _context = context;
            var users = _context.User
                .ToList();

            return users;
        }


    }
}
