using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessEntities.Lookups;

namespace BusinessServices.Interfaces
{
    public interface IStateServices
    {
        StateModel GetById(int id);
        IEnumerable<StateModel> GetAll(bool includeArchived);
        int Create(StateModel stateModel);
        bool Update(int id, StateModel stateModel);
        bool Delete(int id, bool hardDelete);
    }
}
