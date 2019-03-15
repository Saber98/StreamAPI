using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessEntities.Lookups;

namespace BusinessServices.Interfaces
{
    public interface IAddressTypeServices
    {
        AddressTypeModel GetById(int id);
        IEnumerable<AddressTypeModel> GetAll(bool includeArchived);
        int Create(AddressTypeModel addressTypeModel);
        bool Update(int id, AddressTypeModel addressTypeModel);
        bool Delete(int id, bool hardDelete);
    }
}
