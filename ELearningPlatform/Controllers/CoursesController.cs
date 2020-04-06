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
        private readonly UsersData _usersData;
        private readonly ModuleData _moduleData;
        private readonly SubjectData _subjectData;

        public CoursesController(ELearningPlatformContext context)
        {
            _context = context;
            _coursesData = new CoursesData(_context);
            _usersData = new UsersData(_context);
            _moduleData = new ModuleData(_context);
            _subjectData = new SubjectData(_context);
        }

        public IActionResult Index()
        {
            User user = SessionHelper.Get<User>(HttpContext.Session, SessionHelper.SessionKeyUser);
            if (user != null)
            {
                List<int> coursesId = _coursesData.GetSubscribedCourseId(user.Id);
                TempData[TempDataHelper.TempdataKeyInscriptionCourse] = coursesId;
                TempData[TempDataHelper.TempdataKeyIsConnected] = true;
                TempData[TempDataHelper.TempdataKeyUserName] = user.Username;
                TempData[TempDataHelper.TempdataKeyUserRole] = _usersData.GetRoleName(user.IdCode);
            }
            TempData[TempDataHelper.TempdataKeyAllCourses] = _coursesData.GetAllCourses();
            TempData[TempDataHelper.TempdataKeySubjects] = _subjectData.GetAllSubjects();
            return View();
        }

        [Route("CourseDetails")]
        [HttpGet]
        public IActionResult CourseDetails(int id)
        {
            TempData[TempDataHelper.TempdataKeyCourse] = _coursesData.GetCourseById(id);
            User user = SessionHelper.Get<User>(HttpContext.Session, SessionHelper.SessionKeyUser);
            if(user != null)
            {
                TempData[TempDataHelper.TempdataKeyIsEnrolled] = _coursesData.isEnrolled(id, user.Id);
                TempData[TempDataHelper.TempdataKeyIsConnected] = true;
                TempData[TempDataHelper.TempdataKeyUserName] = user.Username;
                TempData[TempDataHelper.TempdataKeyUserRole] = _usersData.GetRoleName(user.IdCode);
                TempData[TempDataHelper.TempdataKeyCompletedModule] = _moduleData.GetCompletedModule(id, user.Id);
            }
            else
            {
                TempData[TempDataHelper.TempdataKeyIsEnrolled] = false;
            }

            TempData[TempDataHelper.TempdataKeyModules] = _moduleData.GetModulesCourse(id);
            TempData[TempDataHelper.TempdataKeyModulesInstructor] = _moduleData.GetInstructorForAllModuleCourse(id);
            return View("CourseDetail");
        }

        [Route("Enroll")]
        [HttpGet]
        public IActionResult EnrollCourse(int id)
        {
            User user = SessionHelper.Get<User>(HttpContext.Session, SessionHelper.SessionKeyUser);
            if(user != null && id != 0)
            {
                Inscription inscription = new Inscription();
                inscription.IdUser = user.Id;
                inscription.IdCourse = id;
                if (_coursesData.addInscription(inscription))
                    return CourseDetails(id);
            }
            return RedirectToAction("Index", "home");
        }


        [Route("Ajax/GetIdCourseBySubject")]
        [HttpGet]
        public String GetIdCourseBySubject(int IdSubject)
        {
            if (IdSubject == 0)
                return "0";
            return JsonConvert.SerializeObject(_coursesData.GetCoursesBySubject(IdSubject));
        }

        [Route("Listing")]
        [HttpGet]
        public IActionResult ListCoursesBySubject(int id)
        {
            List<Course> courses = _coursesData.GetCoursesBySubject(id);
            int[] nbModuleByCourse = new int[courses.Count()];
            int i = 0;
            foreach (Course course in courses)
            {
                nbModuleByCourse[i] = _coursesData.GetNbModulesByCourse(course.Id);
                i++;
            }
            TempData[TempDataHelper.TempdataKeyAllCourses] = courses;
            TempData[TempDataHelper.TempdataKeyNbModuleByCourse] = nbModuleByCourse;
            return View("CoursesListing");
        }

    }
}