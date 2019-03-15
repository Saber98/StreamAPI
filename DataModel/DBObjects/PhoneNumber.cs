using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DBObjects.Lookups;

namespace DataModel.DBObjects
{
    public class PhoneNumber
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PhoneTypeId { get; set; }
        public virtual PhoneTypeLookup PhoneType { get; set; }
        [MaxLength(3)]
        public string AreaCode { get; set; }
        [MaxLength(8)]
        public string Number { get; set; }
        [MaxLength(6)]
        public string Extension { get; set; }
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

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
