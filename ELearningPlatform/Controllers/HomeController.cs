using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ELearningPlatform.Models;
using ELearningPlatform.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ELearningPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ELearningPlatformContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private UsersData usersData;
        private CoursesData coursesData;

        public HomeController(ELearningPlatformContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
            usersData = new UsersData(_context, this.userManager);
            coursesData = new CoursesData(_context);
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Courses");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("Login");
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View("SignUp");
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        public IActionResult Listing()
        {
            return RedirectToAction("ListAllSubjects", "Subjects");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword(string Code)
        {
            TempData[TempDataHelper.TempdataKeyIdUser] = usersData.GetIdUserByTokenCode(Code);
            return View("ResetPassword");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
