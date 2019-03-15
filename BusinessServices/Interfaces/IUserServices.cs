using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices.Interfaces
{
    public interface IUserServices
    {
        UserModel GetById(int id);
        IEnumerable<UserModel> GetAll(bool includeArchived);
        int Create(UserModel userModel);
        bool Update(int id, UserModel userModel);
        bool Delete(int id, bool hardDelete);
    }
}
