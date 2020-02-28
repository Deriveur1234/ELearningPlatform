using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ELearningPlatform.Models;
using ELearningPlatform.Data;
using Microsoft.AspNetCore.Http;

namespace ELearningPlatform.Controllers
{
    //This controller is use as a router
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UsersData usersData;

        public HomeController(ILogger<HomeController> logger, ELearningPlatformContext context)
        {
            _logger = logger;
            usersData = new UsersData(context);
        }

        public IActionResult Index()
        {
            if (SessionHelper.Get<User>(HttpContext.Session, SessionHelper.SessionKeyUser) != null)
                TempData[TempDataHelper.TempdataKeyIsConnected] = true;
            return View();
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
