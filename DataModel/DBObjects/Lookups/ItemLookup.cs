using System;
using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects.Lookups
{
    public class ItemLookup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ItemCode { get; set; }
        [Required]
        public string ItemText { get; set; }
        public string ItemDescription { get; set; }
        public double ItemPrice { get; set; }
        public bool TaxableInd { get; set; }
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
