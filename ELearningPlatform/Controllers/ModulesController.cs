using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELearningPlatform.Models;
using ELearningPlatform.Data;
using Microsoft.AspNetCore.Identity;

namespace ELearningPlatform.Controllers
{
    public class ModulesController : Controller
    {
        private readonly ELearningPlatformContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly CoursesData _coursesData;
        private readonly UsersData _usersData;
        private readonly ModuleData _moduleData;

        public ModulesController(ELearningPlatformContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
            _coursesData = new CoursesData(_context);
            _usersData = new UsersData(_context, userManager);
            _moduleData = new ModuleData(_context);
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("ModuleDetails")]
        [HttpGet]
        public IActionResult DetailsModule(int id)
        {
            User user = SessionHelper.Get<User>(HttpContext.Session, SessionHelper.SessionKeyUser);
            List<Content> contents = _moduleData.GetContents(id);
            TempData[TempDataHelper.TempdataKeyContents] = contents;
            TempData[TempDataHelper.TempdataKeyModule] = _moduleData.GetModule(id);
            TempData[TempDataHelper.TempdataKeyModuleInstructor] = _moduleData.GetInstructor(id);
            TempData[TempDataHelper.TempdataKeyCourse] = _coursesData.GetCourseByModule(id);
            if(user != null)
                TempData[TempDataHelper.TempdataKeyModuleIsFinished] = _moduleData.isFinished(id, User.Identity.Name);
            return View("ModuleDetails");
        }

        [Route("ModuleListing")]
        [HttpGet]
        public IActionResult ModuleListingByCourse(int id)
        {
            List<Module> modules = _moduleData.GetModulesCourse(id);
            TempData[TempDataHelper.TempdataKeyModules] = modules;
            return View("ListModule");
        }

        [Route("CompleteModule")]
        [HttpGet]
        public IActionResult CompleteModule(int id)
        {
            User user = SessionHelper.Get<User>(HttpContext.Session, SessionHelper.SessionKeyUser);
            if(user != null)
            {
                if (_moduleData.CompleteModule(id, User.Identity.Name))
                    return DetailsModule(id);
            }
            return RedirectToAction("Login", "Home");
        }
    }
}