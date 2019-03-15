using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices.Interfaces
{
    public interface IContactServices
    {
        ContactModel GetById(int id);
        IEnumerable<ContactModel> GetAll(bool includeArchived);
        int Create(ContactModel contactModel);
        bool Update(int id, ContactModel contactModel);
        bool Delete(int id, bool hardDelete);
    }
}
