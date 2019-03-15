using System;
using System.ComponentModel.DataAnnotations;
using DataModel.DBObjects.Lookups;

namespace DataModel.DBObjects
{
    public class DocumentFile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DocFileName { get; set; }
        public DocumentTypeLookup DocumentType { get; set; }
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
