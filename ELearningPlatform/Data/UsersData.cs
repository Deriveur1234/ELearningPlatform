using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ELearningPlatform;
using ELearningPlatform.Models;
using ELearningPlatform.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace ELearningPlatform.Data
{
    public class UsersData
    {
        private ELearningPlatformContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public UsersData(ELearningPlatformContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }


        // still change to user identity framework
        public User GetUserByEmail(string Email)
        {
            var users = _context.User.Where(u => u.Email == Email).ToList();
            var user = userManager.FindByEmailAsync(Email);
            if (users.Count() > 0)
                return users[0];
            return null;
        }

        public User GetUserByUsername(string Username)
        {
            var users = _context.User.Where(u => u.Username == Username).ToList();
            var user = userManager.FindByNameAsync(Username);
            if (users.Count() > 0)
                return users[0];
            return null;
        }

        public int GetIdUserByTokenCode(string TokenCode)
        {
            return _context.User.Find(_context.Token.Where(e => e.Code == TokenCode).FirstOrDefault().Id).Id;
        }

        public string GetRoleName(int IdRole)
        {
            return _context.Role.Find(IdRole).Label;
        }

    }
}
