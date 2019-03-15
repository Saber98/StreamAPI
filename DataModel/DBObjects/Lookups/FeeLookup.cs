using System;
using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects.Lookups
{
    public class FeeLookup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FeeCode { get; set; }
        [Required]
        public string FeeText { get; set; }
        public string FeeDescription { get; set; }
        public double FeePrice { get; set; }
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
