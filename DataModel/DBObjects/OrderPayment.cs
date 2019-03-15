using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DBObjects.Lookups;

namespace DataModel.DBObjects
{
    public class OrderPayment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public OrderHeader Order { get; set; }
        [Required]
        public PaymentTypeLookup PaymentType { get; set; }
        [Required]
        public double PaymentAmount { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string CreatedBy { get; set; }
    }
}
