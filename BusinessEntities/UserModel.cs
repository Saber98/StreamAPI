using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Lookups;

namespace BusinessEntities
{
    public class UserModel : AuditModelBase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public int ContactId { get; set; }
        public virtual ContactModel Contact { get; set; }
        public bool Archived { get; set; }

        public virtual ICollection<RoleModel> Roles { get; set; }

    }
}
