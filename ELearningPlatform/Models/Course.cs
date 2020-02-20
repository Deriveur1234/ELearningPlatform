﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearningPlatform.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool IsEnable { get; set; }
        public int TeacherId { get; set; }
    }
}
