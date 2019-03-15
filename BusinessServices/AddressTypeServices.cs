using System;
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
    public class AddressTypeServices:IAddressTypeServices
    {
        private readonly DbActions _dbActions;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public AddressTypeServices(DbActions dbActions)
        {
            _dbActions = dbActions;
        }

        /// <summary>
        /// Fetches address type details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AddressTypeModel GetById(int id)
        {
            var addressType = _dbActions.AddressTypeRepository.GetById(id);
            if (addressType != null)
            {
              
                AddressTypeModel addressTypeEntity = new AddressTypeModel()
                {
                    Id = addressType.Id,
                    AddressType = addressType.AddressType,
                    Archived = addressType.Archived
                };
                return addressTypeEntity;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the address types.
        /// </summary>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        public IEnumerable<AddressTypeModel> GetAll(bool includeArchived)
        {
            var addressTypes = includeArchived ? _dbActions.AddressTypeRepository.GetAll().ToList()
                                               : _dbActions.AddressTypeRepository.GetMany(p=>p.Archived == false).ToList();
            if (!addressTypes.Any()) return null;
            List<AddressTypeModel> addressTypeEntities = new List<AddressTypeModel>();
            foreach (AddressTypeLookup addressType in addressTypes)
            {
                AddressTypeModel addressTypeEntity = new AddressTypeModel()
                {
                    Id = addressType.Id,
                    AddressType = addressType.AddressType,
                    Archived = addressType.Archived
                };
                addressTypeEntities.Add(addressTypeEntity);
            }

            return addressTypeEntities;
        }

        /// <summary>
        /// Creates a address type
        /// </summary>
        /// <param name="addressTypeModel"></param>
        /// <returns></returns>
        public int Create(AddressTypeModel addressTypeModel)
        {
            using (var scope = new TransactionScope())
            {
                var addressType = new AddressTypeLookup()
                {
                    AddressType = addressTypeModel.AddressType,
                    Archived = addressTypeModel.Archived
                };
                var addressTypeCheck = _dbActions.AddressTypeRepository.GetSingle(p=>p.AddressType == addressTypeModel.AddressType);
                if (addressTypeCheck == null)
                {
                    _dbActions.AddressTypeRepository.Insert(addressType);
                    _dbActions.Save();
                    scope.Complete();
                    return addressType.Id;
                }
                scope.Complete();
                return -1;
            }
        }

        /// <summary>
        /// Updates a lookup type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressTypeModel"></param>
        /// <returns></returns>
        public bool Update(int id, AddressTypeModel addressTypeModel)
        {
            var success = false;
            if (addressTypeModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var addressType = _dbActions.AddressTypeRepository.GetById(id);
                    if (addressType != null)
                    {
                        addressType.AddressType = addressTypeModel.AddressType;
                        addressType.Archived = addressTypeModel.Archived;
                        _dbActions.AddressTypeRepository.Update(addressType);
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
                    var addressType = _dbActions.AddressTypeRepository.GetById(id);
                    if (addressType != null)
                    {
                        if (hardDelete)
                        {
                            _dbActions.AddressTypeRepository.Delete(addressType);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
                        else  // Soft Delete.  Set Archived = true
                        {
                            addressType.Archived = true;
                            _dbActions.AddressTypeRepository.Update(addressType);
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
