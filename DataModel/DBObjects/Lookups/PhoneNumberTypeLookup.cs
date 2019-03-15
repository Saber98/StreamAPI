using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects.Lookups
{
    public class PhoneTypeLookup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(8)]
        public string PhoneType { get; set; }
        [Required]
        public bool Archived { get; set; }
    }
}
