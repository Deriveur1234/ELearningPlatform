using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearningPlatform.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string DataType { get; set; }
        
        //add equivalent of longblob 

        public int PlaceInModule { get; set; }
        public int idModule { get; set; }
    }
}
