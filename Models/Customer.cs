using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project3.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Purs = new HashSet<Pur>();
        }

        public int Cid { get; set; }
        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "*"), StringLength(20, ErrorMessage = "The name cannot be more than 40 characters")]
        public string? Cname { get; set; }
        [Display(Name = "Password"), StringLength(20, ErrorMessage = "The name cannot be more than 40 characters")]
        [Required(ErrorMessage = "*")]
        public string? Pass { get; set; }
        [Display(Name = "Confirm Password"), StringLength(20, ErrorMessage = "The name cannot be more than 40 characters")]
        [Required(ErrorMessage = "*")]
        public string? Cpass { get; set; }

        public virtual ICollection<Pur> Purs { get; set; }
    }
}
