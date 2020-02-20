using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ELearningPlatform.Models
{
    public class PageCourse
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public int IdCourse { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreation { get; set; }
    }
}
