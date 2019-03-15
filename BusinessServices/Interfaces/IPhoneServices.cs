using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessEntities.Lookups;

namespace BusinessServices.Interfaces
{
    public interface IPhoneServices
    {
        PhoneModel GetById(int id);
        IEnumerable<PhoneModel> GetAll(bool includeArchived);
        int Create(PhoneModel phoneModel);
        bool Update(int id, PhoneModel phoneModel);
        bool Delete(int id, bool hardDelete);
    }
}
