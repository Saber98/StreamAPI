using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataModel.DBObjects.Lookups;

namespace DataModel.DBObjects
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AddressTypeId { get; set; }
        public virtual AddressTypeLookup AddressType { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int StateId { get; set; }
        public virtual StateLookup State { get; set; }
        [Required]
        public string Zip { get; set; }
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
