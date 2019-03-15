using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BusinessEntities;
using BusinessEntities.Lookups;
using BusinessServices.Interfaces;
using DataModel.DBActions;
using DataModel.DBObjects;
using DataModel.DBObjects.Lookups;

namespace BusinessServices
{
    public class PhoneTypeServices:IPhoneTypeServices
    {
        private readonly DbActions _dbActions;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public PhoneTypeServices(DbActions dbActions)
        {
            _dbActions = dbActions;
        }

        /// <summary>
        /// Fetches phone type details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PhoneTypeModel GetById(int id)
        {
            var phoneType = _dbActions.PhoneTypeRepository.GetById(id);
            if (phoneType != null)
            {
              
                PhoneTypeModel phoneTypeEntity = new PhoneTypeModel()
                {
                    Id = phoneType.Id,
                    PhoneType = phoneType.PhoneType,
                    Archived = phoneType.Archived
                };
                return phoneTypeEntity;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the phone types.
        /// </summary>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        public IEnumerable<PhoneTypeModel> GetAll(bool includeArchived)
        {
            var phoneTypes = includeArchived ? _dbActions.PhoneTypeRepository.GetAll().ToList()
                                             : _dbActions.PhoneTypeRepository.GetMany(p=>p.Archived == false).ToList();
            if (!phoneTypes.Any()) return null;
            List<PhoneTypeModel> phoneTypeEntities = new List<PhoneTypeModel>();
            foreach (PhoneTypeLookup phoneType in phoneTypes)
            {
                PhoneTypeModel phoneTypeEntity = new PhoneTypeModel()
                {
                    Id = phoneType.Id,
                    PhoneType = phoneType.PhoneType,
                    Archived = phoneType.Archived
                };
                phoneTypeEntities.Add(phoneTypeEntity);
            }

            return phoneTypeEntities;
        }

        /// <summary>
        /// Creates a address type
        /// </summary>
        /// <param name="phoneTypeModel"></param>
        /// <returns></returns>
        public int Create(PhoneTypeModel phoneTypeModel)
        {
            using (var scope = new TransactionScope())
            {
                var phoneType = new PhoneTypeLookup()
                {
                    PhoneType = phoneTypeModel.PhoneType,
                    Archived = phoneTypeModel.Archived
                };

                var phoneTypeCheck = _dbActions.PhoneTypeRepository.GetSingle(p => p.PhoneType == phoneTypeModel.PhoneType);
                if (phoneTypeCheck == null)
                {
                    _dbActions.PhoneTypeRepository.Insert(phoneType);
                    _dbActions.Save();
                    scope.Complete();
                    return phoneType.Id;
                }
                scope.Complete();
                return -1;

            }
        }

        /// <summary>
        /// Updates a lookup type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phoneTypeModel"></param>
        /// <returns></returns>
        public bool Update(int id, PhoneTypeModel phoneTypeModel)
        {
            var success = false;
            if (phoneTypeModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var phoneType = _dbActions.PhoneTypeRepository.GetById(id);
                    if (phoneType != null)
                    {
                        phoneType.PhoneType = phoneTypeModel.PhoneType;
                        phoneType.Archived = phoneTypeModel.Archived;
                        _dbActions.PhoneTypeRepository.Update(phoneType);
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
                    var phoneType = _dbActions.PhoneTypeRepository.GetById(id);
                    if (phoneType != null)
                    {
                        if (hardDelete)
                        {
                            _dbActions.PhoneTypeRepository.Delete(phoneType);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
                        else  // Soft Delete.  Set Archived = true
                        {
                            phoneType.Archived = true;
                            _dbActions.PhoneTypeRepository.Update(phoneType);
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
