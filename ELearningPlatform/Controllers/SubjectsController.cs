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
using Microsoft.AspNetCore.Authorization;

namespace ELearningPlatform.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubjectsController : Controller
    {
        private readonly ELearningPlatformContext _context;
        private readonly SubjectData _subjectData;

        public SubjectsController(ELearningPlatformContext context)
        {
            _context = context;
            _subjectData = new SubjectData(_context);
        }

        [Route("Listing")]
        [Authorize]
        public IActionResult ListAllSubjects()
        {
            List<Subject> subjects = _subjectData.GetAllSubjects();
            int[] nbModuleBySubjects = new int[subjects.Count()];
            int i = 0;
            foreach (Subject subject in subjects)
            {
                nbModuleBySubjects[i] = _subjectData.NbCoursesBySubject(subject.Id);
                i++;
            }
            TempData[TempDataHelper.TempdataKeySubjects] = subjects;
            TempData[TempDataHelper.TempdataKeyNbCoursesBySubject] = nbModuleBySubjects;
            return View("ListingSubject");
        }

        [Route("AddSubject")]
        [Authorize]
        public IActionResult AddSubject(string SubjectLabel)
        {
            if (SubjectLabel != null)
                _subjectData.AddSubject(SubjectLabel);
            return RedirectToAction("Listing", "Subjects");
        }
    }
}