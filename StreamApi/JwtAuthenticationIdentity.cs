using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace StreamApi
{
    public class JwtAuthenticationIdentity : GenericIdentity
    {

        public string UserName { get; set; }
        public int UserId { get; set; }
        public IList<string> Roles { get; set; }

        public JwtAuthenticationIdentity(string userName)
            : base(userName)
        {
            UserName = userName;
            Roles = new List<string>();
        }


    }
}