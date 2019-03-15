using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Lookups;

namespace BusinessEntities
{
    public class RoleModel : AuditModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Archived { get; set; }

        public List<UserModel> AssignedUsers { get; set; }

    }
}
