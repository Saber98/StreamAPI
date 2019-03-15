using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Lookups;

namespace BusinessEntities
{
    public class PhoneModel : AuditModelBase
    {
        public int Id { get; set; }
        public int PhoneTypeId { get; set; }
        public PhoneTypeModel PhoneType { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
        public bool Archived { get; set; }

    }
}
