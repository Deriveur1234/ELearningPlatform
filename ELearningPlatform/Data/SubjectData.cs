using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearningPlatform.Models;

namespace ELearningPlatform.Data
{
    public class SubjectData
    {
        private readonly ELearningPlatformContext _context;

        public SubjectData(ELearningPlatformContext context)
        {
            _context = context;
        }

        public List<Subject> GetAllSubjects()
        {
            return _context.Subject.ToList();
        }

        public int NbCoursesBySubject(int Id)
        {
            return _context.Course.Where(e => e.IdSubject == Id).Count();
        }

        public bool AddSubject(string Label)
        {
            Subject subject = new Subject();
            subject.Label = Label;
            try
            {
                _context.Subject.Add(subject);
                _context.SaveChanges();
            }
            catch { return false; }
            return true;
        }
    }
}
