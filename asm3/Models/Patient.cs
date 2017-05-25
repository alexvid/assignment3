using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asm3.Models
{
    public class Patient
    {
        public int id { get; set; }
        public string idCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        public string address { get; set; }
    }
}