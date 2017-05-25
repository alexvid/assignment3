using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asm3.Models
{
    public class Consultatie
    {
        public int ID { get; set; }
        public int IDPatient { get; set; }
        public int IDDoctor { get; set; }
        public DateTime schedule { get; set; }
    }
}