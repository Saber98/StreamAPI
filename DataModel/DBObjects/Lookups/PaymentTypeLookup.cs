using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects.Lookups
{
    public class PaymentTypeLookup
    {
        [Key]
        public int Id { get; set; }
        [Required] [MaxLength(8)]
        public string PaymentType { get; set; }
        [Required] 
        public bool Archived { get; set; }
    }
}
