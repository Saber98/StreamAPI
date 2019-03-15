using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects.Lookups
{
    public class AddressTypeLookup
    {
        [Key]
        public int Id { get; set; }
        [Required] [MaxLength(10)]
        public string AddressType { get; set; }
        [Required] 
        public bool Archived { get; set; }
    }
}
