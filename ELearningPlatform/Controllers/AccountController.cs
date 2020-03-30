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
using EmailService;

namespace ELearningPlatform.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ELearningPlatformContext _context;
        private readonly UsersData usersData;
        private readonly EmailSender _emailSender;
        private static Random r = new Random();

        public AccountController(ILogger<HomeController> logger, ELearningPlatformContext context, EmailConfiguration emailConfiguration)
        {
            _logger = logger;
            _context = context;
            usersData = new UsersData(_context);
            _emailSender = new EmailSender(emailConfiguration);
            
            
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
            user.Password = GetSha1(Password);
            if (usersData.CheckLogin(user))
            {
                user = usersData.GetUserByUsername(user.Username);
                if (!user.IsConfirmed)
                {
                    return View("ResendEmail");
                }
                // dont want to stock the password in session even if hashed
                user.Password = null;
                // Put in session an serialized object User 
                SessionHelper.Set<User>(HttpContext.Session, SessionHelper.SessionKeyUser, user);
                return RedirectToAction("Index", "home");
            }
            else
                TempData[TempDataHelper.TempdataKeyErrorMessage] = "Username or password incorrect";
            return View("Login");
        }

        [Route("LogOut")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            SessionHelper.Set<User>(HttpContext.Session, SessionHelper.SessionKeyUser, null);
            TempData[TempDataHelper.TempdataKeyIsConnected] = null;
            return RedirectToAction("Index", "home");
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
                SendActivationMail(newUser.Email, CreateToken(newUser).Code);
            }
            return RedirectToAction("Index", "home");
        }

        [Route("Activate")]
        [HttpGet]
        public async Task<IActionResult> ActivateUser(string Code)
        {
            User user;
            try
            {
                List<Token> token = _context.Token.Where(e => e.Code == Code).ToList();
                if (token.Count > 0 && DateTime.Compare(token[0].ValidateTill, DateTime.Now) > 0)
                {
                    user = _context.User.Find(token[0].Id);
                    user.IsConfirmed = true;
                    await _context.SaveChangesAsync();
                }
            }catch
            {
                return RedirectToAction("Index", "home");
            }

            return View("Login");
        }

        [Route("ForgotPassword")]
        [HttpGet]
        public IActionResult ForgotPasswordUser(string Email)
        {
            if(Email != null)
            {
                List<User> user = _context.User.Where(e => e.Email == Email).ToList(); 
                if(user.Count > 0)
                {
                    if(DeleteToken(user[0].Id))
                    {
                        Token token = CreateToken(user[0]);
                        SendResetPasswordMail(user[0].Email, token.Code);
                    }
                }
            }
            return View("Login");
        }

        [Route("ResetPassword")]
        [HttpGet]
        public IActionResult ResetPasswordUser(string Id, string newPassword)
        {
            try
            {
                List<User> user = _context.User.Where(e => e.Id == Convert.ToInt32(Id)).ToList();
                if(user.Count > 0)
                {
                    user[0].Password = GetSha1(newPassword);
                    _context.SaveChanges();
                }
            }
            catch
            {
                return View("ResetPassword");
            }
            return View("Login");
        }

        [Route("ResendEmail")]
        [HttpGet]
        public IActionResult ResendEmailUser(string Email)
        {
            if(Email != null)
            {
                User user = usersData.GetUserByEmail(Email);
                DeleteToken(user.Id);
                Token token = CreateToken(user);
                SendActivationMail(user.Email, token.Code);
                return RedirectToAction("Login", "home");
            }
            return View("ResendEmail");
        }


        private Token CreateToken(User User)
        {
            Token t = new Token();
            t.Id = User.Id;
            t.ValidateTill = DateTime.Now.AddDays(1f);
            t.Code = GenerateCode();
            _context.Token.Add(t);
            _context.SaveChanges();
            return t;
        }

        private bool DeleteToken(int IdUser)
        {
            try
            {
                _context.Token.Remove(_context.Token.Find(IdUser));
                _context.SaveChanges();
            }catch
            {
                return false;
            }
            return true;
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

        private bool SendActivationMail(string newUserEmail, string TokenCode)
        {
            try
            {
                var message = new Message(new string[] { newUserEmail }, "Account confirmation", "Use this link to activate your email : https://localhost:44351/Account/Activate?Code=" + TokenCode);
                _emailSender.SendEmail(message);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool SendResetPasswordMail(string email, string tokenCode)
        {
            try
            {
                var message = new Message(new string[] { email }, "Reset Password", "Use this link to reset your password : https://localhost:44351/Home/ResetPassword?Code=" + tokenCode);
                _emailSender.SendEmail(message);
            }
            catch
            {
                return false;
            }
            return true;
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

        public static string GenerateCode(int length = 20)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[r.Next(s.Length)]).ToArray());
        }
    }
}
