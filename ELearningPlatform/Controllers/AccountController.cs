using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELearningPlatform.Data;
using ELearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ELearningPlatform.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ELearningPlatformContext _context;
        private UsersData usersData;

        public AccountController(ILogger<HomeController> logger, ELearningPlatformContext context)
        {
            _logger = logger;
            _context = context;
            usersData = new UsersData(_context);
            
            if (_context.User.Count() == 0)
            {
                // Create a new User if collection is empty,
                // which means you can't delete all users.
                _context.User.Add(new User { Email = "loicburnand@gmail.com", IsConfirmed = true, IdCode = 3 });
                _context.SaveChanges();
            }
            usersData.GetAllUser();
        }

        [HttpGet("{id=0}")]
        public async Task<IActionResult> GetUser(int id)
        {
            usersData.GetAllUser();

            return NoContent();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromForm] string Username, [FromForm] string Password)
        {
            if (Username == null || Password == null)
                return View("Login");
            User user = new User();
            user.Username = Username;
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            user.Password = GetSha1(Password);
            if(usersData.CheckLogin(user))
            {
                user = usersData.GetUserByUsername(user.Username);
                if (!user.IsConfirmed)
                {
                    TempData[TempDataHelper.TempdataKeyErrorMessage] = "Please check your email to confirm your account";
                    return View("Index");
                }
                // dont want to stock the password in session even if hashed
                user.Password = null;
                // Put in session an serialized object User 
                SessionHelper.Set<User>(HttpContext.Session, SessionHelper.SessionKeyUser, user);
                TempData[TempDataHelper.TempdataKeyIsConnected] = true;
            }
            return View("Index");
        }

        [Route("LogOut")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            SessionHelper.Set<User>(HttpContext.Session, SessionHelper.SessionKeyUser, null);
            TempData[TempDataHelper.TempdataKeyIsConnected] = null;
            return View("Index");
        }

        [Route("SignUp")]
        [HttpGet]
        public async Task<IActionResult> SignUpUser(string Email, string Username, string Password)
        {
            User newUser = new User();
            newUser.IdCode = Role.RoleGuest;
            newUser.IsConfirmed = false;
            newUser.Username = Username;
            newUser.Email = Email;
            newUser.Password = GetSha1(Password);
            if(EmailExists(newUser.Email) || UsernameExists(newUser.Username))
            {
                TempData[TempDataHelper.TempdataKeyErrorMessage] = "Email or username already exist";
                return View("SignUp");
            }
            else
            {
                _context.User.Add(newUser);
                _context.SaveChanges();
                _context.Entry(newUser).GetDatabaseValues();
                CreateToken(newUser.Id);
            }
            return View("Index");
        }

        private bool CreateToken(int IdUser)
        {
            Token t = new Token();
            t.Id = IdUser;
            t.ValidateTill = DateTime.Now.AddDays(1f);
            _context.Token.Add(t);
            _context.SaveChanges();
            return false;
        }
        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        private bool EmailExists(string email)
        {
            return _context.User.Any(e => e.Email == email);
        }

        private bool UsernameExists(string username)
        {
            return _context.User.Any(e => e.Username == username);
        }

        private static string GetSha1(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var hashData = new SHA1Managed().ComputeHash(data);
            var hash = string.Empty;
            foreach (var b in hashData)
            {
                hash += b.ToString("X2");
            }
            return hash;
        }
    }
}
