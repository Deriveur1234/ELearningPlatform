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
    public class UsersData
    {
        private ELearningPlatformContext _context;

        public UsersData(ELearningPlatformContext context)
        {
            _context = context;
        }

        /*
         * @brief Return all users
         * @return [list<Use>] List of users
         */
        public List<User> GetAllUser()
        {
            var users = _context.User
                .ToList();

            return users;
        }


        public User GetUserByEmail(string Email)
        {
            var users = _context.User.Where(u => u.Email == Email).ToList();
            if (users.Count() > 0)
                return users[0];
            return null;
        }

        public User GetUserByUsername(string Username)
        {
            var users = _context.User.Where(u => u.Username == Username).ToList();
            if (users.Count() > 0)
                return users[0];
            return null;
        }

        /**
	     * @brief Retourne true si le password et le nickname sont dans la base
	     * @param user De type User, doit contenir le username et le password
	     * @return [bool] Retourne true si l'utilisateur et le password corréspondent
	     */
        public bool CheckLogin(User user)
        {
            var users = _context.User
                .Where(u => u.Username == user.Username && u.Password == user.Password)
                .ToList();
            return (users.Count > 0);
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
