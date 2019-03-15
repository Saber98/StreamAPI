using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects.Lookups
{
    public class DocumentTypeLookup
    {
        [Key]
        public int Id { get; set; }
        [Required] [MaxLength(10)]
        public string DocumentType { get; set; }
        [Required] 
        public bool Archived { get; set; }
    }
}
