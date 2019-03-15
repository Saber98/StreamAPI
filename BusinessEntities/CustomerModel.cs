using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class CustomerModel : AuditModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccountRepId { get; set; }
        public ContactModel AccountRep { get; set; }
        public bool ShipToBill { get; set; }
        public List<AddressModel> Addresses { get; set; }
        public List<PhoneModel> PhoneNumbers { get; set; }
        public List<ContactModel> Contacts { get; set; }
        public string WebsiteUrl { get; set; }
        public string AccountNumber { get; set; }
        public int? ParentId { get; set; }
        public bool IsParent { get; set; }
        public bool Archived { get; set; }
    }
}
