using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using BusinessEntities;
using BusinessEntities.Lookups;
using BusinessServices.Interfaces;
using BusinessServices.Utilities;
using DataModel.DBActions;
using DataModel.DBObjects;
using DataModel.DBObjects.Lookups;

namespace BusinessServices
{
    public class UserSecurityServices:IUserSecurityServices
    {
        private readonly DbActions _dbActions;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public UserSecurityServices(DbActions dbActions)
        {
            _dbActions = dbActions;
        }

        /// <summary>
        /// Fetches user security details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserSecurityModel GetById(int id)
        {
            var userSecurity = _dbActions.UserSecurityRepository.GetById(id);
            if (userSecurity != null)
            {
                UserSecurityModel userSecurityEntity = new UserSecurityModel()
                {
                    Id = userSecurity.Id,
                    UserId = userSecurity.UserId,
                    User = new UserModel()
                    {
                        Id = userSecurity.User.Id,
                        UserName = userSecurity.User.UserName,
                        EmailAddress = userSecurity.User.EmailAddress,
                        ContactId = userSecurity.User.ContactId,
                        Contact = new ContactModel()
                        {
                            Id = userSecurity.User.Contact.Id,
                            FirstName = userSecurity.User.Contact.FirstName,
                            LastName = userSecurity.User.Contact.LastName,
                            EmailAddress = userSecurity.User.Contact.EmailAddress,
                            Addresses = userSecurity.User.Contact.Addresses.Select(p => new AddressModel()
                            {
                                Id = p.Id,
                                AddressTypeId = p.AddressType.Id,
                                AddressType = new AddressTypeModel()
                                {
                                    Id = p.AddressType.Id,
                                    AddressType = p.AddressType.AddressType,
                                    Archived = p.AddressType.Archived
                                },
                                Address1 = p.Address1,
                                Address2 = p.Address2,
                                City = p.City,
                                StateId = p.StateId,
                                State = new StateModel()
                                {
                                    Id = p.State.Id,
                                    StateAbbreviation = p.State.StateAbbreviation,
                                    StateName = p.State.StateName,
                                    Archived = p.State.Archived
                                },
                                Zip = p.Zip,
                                CreatedBy = p.CreatedBy,
                                Created = p.Created,
                                LastModified = p.LastModified,
                                LastModifiedBy = p.LastModifiedBy
                            }).ToList(),
                            PhoneNumbers = userSecurity.User.Contact.PhoneNumbers.Select(p => new PhoneModel()
                            {
                                Id = p.Id,
                                PhoneTypeId = p.PhoneType.Id,
                                PhoneType = new PhoneTypeModel()
                                {
                                    Id = p.PhoneType.Id,
                                    PhoneType = p.PhoneType.PhoneType,
                                    Archived = p.PhoneType.Archived
                                },
                                AreaCode = p.AreaCode,
                                PhoneNumber = p.Number,
                                Extension = p.Extension,
                                CreatedBy = p.CreatedBy,
                                Created = p.Created,
                                LastModified = p.LastModified,
                                LastModifiedBy = p.LastModifiedBy
                            }).ToList(),
                            Created = userSecurity.User.Contact.Created,
                            CreatedBy = userSecurity.User.Contact.CreatedBy,
                            LastModified = userSecurity.User.Contact.LastModified,
                            LastModifiedBy = userSecurity.User.Contact.LastModifiedBy,
                            Archived = userSecurity.User.Contact.Archived
                        },
                        Roles = userSecurity.User.Roles.Select(p => new RoleModel()
                        {
                            Id = p.Id,
                            Name = p.RoleName,
                            Description = p.Description,
                            CreatedBy = p.CreatedBy,
                            Created = p.Created,
                            LastModified = p.LastModified,
                            LastModifiedBy = p.LastModifiedBy
                        }).ToList(),
                        Created = userSecurity.Created,
                        CreatedBy = userSecurity.CreatedBy,
                        LastModified = userSecurity.LastModified,
                        LastModifiedBy = userSecurity.LastModifiedBy

                    },
                    Password = userSecurity.Password,
                    IpAddress = userSecurity.IpAddress,
                    IsLockedOut = userSecurity.LockedOut,
                    LastLogin = userSecurity.LastLogin,
                    Created = userSecurity.Created,
                    CreatedBy = userSecurity.CreatedBy,
                    LastModified = userSecurity.LastModified,
                    LastModifiedBy = userSecurity.LastModifiedBy
                };
                return userSecurityEntity;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the user Security.
        /// </summary>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        public IEnumerable<UserSecurityModel> GetAll(bool includeArchived)
        {
            var userSecurities = _dbActions.UserSecurityRepository.GetAll().ToList();
            
            if (!userSecurities.Any()) return null;
            List<UserSecurityModel> userSecurityEntities = new List<UserSecurityModel>();
            foreach (UserSecurity userSecurity in userSecurities)
            {
                //var lookupType = _dbActions.LookupTypeRepository.GetById(lookup.LookupTypeId);
                UserSecurityModel userSecurityEntity = new UserSecurityModel()
                {
                    Id = userSecurity.Id,
                    UserId = userSecurity.UserId,
                    User = new UserModel()
                    {
                        Id = userSecurity.User.Id,
                        UserName = userSecurity.User.UserName,
                        EmailAddress = userSecurity.User.EmailAddress,
                        ContactId = userSecurity.User.ContactId,
                        Contact = new ContactModel()
                        {
                            Id = userSecurity.User.Contact.Id,
                            FirstName = userSecurity.User.Contact.FirstName,
                            LastName = userSecurity.User.Contact.LastName,
                            EmailAddress = userSecurity.User.Contact.EmailAddress,
                            Addresses = userSecurity.User.Contact.Addresses.Select(p => new AddressModel()
                            {
                                Id = p.Id,
                                AddressTypeId = p.AddressType.Id,
                                AddressType = new AddressTypeModel()
                                {
                                    Id = p.AddressType.Id,
                                    AddressType = p.AddressType.AddressType,
                                    Archived = p.AddressType.Archived
                                },
                                Address1 = p.Address1,
                                Address2 = p.Address2,
                                City = p.City,
                                StateId = p.StateId,
                                State = new StateModel()
                                {
                                    Id = p.State.Id,
                                    StateAbbreviation = p.State.StateAbbreviation,
                                    StateName = p.State.StateName,
                                    Archived = p.State.Archived
                                },
                                Zip = p.Zip,
                                CreatedBy = p.CreatedBy,
                                Created = p.Created,
                                LastModified = p.LastModified,
                                LastModifiedBy = p.LastModifiedBy
                            }).ToList(),
                            PhoneNumbers = userSecurity.User.Contact.PhoneNumbers.Select(p => new PhoneModel()
                            {
                                Id = p.Id,
                                PhoneTypeId = p.PhoneType.Id,
                                PhoneType = new PhoneTypeModel()
                                {
                                    Id = p.PhoneType.Id,
                                    PhoneType = p.PhoneType.PhoneType,
                                    Archived = p.PhoneType.Archived
                                },
                                AreaCode = p.AreaCode,
                                PhoneNumber = p.Number,
                                Extension = p.Extension,
                                CreatedBy = p.CreatedBy,
                                Created = p.Created,
                                LastModified = p.LastModified,
                                LastModifiedBy = p.LastModifiedBy
                            }).ToList(),
                            Created = userSecurity.User.Contact.Created,
                            CreatedBy = userSecurity.User.Contact.CreatedBy,
                            LastModified = userSecurity.User.Contact.LastModified,
                            LastModifiedBy = userSecurity.User.Contact.LastModifiedBy,
                            Archived = userSecurity.User.Contact.Archived
                        },
                        Roles = userSecurity.User.Roles.Select(p => new RoleModel()
                        {
                            Id = p.Id,
                            Name = p.RoleName,
                            Description = p.Description,
                            CreatedBy = p.CreatedBy,
                            Created = p.Created,
                            LastModified = p.LastModified,
                            LastModifiedBy = p.LastModifiedBy
                        }).ToList(),
                        Created = userSecurity.Created,
                        CreatedBy = userSecurity.CreatedBy,
                        LastModified = userSecurity.LastModified,
                        LastModifiedBy = userSecurity.LastModifiedBy

                    },
                    Password = userSecurity.Password,
                    IpAddress = userSecurity.IpAddress,
                    IsLockedOut = userSecurity.LockedOut,
                    LastLogin = userSecurity.LastLogin,
                    Created = userSecurity.Created,
                    CreatedBy = userSecurity.CreatedBy,
                    LastModified = userSecurity.LastModified,
                    LastModifiedBy = userSecurity.LastModifiedBy
                };
                userSecurityEntities.Add(userSecurityEntity);
            }

            return userSecurityEntities;
        }

        /// <summary>
        /// Creates a user security
        /// </summary>
        /// <param name="userSecurityModel"></param>
        /// <returns></returns>
        public int Create(UserSecurityModel userSecurityModel)
        {
            using (var scope = new TransactionScope())
            {
                var userSecurity = new UserSecurity()
                {
                    UserId = userSecurityModel.Id,
                    IpAddress = userSecurityModel.IpAddress,
                    LockedOut = userSecurityModel.IsLockedOut,
                    Password = userSecurityModel.Password,
                    Created = userSecurityModel.Created,
                    CreatedBy = userSecurityModel.CreatedBy,
                    LastModified = userSecurityModel.LastModified,
                    LastModifiedBy = userSecurityModel.LastModifiedBy,
                };
                var userCheck = _dbActions.UserSecurityRepository.GetSingle(p => p.UserId == userSecurityModel.UserId || p.IpAddress == userSecurityModel.IpAddress);
                if (userCheck == null)
                {
                    _dbActions.UserSecurityRepository.Insert(userSecurity);
                    _dbActions.Save();
                    scope.Complete();
                    return userSecurity.Id;
                }
                scope.Complete();
                return -1;

            }
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userSecurityModel"></param>
        /// <returns></returns>
        public bool Update(int id, UserSecurityModel userSecurityModel)
        {
            var success = false;
            if (userSecurityModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var userSecurity = _dbActions.UserSecurityRepository.GetById(id);
                    if (userSecurity != null)
                    {
                        userSecurity.Id = userSecurityModel.Id;
                        userSecurity.IpAddress = userSecurityModel.IpAddress;
                        userSecurity.UserId = userSecurityModel.UserId;
                        userSecurity.Password = userSecurityModel.Password;
                        userSecurity.LastLogin = userSecurityModel.LastLogin;
                        userSecurity.LockedOut = userSecurityModel.IsLockedOut;
                        userSecurity.Created = userSecurityModel.Created;
                        userSecurity.CreatedBy = userSecurityModel.CreatedBy;
                        userSecurity.LastModified = userSecurityModel.LastModified;
                        userSecurity.LastModifiedBy = userSecurityModel.LastModifiedBy;
                        
                        _dbActions.UserSecurityRepository.Update(userSecurity);
                        _dbActions.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular user security id
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
                    var user = _dbActions.UserSecurityRepository.GetById(id);
                    if (user != null)
                    {
                        if (hardDelete)
                        {
                            _dbActions.UserSecurityRepository.Delete(user);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
                    }
                }
            }
            return success;
        }

        #region Private Methods
        private void UpdateContactPhoneNumbers(ContactModel contactModel, Contact contact)
        {
            var convertedPhoneNumbers = contactModel.PhoneNumbers.Select(p => new PhoneNumber()
            {
                Id = p.Id,
                PhoneTypeId = p.PhoneTypeId,
                PhoneType = new PhoneTypeLookup()
                {
                    Id = p.PhoneType.Id,
                    PhoneType = p.PhoneType.PhoneType,
                    Archived = p.PhoneType.Archived
                },
                AreaCode = p.AreaCode,
                Number = p.PhoneNumber,
                Extension = p.Extension,
                Created = p.Created,
                CreatedBy = p.CreatedBy,
                LastModified = p.LastModified,
                LastModifiedBy = p.LastModifiedBy,
                Archived = p.Archived
            }).ToList();
            foreach (PhoneNumber phone in convertedPhoneNumbers)
            {
                if (phone.Id <= 0) continue;
                if (phone.PhoneTypeId == phone.PhoneType.Id)
                {
                    _dbActions.PhoneNumberRepository.Update(phone);
                }
                else
                {
                    throw new Exception("Phone Type Id mismatch");
                }
            }
            if (convertedPhoneNumbers.Count > 0)
            {
                var deletedPhoneNumbers = contact.PhoneNumbers.Except(convertedPhoneNumbers, add => add.Id).ToList();
                var addedPhoneNumbers = convertedPhoneNumbers.Except(contact.PhoneNumbers, add => add.Id).ToList();

                //Remove deleted phone numbers
                deletedPhoneNumbers.ForEach(p => contact.PhoneNumbers.Remove(p));

                // Add new phone numbers
                foreach (PhoneNumber phone in addedPhoneNumbers)
                {
                    if (phone.Id == 0) // New phone number.  Need to insert before we can attach.
                    {
                        _dbActions.Context.Entry(phone.PhoneType).State = EntityState.Unchanged;
                        _dbActions.PhoneNumberRepository.Insert(phone);
                        _dbActions.Save();
                    }

                    // Attach phone numbers from disconnected object
                    if (_dbActions.Context.Entry(phone).State == EntityState.Detached)
                    {
                        _dbActions.Context.PhoneNumbers.Attach(phone);
                    }

                    contact.PhoneNumbers.Add(phone);
                }
            }
        }

        private void UpdateContactAddresses(ContactModel contactModel, Contact contact)
        {
            var convertedAddresses = contactModel.Addresses.Select(p => new Address()
            {
                Id = p.Id,
                AddressTypeId = p.AddressType.Id,
                AddressType = new AddressTypeLookup()
                {
                    Id = p.AddressType.Id,
                    AddressType = p.AddressType.AddressType,
                    Archived = p.AddressType.Archived
                },
                Address1 = p.Address1,
                Address2 = p.Address2,
                City = p.City,
                StateId = p.State.Id,
                Zip = p.Zip,
                Created = p.Created,
                CreatedBy = p.CreatedBy,
                LastModified = p.LastModified,
                LastModifiedBy = p.LastModifiedBy,
                Archived = p.Archived
            }).ToList();
            foreach (Address address in convertedAddresses)
            {
                if (address.Id > 0)
                {
                    if (address.AddressTypeId == address.AddressType.Id)
                    {
                        _dbActions.AddressRepository.Update(address);
                    }
                    else
                    {
                        throw new Exception("Phone Type Id mismatch");
                    }

                }
            }
            if (convertedAddresses.Count > 0)
            {
                var deletedAddresses = contact.Addresses.Except(convertedAddresses, add => add.Id).ToList();
                var addedAddresses = convertedAddresses.Except(contact.Addresses, add => add.Id).ToList();

                //Remove deleted addresses
                deletedAddresses.ForEach(a => contact.Addresses.Remove(a));

                // Add new Addresses
                foreach (Address a in addedAddresses)
                {
                    if (a.Id == 0) // New address.  Need to insert before we can attach.
                    {
                        _dbActions.Context.Entry(a.AddressType).State = EntityState.Unchanged;
                        _dbActions.AddressRepository.Insert(a);
                        _dbActions.Save();
                    }

                    // Attach addresses from disconnected object
                    if (_dbActions.Context.Entry(a).State == EntityState.Detached)
                    {
                        _dbActions.Context.Addresses.Attach(a);
                    }

                    contact.Addresses.Add(a);
                }
            }
        }
        #endregion

    }
}
