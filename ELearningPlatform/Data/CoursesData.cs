using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ELearningPlatform.Data
{
    public class CoursesData
    {
        private readonly ELearningPlatformContext _context;

        public const string ViewDataId = "_Id";
        public const string ViewDataName = "_Name";
        public const string ViewDataDesc = "_Desc";
        public const string ViewDataCreationDate = "_CreationDate";
        public const string ViewDataIco = "_Ico";
        public const string ViewDataIdSubject = "_IdSubject";

        public CoursesData(ELearningPlatformContext context)
        {
            _context = context;
        }

        public List<Course> GetAllCourses()
        {
            var Courses = _context.Course.ToList();
            return Courses;
        }

        public List<int> GetSubscribedCourseId(string IdUser)
        {
            List<Inscription> inscriptions = _context.Inscription.Where(e=> e.IdUser == IdUser).ToList();
            List<int> coursesId = new List<int>();
            foreach (Inscription inscription in inscriptions)
                coursesId.Add(inscription.IdCourse);
            return coursesId;
        }

        public Course GetCourseById(int IdCourse)
        {
            return _context.Course.Find(IdCourse);
        }

        public Course GetCourseByModule(int IdModule)
        {
            return _context.Course.Where(e => e.Id == _context.Module.Find(IdModule).IdCourse).FirstOrDefault();
        }

        public List<Course> GetCoursesBySubject(int IdSubject)
        {
            try
            {
                return _context.Course.Where(e => e.IdSubject == IdSubject).ToList();
            }
            catch
            {
                return new List<Course>();
            }
        }

        public bool addInscription(Inscription inscription)
        {
            try
            {
                _context.Inscription.Add(inscription);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public int GetNbModulesByCourse(int Id)
        {
            return _context.Module.Where(e => e.IdCourse == Id).Count();
        }

        public bool isEnrolled(int IdCourse, string IdUser)
        {
            return (_context.Inscription.Where(e => e.IdCourse == IdCourse && e.IdUser == IdUser).Count() > 0);
        }
    }
}
