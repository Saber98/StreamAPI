using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices.Interfaces
{
    public interface IAddressServices
    {
        AddressModel GetById(int id);
        IEnumerable<AddressModel> GetAll(bool includeArchived);
        int Create(AddressModel addressModel);
        bool Update(int id, AddressModel addressModel);
        bool Delete(int id, bool hardDelete);
    }
}
