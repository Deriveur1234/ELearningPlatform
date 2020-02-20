using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ELearningPlatform.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public string Password { get; set; }
        public int RoleCode { get; set; }
    }
}
