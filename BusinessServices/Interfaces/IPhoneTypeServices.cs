using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessEntities.Lookups;

namespace BusinessServices.Interfaces
{
    public interface IPhoneTypeServices
    {
        PhoneTypeModel GetById(int id);
        IEnumerable<PhoneTypeModel> GetAll(bool includeArchived);
        int Create(PhoneTypeModel phoneTypeModel);
        bool Update(int id, PhoneTypeModel phoneTypeModel);
        bool Delete(int id, bool hardDelete);
    }
}
