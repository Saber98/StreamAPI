using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DBObjects
{
    public class OrderPrice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public OrderHeader Order { get; set; }
        [Required]
        public double SubTotal { get; set; }
        [Required]
        public double TaxRate { get; set; }
        [Required]
        public double TaxAmount { get; set; }
        [Required]
        public double Shipping { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public double BalanceDue { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime LastModified { get; set; }
        [Required]
        public string LastModifiedBy { get; set; }
        [Required]
        public bool Archived { get; set; }
    }
}
