﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearningPlatform.Models
{
    public class Module
    {
        public int Id { get; set; }
        public int IdCourse { get; set; }
        public string Label { get; set; }
        public int IdInstructor { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
