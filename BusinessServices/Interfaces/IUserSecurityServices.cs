using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataModel.DBObjects;

namespace BusinessServices.Interfaces
{
    public interface IUserSecurityServices
    {
        UserSecurityModel GetById(int id);
        IEnumerable<UserSecurityModel> GetAll(bool includeArchived);
        int Create(UserSecurityModel userSecurityModel);
        bool Update(int id, UserSecurityModel userSecurityModel);
        bool Delete(int id, bool hardDelete);
    }
}
