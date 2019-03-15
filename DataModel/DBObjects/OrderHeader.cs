using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DBObjects.Lookups;

namespace DataModel.DBObjects
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OrderNumber { get; set; }
        [Required]
        public int OrderTypeId { get; set; }
        public OrderTypeLookup OrderType { get; set; }
        [Required]
        public string PurchaseOrder { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime OrderDueDate { get; set; }
        [Required] 
        public User TakenBy { get; set; }
        [Required]
        public User AssignedTo { get; set; }
        [Required]
        public int OrderStatusId { get; set; }
        public OrderStatusLookup OrderStatus { get; set; }
        public bool ReorderInd { get; set; }
        public OrderHeader PreviousOrder { get; set; }
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
