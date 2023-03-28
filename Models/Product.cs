using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project3.Models
{
    public partial class Product
    {
        public Product()
        {
            Purs = new HashSet<Pur>();
        }

        [Key]
        public int Pid { get; set; }
        [Display(Name = "Product Name"), StringLength(20, ErrorMessage = "The name cannot be more than 40 characters")]
        [Required(ErrorMessage = "*")]
        public string Pname { get; set; }
        [Display(Name = "Product Cost in Rupees")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Cost must at least be 1 Rupees")]

        public double? Pcost { get; set; }
        [Display(Name = "Product Quantity")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Quantity should be positive ")]

        public int? Pquantity { get; set; }

        public virtual Purchase? Purchase { get; set; }
        public virtual ICollection<Pur> Purs { get; set; }
    }
}
