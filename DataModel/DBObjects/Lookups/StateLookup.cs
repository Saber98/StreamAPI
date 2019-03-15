using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects.Lookups
{
    public class StateLookup
    {
        [Key]
        public int Id { get; set; }
        [Required] [MaxLength(2)]
        public string StateAbbreviation { get; set; }
        [Required] [MaxLength(35)]
        public string StateName { get; set; }
        [Required]
        public bool Archived { get; set; }
    }   
}
