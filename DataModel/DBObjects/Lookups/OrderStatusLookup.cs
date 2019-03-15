using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects.Lookups
{
    public class OrderStatusLookup
    {
        [Key]
        public int Id { get; set; }
        [Required] [MaxLength(15)]
        public string OrderStatus { get; set; }
        [Required] 
        public bool Archived { get; set; }
    }
}
