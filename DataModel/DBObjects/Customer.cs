using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int AccountRepId { get; set; }
        public virtual Contact AccountRep { get; set; }
        [Required]
        public bool ShipToBillInd { get; set; }
        public string WebsiteUrl { get; set; }
        public string AccountNumber { get; set; }
        [Required]
        public bool IsParentInd { get; set; }
        public int? ParentId { get; set; }
        public virtual Customer Parent { get; set; }
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

        public Customer()
        {
            Addresses = new HashSet<Address>();
            PhoneNumbers = new HashSet<PhoneNumber>();
            Contacts = new HashSet<Contact>();
        }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
