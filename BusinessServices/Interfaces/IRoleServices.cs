using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices.Interfaces
{
    public interface IRoleServices
    {
        RoleModel GetById(int id);
        IEnumerable<RoleModel> GetAll(bool includeArchived);
        int Create(RoleModel userRoleModel);
        bool Update(int id, RoleModel userRoleModel);
        bool Delete(int id, bool hardDelete);
    }
}
