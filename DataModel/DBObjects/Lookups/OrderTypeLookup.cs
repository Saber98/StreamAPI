using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects.Lookups
{
    public class OrderTypeLookup
    {
        [Key]
        public int Id { get; set; }
        [Required] [MaxLength(35)]
        public string OrderType { get; set; }
        [Required] 
        public bool Archived { get; set; }
    }
}
