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

namespace ELearningPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ELearningPlatformContext _context;
        private UsersData usersData;
        private CoursesData coursesData;

        public HomeController(ELearningPlatformContext context)
        {
            _context = context;
            usersData = new UsersData(_context);
            coursesData = new CoursesData(_context);
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Courses");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        public IActionResult SignUp()
        {
            return View("SignUp");
        }

        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

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
