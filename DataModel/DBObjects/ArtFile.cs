using System;
using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects
{
    public class ArtFile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImageFile { get; set; }
        public string ArtFolder { get; set; }
        public string Note { get; set; }
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
