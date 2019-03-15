using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Lookups
{
    public class StateModel
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public string StateAbbreviation { get; set; }
        public bool Archived { get; set; }

    }
}
