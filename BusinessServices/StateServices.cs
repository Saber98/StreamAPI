using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BusinessEntities.Lookups;
using BusinessServices.Interfaces;
using DataModel.DBActions;
using DataModel.DBObjects.Lookups;

namespace BusinessServices
{
    public class StateServices:IStateServices
    {
        private readonly DbActions _dbActions;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public StateServices(DbActions dbActions)
        {
            _dbActions = dbActions;
        }

        /// <summary>
        /// Fetches state details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StateModel GetById(int id)
        {
            var state = _dbActions.StateRepository.GetById(id);
            if (state != null)
            {
              
                StateModel stateEntity = new StateModel()
                {
                    Id = state.Id,
                    StateName = state.StateName,
                    StateAbbreviation = state.StateAbbreviation,
                    Archived = state.Archived
                };
                return stateEntity;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the states.
        /// </summary>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        public IEnumerable<StateModel> GetAll(bool includeArchived)
        {
            var states = includeArchived ? _dbActions.StateRepository.GetAll().ToList()
                                         : _dbActions.StateRepository.GetMany(p=>p.Archived == false).ToList();
            if (!states.Any()) return null;
            List<StateModel> stateEntities = new List<StateModel>();
            foreach (StateLookup state in states)
            {
                StateModel stateEntity = new StateModel()
                {
                    Id = state.Id,
                    StateName = state.StateName
                };
                stateEntities.Add(stateEntity);
            }

            return stateEntities;
        }

        /// <summary>
        /// Creates a state
        /// </summary>
        /// <param name="stateModel"></param>
        /// <returns></returns>
        public int Create(StateModel stateModel)
        {
            using (var scope = new TransactionScope())
            {
                var state = new StateLookup()
                {
                    StateName = stateModel.StateName,
                    StateAbbreviation = stateModel.StateAbbreviation,
                    Archived = stateModel.Archived
                };
                _dbActions.StateRepository.Insert(state);
                _dbActions.Save();
                scope.Complete();
                return state.Id;
            }
        }

        /// <summary>
        /// Updates a state
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stateModel"></param>
        /// <returns></returns>
        public bool Update(int id, StateModel stateModel)
        {
            var success = false;
            if (stateModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var state = _dbActions.StateRepository.GetById(id);
                    if (state != null)
                    {
                        state.StateName = stateModel.StateName;
                        state.StateAbbreviation = stateModel.StateAbbreviation;
                        state.Archived = stateModel.Archived;
                        _dbActions.StateRepository.Update(state);
                        _dbActions.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular type id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hardDelete"></param>
        /// <returns></returns>
        public bool Delete(int id, bool hardDelete)
        {
            var success = false;
            if (id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var state = _dbActions.StateRepository.GetById(id);
                    if (state != null)
                    {
                        if (hardDelete)
                        {
                            _dbActions.StateRepository.Delete(state);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
                        else  // Soft Delete.  Set Archived = true
                        {
                            state.Archived = true;
                            _dbActions.StateRepository.Update(state);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
                        
                    }
                }
            }
            return success;
        }
    }
}
