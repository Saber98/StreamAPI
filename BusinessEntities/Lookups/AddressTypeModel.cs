using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Lookups
{
    public class AddressTypeModel
    {
        public int Id { get; set; }
        public string AddressType { get; set; }
        public bool Archived { get; set; }
    }
}
