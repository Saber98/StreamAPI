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
    public class UserServices:IUserServices
    {
        private readonly DbActions _dbActions;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public UserServices(DbActions dbActions)
        {
            _dbActions = dbActions;
        }

        /// <summary>
        /// Fetches user details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserModel GetById(int id)
        {
            var user = _dbActions.UserRepository.GetById(id);
            if (user != null)
            {
                UserModel userEntity = new UserModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    EmailAddress = user.EmailAddress,
                    ContactId = user.ContactId,
                    Contact = new ContactModel()
                    {
                        Id = user.Contact.Id,
                        FirstName = user.Contact.FirstName,
                        LastName = user.Contact.LastName,
                        EmailAddress = user.Contact.EmailAddress,
                        Addresses = user.Contact.Addresses.Select(p => new AddressModel()
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
                        PhoneNumbers = user.Contact.PhoneNumbers.Select(p => new PhoneModel()
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
                        Created = user.Contact.Created,
                        CreatedBy = user.Contact.CreatedBy,
                        LastModified = user.Contact.LastModified,
                        LastModifiedBy = user.Contact.LastModifiedBy,
                        Archived = user.Contact.Archived
                    },
                    Roles = user.Roles.Select(p => new RoleModel()
                    {
                        Id = p.Id,
                        Name = p.RoleName,
                        Description = p.Description,
                        CreatedBy = p.CreatedBy,
                        Created = p.Created,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy
                    }).ToList(),
                    Created = user.Created,
                    CreatedBy = user.CreatedBy,
                    LastModified = user.LastModified,
                    LastModifiedBy = user.LastModifiedBy,
                    Archived = user.Archived
                };
                return userEntity;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the users.
        /// </summary>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> GetAll(bool includeArchived)
        {
            var users = includeArchived ? _dbActions.UserRepository.GetAll().ToList() 
                                        : _dbActions.UserRepository.GetMany(p => p.Archived == false).ToList();
            
            if (!users.Any()) return null;
            List<UserModel> userEntities = new List<UserModel>();
            foreach (User user in users)
            {
                //var lookupType = _dbActions.LookupTypeRepository.GetById(lookup.LookupTypeId);
                UserModel userEntity = new UserModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    EmailAddress = user.EmailAddress,
                    ContactId = user.ContactId,
                    Contact = new ContactModel()
                    {
                        Id = user.Contact.Id,
                        FirstName = user.Contact.FirstName,
                        LastName = user.Contact.LastName,
                        EmailAddress = user.Contact.EmailAddress,
                        Addresses = user.Contact.Addresses.Select(p => new AddressModel()
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
                        PhoneNumbers = user.Contact.PhoneNumbers.Select(p => new PhoneModel()
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
                        Created = user.Contact.Created,
                        CreatedBy = user.Contact.CreatedBy,
                        LastModified = user.Contact.LastModified,
                        LastModifiedBy = user.Contact.LastModifiedBy,
                        Archived = user.Contact.Archived
                    },
                    Roles = user.Roles.Select(p => new RoleModel()
                    {
                        Id = p.Id,
                        Name = p.RoleName,
                        Description = p.Description,
                        CreatedBy = p.CreatedBy,
                        Created = p.Created,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy
                    }).ToList(),
                    Created = user.Created,
                    CreatedBy = user.CreatedBy,
                    LastModified = user.LastModified,
                    LastModifiedBy = user.LastModifiedBy,
                    Archived = user.Archived
                };
                userEntities.Add(userEntity);
            }

            return userEntities;
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public int Create(UserModel userModel)
        {
            using (var scope = new TransactionScope())
            {
                var user = new User()
                {
                    UserName = userModel.UserName,
                    EmailAddress = userModel.EmailAddress,
                    ContactId = userModel.ContactId,
                    Roles = userModel.Roles.Select(p => new Role()
                    {
                        Id = p.Id,
                        RoleName = p.Name,
                        Description = p.Description,
                        CreatedBy = p.CreatedBy,
                        Created = p.Created,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy,
                        Archived = p.Archived
                        
                    }).ToList(),
                    Created = userModel.Created,
                    CreatedBy = userModel.CreatedBy,
                    LastModified = userModel.LastModified,
                    LastModifiedBy = userModel.LastModifiedBy,
                    Archived = userModel.Archived
                };
                var userCheck = _dbActions.UserRepository.GetSingle(p => p.UserName == userModel.UserName || p.EmailAddress == userModel.EmailAddress);
                if (userCheck == null)
                {
                    _dbActions.UserRepository.Insert(user);
                    _dbActions.Save();
                    scope.Complete();
                    return user.Id;
                }
                scope.Complete();
                return -1;

            }
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public bool Update(int id, UserModel userModel)
        {
            var success = false;
            if (userModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var user = _dbActions.UserRepository.GetById(id);
                    if (user != null)
                    {
                        user.Id = userModel.Id;
                        user.UserName = userModel.UserName;
                        user.EmailAddress = userModel.EmailAddress;
                        user.ContactId = userModel.ContactId;
                        UpdateContactAddresses(userModel.Contact, user.Contact);
                        
                        UpdateContactPhoneNumbers(userModel.Contact, user.Contact);

                        UpdateUserRoles(userModel, user);
                        user.Created = userModel.Created;
                        user.CreatedBy = userModel.CreatedBy;
                        user.LastModified = userModel.LastModified;
                        user.LastModifiedBy = userModel.LastModifiedBy;
                        user.Archived = userModel.Archived;
                        _dbActions.UserRepository.Update(user);
                        _dbActions.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular user id
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
                    var user = _dbActions.UserRepository.GetById(id);
                    if (user != null)
                    {
                        if (hardDelete)
                        {
                            _dbActions.UserRepository.Delete(user);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
                        else  // Soft Delete.  Set Archived = true
                        {
                            user.Archived = true;
                            _dbActions.UserRepository.Update(user);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }

                    }
                }
            }
            return success;
        }

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

        private void UpdateUserRoles(UserModel userModel, User user)
        {
            var convertedRoles = userModel.Roles.Select(p => new Role()
            {
                Id = p.Id,
                RoleName = p.Name,
                Description = p.Description,
                Created = p.Created,
                CreatedBy = p.CreatedBy,
                LastModified = p.LastModified,
                LastModifiedBy = p.LastModifiedBy,
                Archived = p.Archived
            }).ToList();
            
            if (convertedRoles.Count > 0)
            {
                var deletedRoles = user.Roles.Except(convertedRoles, add => add.Id).ToList();
                var addedRoles = convertedRoles.Except(user.Roles, add => add.Id).ToList();

                //Remove deleted roles
                deletedRoles.ForEach(a => user.Roles.Remove(a));

                // Add new Roles
                foreach (Role r in addedRoles)
                {
                    if (r.Id == 0) // New Role.  Need to insert before we can attach.
                    {
                        _dbActions.Context.Entry(r).State = EntityState.Unchanged;
                        _dbActions.UserRoleRepository.Insert(r);
                        _dbActions.Save();
                    }

                    // Attach Role from disconnected object
                    if (_dbActions.Context.Entry(r).State == EntityState.Detached)
                    {
                        _dbActions.Context.Roles.Attach(r);
                    }

                    user.Roles.Add(r);
                }
            }
        }
    }
}
