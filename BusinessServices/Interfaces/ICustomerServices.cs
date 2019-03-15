using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices.Interfaces
{
    public interface ICustomerServices
    {
        CustomerModel GetById(int id);
        IEnumerable<CustomerModel> GetAll(bool includeArchived);
        int Create(CustomerModel customerModel);
        bool Update(int id, CustomerModel customerModel);
        bool Delete(int id, bool hardDelete);
    }
}
