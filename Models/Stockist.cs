using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project3.Models
{
    public partial class Stockist
    {
        public int StId { get; set; }
        [Display(Name = "Admin Name")]
        public string? StName { get; set; }
        public string? Password { get; set; }
        public string? Cpwd { get; set; }
    }
}
