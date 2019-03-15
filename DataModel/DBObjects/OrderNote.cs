using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DBObjects
{
    public class OrderNote
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NoteValue { get; set; }
        [Required]
        public bool PrivateInd { get; set; }
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

        public virtual ICollection<Customer> Customers { get; set; }

    }
}
