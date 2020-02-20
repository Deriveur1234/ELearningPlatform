using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ELearningPlatform.Models
{
    public class Token
    {
        public int Id { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime ValidateTill { get; set; }
    }
}
