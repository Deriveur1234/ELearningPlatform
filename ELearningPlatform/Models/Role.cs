using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearningPlatform.Models
{
    public class Role
    {
        public const int RoleGuest = 1;
        public const int RoleStudent = 2;
        public const int RoleInstructor = 3;
        public int Id { get; set; }
        public string Label { get; set; }
    }
}
