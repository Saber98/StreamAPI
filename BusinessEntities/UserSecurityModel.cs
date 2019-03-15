using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Lookups;

namespace BusinessEntities
{
    public class UserSecurityModel : AuditModelBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsLockedOut { get; set; }
        public string IpAddress { get; set; }

    }
}
