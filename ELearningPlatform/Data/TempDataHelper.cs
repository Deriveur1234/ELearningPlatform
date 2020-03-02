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

        //Courses TempData Key
        public const string TempdataKeyAllCourses = "_AllCourses";
        public const string TempdataKeyInscriptionCourse = "_CourseInscripted";
        public const string TempdataKeyCourse = "_Course";
        public const string TempdataKeyIsEnrolled = "_IsEnrolled";
    }
}
