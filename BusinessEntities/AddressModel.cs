using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Lookups;

namespace BusinessEntities
{
    public class AddressModel : AuditModelBase
    {
        public int Id { get; set; }
        public int AddressTypeId { get; set; }
        public AddressTypeModel AddressType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public StateModel State { get; set; }
        public string Zip { get; set; }
        public bool Archived { get; set; }
    }
}
