using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearningPlatform.Models;

namespace ELearningPlatform.Data
{
    public class ModuleData
    {
        public const string DataTypeImg = "img";
        public const string DataTypeString = "string";

        private readonly ELearningPlatformContext _context;

        public ModuleData(ELearningPlatformContext context)
        {
            _context = context;
        }

        public Module GetModule(int IdModule)
        {
            return _context.Module.Find(IdModule);
        }

        public List<Module> GetModulesCourse(int IdCourse)
        {
            return _context.Module.Where(m => m.IdCourse == IdCourse).ToList();
        }

        public List<int> GetCompletedModule(int IdCourse, int IdUser)
        {
            List<Module> modules = _context.Module.Where(m => m.IdCourse == IdCourse).ToList();
            List<int> completedModules = new List<int>();
            foreach(Module module in modules)
            {
                if (_context.Completed.Where(e => e.IdModule == module.Id && e.IdUser == IdUser).ToList().Count() > 0)
                    completedModules.Add(module.Id);
            }
            return completedModules;
        }

        public string GetInstructor(int IdModule)
        {
            return _context.User.Where(e => e.Id == _context.Module.Find(IdModule).IdInstructor).FirstOrDefault().Username;
        }

        public IDictionary<int, string> GetInstructorForAllModuleCourse(int idCourse)
        {
            List<Module> modules = GetModulesCourse(idCourse);
            IDictionary<int, string> InstructorName = new Dictionary<int, string>();
            foreach(Module module in modules)
            {
                InstructorName[module.Id] = GetInstructor(module.Id);
            }
            return InstructorName;
        }

        public List<Content> GetContents(int idModule)
        {
            return _context.Content.Where(e => e.idModule == idModule).ToList();
        }

        public bool isFinished(int idModule, int idUser)
        {
            return (_context.Completed.Where(e => e.IdModule == idModule && e.IdUser == idUser).Count() > 0);
        }
    }
}
