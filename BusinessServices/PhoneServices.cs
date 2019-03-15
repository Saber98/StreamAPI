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
    public class PhoneServices:IPhoneServices
    {
        private readonly DbActions _dbActions;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public PhoneServices(DbActions dbActions)
        {
            _dbActions = dbActions;
        }

        /// <summary>
        /// Fetches phone details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PhoneModel GetById(int id)
        {
            var phone = _dbActions.PhoneNumberRepository.GetById(id);
            if (phone != null)
            {

                PhoneModel phoneEntity = new PhoneModel()
                {
                    Id = phone.Id,
                    Extension = phone.Extension,
                    PhoneTypeId = phone.PhoneTypeId,
                    PhoneType = new PhoneTypeModel()
                    {
                        Id = phone.PhoneType.Id,
                        PhoneType = phone.PhoneType.PhoneType
                    },
                    PhoneNumber = phone.Number,
                    AreaCode = phone.AreaCode,
                    Created = phone.Created,
                    CreatedBy = phone.CreatedBy,
                    LastModified = phone.LastModified,
                    LastModifiedBy = phone.LastModifiedBy,
                    Archived = phone.Archived
                };
                return phoneEntity;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the phone numbers.
        /// </summary>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        public IEnumerable<PhoneModel> GetAll(bool includeArchived)
        {
            var phoneNumbers = includeArchived ? _dbActions.PhoneNumberRepository.GetAll().ToList()
                                               : _dbActions.PhoneNumberRepository.GetMany(p=>p.Archived == false).ToList();
            if (!phoneNumbers.Any()) return null;
            List<PhoneModel> phoneEntities = new List<PhoneModel>();
            foreach (PhoneNumber phone in phoneNumbers)
            {
                PhoneModel phoneEntity = new PhoneModel()
                {
                    Id = phone.Id,
                    Extension = phone.Extension,
                    PhoneTypeId = phone.PhoneTypeId,
                    PhoneType = new PhoneTypeModel()
                    {
                        Id = phone.PhoneType.Id,
                        PhoneType = phone.PhoneType.PhoneType
                    },
                    PhoneNumber = phone.Number,
                    AreaCode = phone.AreaCode,
                    Created = phone.Created,
                    CreatedBy = phone.CreatedBy,
                    LastModified = phone.LastModified,
                    LastModifiedBy = phone.LastModifiedBy,
                    Archived = phone.Archived
                };
                phoneEntities.Add(phoneEntity);
            }

            return phoneEntities;
        }

        /// <summary>
        /// Creates a phone number
        /// </summary>
        /// <param name="phoneModel"></param>
        /// <returns></returns>
        public int Create(PhoneModel phoneModel)
        {
            using (var scope = new TransactionScope())
            {
                var phone = new PhoneNumber()
                {
                    AreaCode = phoneModel.AreaCode,
                    Created = DateTime.Now,
                    CreatedBy = phoneModel.CreatedBy,
                    LastModified = DateTime.Now,
                    LastModifiedBy = phoneModel.LastModifiedBy,
                    Extension = phoneModel.Extension,
                    Number = phoneModel.PhoneNumber,
                    PhoneTypeId = phoneModel.PhoneTypeId,
                    Archived = phoneModel.Archived
                };

                var phoneCheck = _dbActions.PhoneNumberRepository.GetSingle(p => p.AreaCode == phoneModel.AreaCode
                                                                                 && p.Number == phoneModel.PhoneNumber
                                                                                 && p.Extension == phoneModel.Extension);
                if (phoneCheck == null)
                {
                    _dbActions.PhoneNumberRepository.Insert(phone);
                    _dbActions.Save();
                    scope.Complete();
                    return phone.Id;
                }
                scope.Complete();
                return -1;

            }
        }

        /// <summary>
        /// Updates a phone number
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phoneModel"></param>
        /// <returns></returns>
        public bool Update(int id, PhoneModel phoneModel)
        {
            var success = false;
            if (phoneModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var phone = _dbActions.PhoneNumberRepository.GetById(id);
                    if (phone != null)
                    {
                        phone.AreaCode = phoneModel.AreaCode;
                        phone.Extension = phoneModel.Extension;
                        phone.LastModified = DateTime.Now;
                        phone.LastModifiedBy = phoneModel.LastModifiedBy;
                        phone.Number = phoneModel.PhoneNumber;
                        phone.PhoneTypeId = phoneModel.PhoneTypeId;
                        phone.Archived = phoneModel.Archived;
                        _dbActions.PhoneNumberRepository.Update(phone);
                        _dbActions.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular phone number id
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
                    var phone = _dbActions.PhoneNumberRepository.GetById(id);
                    if (phone != null)
                    {
                        if (hardDelete)
                        {
                            _dbActions.PhoneTypeRepository.Delete(phone);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
                        else  // Soft Delete.  Set Archived = true
                        {
                            phone.Archived = true;
                            _dbActions.PhoneNumberRepository.Update(phone);
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
