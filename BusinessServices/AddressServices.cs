using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BusinessEntities;
using BusinessEntities.Lookups;
using BusinessServices.Interfaces;
using DataModel.DBActions;
using DataModel.DBObjects;

namespace BusinessServices
{
    public class AddressServices:IAddressServices
    {
        private readonly DbActions _dbActions;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public AddressServices(DbActions dbActions)
        {
            _dbActions = dbActions;
        }

        /// <summary>
        /// Fetches lookup details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AddressModel GetById(int id)
        {
            var address = _dbActions.AddressRepository.GetById(id);
            if (address != null)
            {
                AddressModel addressEntity = new AddressModel()
                {
                    Id = address.Id,
                    Address1 = address.Address1,
                    Address2 = address.Address2,
                    City = address.City,
                    State = new StateModel()
                    {
                        Id = address.State.Id,
                        StateName = address.State.StateName,
                        StateAbbreviation = address.State.StateAbbreviation,
                        Archived = address.State.Archived
                    },
                    Zip = address.Zip,
                    AddressTypeId = address.AddressType.Id,
                    AddressType = new AddressTypeModel()
                    {
                        Id = address.AddressType.Id,
                        AddressType = address.AddressType.AddressType
                    },
                    Created = address.Created,
                    CreatedBy = address.CreatedBy,
                    LastModified = address.LastModified,
                    LastModifiedBy = address.LastModifiedBy,
                    Archived = address.Archived
                };
                return addressEntity;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the lookups.
        /// </summary>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        public IEnumerable<AddressModel> GetAll(bool includeArchived)
        {
            var addresses = includeArchived ? _dbActions.AddressRepository.GetAll().ToList() 
                                        : _dbActions.AddressRepository.GetMany(p => p.Archived == false).ToList();
            
            if (!addresses.Any()) return null;
            List<AddressModel> addressEntities = new List<AddressModel>();
            foreach (Address address in addresses)
            {
                //var lookupType = _dbActions.LookupTypeRepository.GetById(lookup.LookupTypeId);
                AddressModel addressEntity = new AddressModel()
                {
                    Id = address.Id,
                    Address1 = address.Address1,
                    Address2 = address.Address2,
                    City = address.City,
                    State = new StateModel()
                    {
                        Id = address.State.Id,
                        StateName = address.State.StateName,
                        StateAbbreviation = address.State.StateAbbreviation,
                        Archived = address.State.Archived
                    },
                    Zip = address.Zip,
                    AddressTypeId = address.AddressType.Id,
                    AddressType = new AddressTypeModel()
                    {
                        Id = address.AddressType.Id,
                        AddressType = address.AddressType.AddressType
                    },
                    Created = address.Created,
                    CreatedBy = address.CreatedBy,
                    LastModified = address.LastModified,
                    LastModifiedBy = address.LastModifiedBy,
                    Archived = address.Archived
                };
                addressEntities.Add(addressEntity);
            }

            return addressEntities;
        }

        /// <summary>
        /// Creates a lookup type
        /// </summary>
        /// <param name="addressModel"></param>
        /// <returns></returns>
        public int Create(AddressModel addressModel)
        {
            using (var scope = new TransactionScope())
            {
                var address = new Address()
                {
                    Address1 = addressModel.Address1,
                    Address2 = addressModel.Address2,
                    City = addressModel.City,
                    StateId = addressModel.State.Id,
                    Zip = addressModel.Zip,
                    AddressTypeId = addressModel.AddressType.Id,
                    Created = addressModel.Created,
                    CreatedBy = addressModel.CreatedBy,
                    LastModified = addressModel.LastModified,
                    LastModifiedBy = addressModel.LastModifiedBy,
                    Archived = addressModel.Archived
                };
                var addressCheck = _dbActions.AddressRepository.GetSingle(p => p.Address1 == addressModel.Address1
                                                                               && p.Address2 == addressModel.Address2
                                                                               && p.City == addressModel.City
                                                                               && p.StateId == addressModel.StateId
                                                                               && p.Zip == addressModel.Zip);
                if (addressCheck == null)
                {
                    _dbActions.AddressRepository.Insert(address);
                    _dbActions.Save();
                    scope.Complete();
                    return address.Id;
                }
                scope.Complete();
                return -1;

            }
        }

        /// <summary>
        /// Updates a lookup type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressModel"></param>
        /// <returns></returns>
        public bool Update(int id, AddressModel addressModel)
        {
            var success = false;
            if (addressModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var address = _dbActions.AddressRepository.GetById(id);
                    if (address != null)
                    {
                        address.Address1 = addressModel.Address1;
                        address.Address2 = addressModel.Address2;
                        address.City = addressModel.City;
                        address.StateId = addressModel.State.Id;
                        address.Zip = addressModel.Zip;
                        address.AddressTypeId = addressModel.AddressType.Id;
                        address.Created = addressModel.Created;
                        address.CreatedBy = addressModel.CreatedBy;
                        address.LastModified = addressModel.LastModified;
                        address.LastModifiedBy = addressModel.LastModifiedBy;
                        address.Archived = addressModel.Archived;
                        _dbActions.AddressRepository.Update(address);
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
                    var address = _dbActions.AddressRepository.GetById(id);
                    if (address != null)
                    {
                        if (hardDelete)
                        {
                            _dbActions.AddressRepository.Delete(address);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
                        else  // Soft Delete.  Set Archived = true
                        {
                            address.Archived = true;
                            _dbActions.AddressRepository.Update(address);
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
