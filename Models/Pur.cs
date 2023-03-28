using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project3.Models
{
    public partial class Pur
    {
        public int Puid { get; set; }
        [Display(Name = "Product Id")]

        public int? Pid { get; set; }
        [Display(Name = "Product Name"), StringLength(20, ErrorMessage = "The name cannot be more than 40 characters")]
        [Required(ErrorMessage = "*")]
        public string? Pname { get; set; }
        [Display(Name = "Purchase Quantity")]
        [Required(ErrorMessage = "*")]
        public int? Purquantity { get; set; }
        public int? TotalPrice { get; set; }
        public int? Cid { get; set; }
        public bool? Flag { get; set; }

        public virtual Customer? CidNavigation { get; set; }
        public virtual Product? PidNavigation { get; set; }
    }
}
