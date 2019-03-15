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
    public class ContactServices:IContactServices
    {
        private readonly DbActions _dbActions;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ContactServices(DbActions dbActions)
        {
            _dbActions = dbActions;
        }

        /// <summary>
        /// Fetches contact details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactModel GetById(int id)
        {
            var contact = _dbActions.ContactRepository.GetById(id);
            if (contact != null)
            {
                ContactModel contactEntity = new ContactModel()
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    EmailAddress = contact.EmailAddress,
                    Addresses = contact.Addresses.Select(p => new AddressModel()
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
                    PhoneNumbers = contact.PhoneNumbers.Select(p => new PhoneModel()
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
                    Created = contact.Created,
                    CreatedBy = contact.CreatedBy,
                    LastModified = contact.LastModified,
                    LastModifiedBy = contact.LastModifiedBy,
                    Archived = contact.Archived
                };
                return contactEntity;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the contactss.
        /// </summary>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        public IEnumerable<ContactModel> GetAll(bool includeArchived)
        {
            var contacts = includeArchived ? _dbActions.ContactRepository.GetAll().ToList()
                                           : _dbActions.ContactRepository.GetMany(p=>p.Archived == false).ToList();
            if (!contacts.Any()) return null;
            List<ContactModel> contactEntities = new List<ContactModel>();
            foreach (Contact contact in contacts)
            {
                //var lookupType = _dbActions.LookupTypeRepository.GetById(lookup.LookupTypeId);
                ContactModel contactEntity = new ContactModel()
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    EmailAddress = contact.EmailAddress,
                    Addresses = contact.Addresses.Select(p=> new AddressModel()
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
                    PhoneNumbers = contact.PhoneNumbers.Select(p=> new PhoneModel()
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
                    Created = contact.Created,
                    CreatedBy = contact.CreatedBy,
                    LastModified = contact.LastModified,
                    LastModifiedBy = contact.LastModifiedBy,
                    Archived = contact.Archived
                };
                contactEntities.Add(contactEntity);
            }

            return contactEntities;
        }

        /// <summary>
        /// Creates a contact
        /// </summary>
        /// <param name="contactModel"></param>
        /// <returns></returns>
        public int Create(ContactModel contactModel)
        {
            using (var scope = new TransactionScope())
            {
                var contact = new Contact()
                {
                    FirstName = contactModel.FirstName,
                    LastName = contactModel.LastName,
                    EmailAddress = contactModel.EmailAddress,
                    Addresses = contactModel.Addresses?.Select(p=> new Address()
                    {
                        Id = p.Id,
                        AddressTypeId = p.AddressType.Id,
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
                    }).ToList(),
                    PhoneNumbers = contactModel.PhoneNumbers?.Select(p=> new PhoneNumber()
                    {
                        Id = p.Id,
                        AreaCode = p.AreaCode,
                        PhoneTypeId = p.PhoneTypeId,
                        Number = p.PhoneNumber,
                        Extension = p.Extension,
                        Created = p.Created,
                        CreatedBy = p.CreatedBy,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy
                    }).ToList(),
                    Created = DateTime.Now,
                    CreatedBy = contactModel.CreatedBy,
                    LastModified = DateTime.Now,
                    LastModifiedBy = contactModel.LastModifiedBy,
                    Archived = contactModel.Archived
                };
                if (contact.Addresses != null)
                {
                    foreach (Address addr in contact.Addresses)
                    {
                        _dbActions.Context.Entry(addr).State = addr.Id == 0 ? EntityState.Added : EntityState.Unchanged;
                    }
                }
                

                if (contact.PhoneNumbers != null)
                {
                    foreach (PhoneNumber phone in contact.PhoneNumbers)
                    {
                        _dbActions.Context.Entry(phone).State = phone.Id == 0 ? EntityState.Added : EntityState.Unchanged;
                    }
                }

                var contactCheck = _dbActions.ContactRepository.GetSingle(p => p.EmailAddress == contact.EmailAddress);
                if (contactCheck == null)
                {
                    _dbActions.ContactRepository.Insert(contact);
                    _dbActions.Save();
                    scope.Complete();
                    return contact.Id;
                }
                scope.Complete();
                return -1;

            }
        }

        /// <summary>
        /// Updates a contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contactModel"></param>
        /// <returns></returns>
        public bool Update(int id, ContactModel contactModel)
        {
            var success = false;
            if (contactModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var contact = _dbActions.ContactRepository.GetById(id);  //Attached Entity
                    if (contact != null)
                    {
                        contact.FirstName = contactModel.FirstName;
                        contact.LastName = contactModel.LastName;
                        contact.EmailAddress = contactModel.EmailAddress;
                        contact.LastModified = DateTime.Now;
                        contact.LastModifiedBy = contactModel.LastModifiedBy;
                        contact.Archived = contactModel.Archived;
                        //Attach / Update contact's address listing.
                        UpdateContactAddresses(contactModel, contact);

                        //Attach / Update contact's phone number listing.
                        UpdateContactPhoneNumbers(contactModel, contact);

                        _dbActions.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular contact id
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
                    var contact = _dbActions.ContactRepository.GetById(id);
                    if (contact != null)
                    {
                        if (hardDelete)
                        {
                            _dbActions.ContactRepository.Delete(contact);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
                        else  // Soft Delete.  Set Archived = true
                        {
                            contact.Archived = true;
                            _dbActions.ContactRepository.Update(contact);
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
