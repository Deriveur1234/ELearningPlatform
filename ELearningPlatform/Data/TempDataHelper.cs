using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ELearningPlatform.Data
{
    public class TempDataHelper
    {
        //User tempData key
        public const string TempdataKeyIsConnected = "_IsConnected";
        public const string TempdataKeyErrorMessage = "_ErrorMessage";
        public const string TempdataKeyIdUser = "_IdUser";
        public const string TempdataKeyTokenCode = "_TokenCode";
        public const string TempdataKeyUserName = "_UserName";
        public const string TempdataKeyUserRole = "_UserRole";

        //Courses TempData Key
        public const string TempdataKeyAllCourses = "_AllCourses";
        public const string TempdataKeyInscriptionCourse = "_CourseInscripted";
        public const string TempdataKeyCourse = "_Course";
        public const string TempdataKeyIsEnrolled = "_IsEnrolled";

        //Modules TempData Key
        public const string TempdataKeyModules = "_Modules";
        public const string TempdataKeyModule = "_Module";
        public const string TempdataKeyCompletedModule = "_ModuleCompleted";
        public const string TempdataKeyModuleInstructor = "_ModuleInstructor";
        public const string TempdataKeyModulesInstructor = "_ModulesInstructor";
        public const string TempdataKeyContents = "_content";
        public const string TempdataKeyModuleIsFinished = "_isFinished";

    }
}
