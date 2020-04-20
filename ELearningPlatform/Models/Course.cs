using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ELearningPlatform.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string IdInstructor { get; set; }
        public string Name { get; set; }
        public string Ico { get; set; }
        public string Desc { get; set; }
        public int IdSubject { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
    }
}
