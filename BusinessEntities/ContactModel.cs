using System;
using System.Collections.Generic;
using BusinessEntities.Lookups;

namespace BusinessEntities
{
    public class ContactModel : AuditModelBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<PhoneModel> PhoneNumbers { get; set; }
        public bool Archived { get; set; }
    }
}
