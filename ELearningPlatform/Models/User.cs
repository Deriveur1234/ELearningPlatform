using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearningPlatform.Models
{
    public class User
    {
        public int Id { get; set; }
        public int IdCode { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public string Password { get; set; }
    }
}
