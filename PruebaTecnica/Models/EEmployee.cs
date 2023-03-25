using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnica.Models
{
    public class EEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public System.DateTime EmploymentDate { get; set; }
    }
}