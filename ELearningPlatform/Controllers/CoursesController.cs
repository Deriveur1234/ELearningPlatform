using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELearningPlatform.Data;
using ELearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ELearningPlatform.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ELearningPlatformContext _context;
        private readonly CoursesData _coursesData;

        public CoursesController(ELearningPlatformContext context)
        {
            _context = context;
            _coursesData = new CoursesData(_context);
        }

        public IActionResult Index()
        {
            User user = SessionHelper.Get<User>(HttpContext.Session, SessionHelper.SessionKeyUser);
            if (user != null)
            {
                List<int> coursesId = _coursesData.GetSubscribedCourseId(user.Id);
                TempData[TempDataHelper.TempdataKeyInscriptionCourse] = coursesId;
                TempData[TempDataHelper.TempdataKeyIsConnected] = true;
                
            }
            TempData[TempDataHelper.TempdataKeyAllCourses] = _coursesData.GetAllCourses();
            return View();
        }

        [Route("CourseDetails")]
        [HttpGet]
        public IActionResult CourseDetails(int id)
        {
            TempData[TempDataHelper.TempdataKeyCourse] = _coursesData.GetCourseById(id);
            return View("CourseDetail");
        }
    }
}