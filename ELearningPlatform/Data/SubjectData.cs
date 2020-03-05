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
    }
}
