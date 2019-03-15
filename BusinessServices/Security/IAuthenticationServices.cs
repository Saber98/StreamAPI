using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices.Interfaces
{
    public interface IAuthenticationServices
    {
        UserSecurityModel GetUserByUserName(string userName);
        bool UpdateLoginStatus(int userId, string jwToken, DateTime tokenExpiration);
    }
}
