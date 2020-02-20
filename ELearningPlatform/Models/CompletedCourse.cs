using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearningPlatform.Models
{
    public class CompletedCourse
    {
        public int Id { get; set; }
        public int IdParticipation { get; set; }
        public int IdPage { get; set; }
        public bool IsCompleted { get; set; }
    }
}
