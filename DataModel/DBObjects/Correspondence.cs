using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DBObjects.Lookups;

namespace DataModel.DBObjects
{
    public class Correspondence
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int OrderId { get; set; }
        public OrderHeader Order { get; set; }
        [Required]
        public CorrespondenceTypeLookup CorrespondenceType { get; set; }
        [Required] 
        public string CorrespondenceFile { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public bool Archived { get; set; }
    }
}
