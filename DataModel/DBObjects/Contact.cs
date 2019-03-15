using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
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
        [Required]
        public int UserId { get; set; }

        public Contact()
        {
            Addresses = new HashSet<Address>();
            PhoneNumbers = new HashSet<PhoneNumber>();
            Customers = new HashSet<Customer>();
        }
        public virtual ICollection<Address> Addresses {get; set;}
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
