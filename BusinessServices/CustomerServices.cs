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
    public class CustomerServices:ICustomerServices
    {
        private readonly DbActions _dbActions;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public CustomerServices(DbActions dbActions)
        {
            _dbActions = dbActions;
        }

        /// <summary>
        /// Fetches Customer details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CustomerModel GetById(int id)
        {
            var customer = _dbActions.CustomerRepository.GetById(id);
            if (customer != null)
            {
                CustomerModel customerEntity = new CustomerModel()
                {
                    Id = customer.Id,
                    AccountNumber = customer.AccountNumber,
                    AccountRepId = customer.AccountRep.Id,
                    AccountRep = new ContactModel()
                    {
                        Id = customer.AccountRep.Id,
                        EmailAddress = customer.AccountRep.EmailAddress,
                        FirstName = customer.AccountRep.FirstName,
                        LastName = customer.AccountRep.LastName,
                        Addresses = customer.Addresses?.Select(p => new AddressModel()
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
                            Created = p.Created,
                            CreatedBy = p.CreatedBy,
                            LastModified = p.LastModified,
                            LastModifiedBy = p.LastModifiedBy,
                            Archived = p.Archived
                        }).ToList(),
                        PhoneNumbers = customer.PhoneNumbers?.Select(p => new PhoneModel()
                        {
                            Id = p.Id,
                            AreaCode = p.AreaCode,
                            PhoneTypeId = p.PhoneTypeId,
                            PhoneType = new PhoneTypeModel()
                            {
                                Id = p.PhoneType.Id,
                                PhoneType = p.PhoneType.PhoneType,
                                Archived = p.PhoneType.Archived
                            },
                            PhoneNumber = p.Number,
                            Extension = p.Extension,
                            Created = p.Created,
                            CreatedBy = p.CreatedBy,
                            LastModified = p.LastModified,
                            LastModifiedBy = p.LastModifiedBy
                        }).ToList(),
                        Created = customer.AccountRep.Created,
                        CreatedBy = customer.AccountRep.CreatedBy,
                        LastModified = customer.AccountRep.LastModified,
                        LastModifiedBy = customer.AccountRep.LastModifiedBy
                    },
                    IsParent = customer.IsParentInd,
                    ParentId = customer.ParentId,
                    Name = customer.Name,
                    ShipToBill = customer.ShipToBillInd,
                    WebsiteUrl = customer.WebsiteUrl,
                    Addresses = customer.Addresses?.Select(p => new AddressModel()
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
                        Created = p.Created,
                        CreatedBy = p.CreatedBy,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy,
                        Archived = p.Archived
                    }).ToList(),
                    PhoneNumbers = customer.PhoneNumbers?.Select(p => new PhoneModel()
                    {
                        Id = p.Id,
                        AreaCode = p.AreaCode,
                        PhoneTypeId = p.PhoneTypeId,
                        PhoneType = new PhoneTypeModel()
                        {
                            Id = p.PhoneType.Id,
                            PhoneType = p.PhoneType.PhoneType,
                            Archived = p.PhoneType.Archived
                        },
                        PhoneNumber = p.Number,
                        Extension = p.Extension,
                        Created = p.Created,
                        CreatedBy = p.CreatedBy,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy
                    }).ToList(),
                    Contacts = customer.Contacts.Select(p => new ContactModel()
                    {
                        Id = p.Id,
                        EmailAddress = p.EmailAddress,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Addresses = p.Addresses.Select(a => new AddressModel()
                        {
                            Id = a.Id,
                            AddressTypeId = a.AddressType.Id,
                            AddressType = new AddressTypeModel()
                            {
                                Id = a.AddressType.Id,
                                AddressType = a.AddressType.AddressType,
                                Archived = a.AddressType.Archived
                            },
                            Address1 = a.Address1,
                            Address2 = a.Address2,
                            City = a.City,
                            StateId = a.StateId,
                            State = new StateModel()
                            {
                                Id = a.State.Id,
                                StateAbbreviation = a.State.StateAbbreviation,
                                StateName = a.State.StateName,
                                Archived = a.State.Archived
                            },
                            Zip = a.Zip,
                            Created = a.Created,
                            CreatedBy = a.CreatedBy,
                            LastModified = a.LastModified,
                            LastModifiedBy = a.LastModifiedBy,
                            Archived = a.Archived
                        }).ToList(),
                        PhoneNumbers = p.PhoneNumbers.Select(ph => new PhoneModel()
                        {
                            Id = ph.Id,
                            AreaCode = ph.AreaCode,
                            PhoneTypeId = ph.PhoneTypeId,
                            PhoneType = new PhoneTypeModel()
                            {
                                Id = ph.PhoneType.Id,
                                PhoneType = ph.PhoneType.PhoneType,
                                Archived = ph.PhoneType.Archived
                            },
                            PhoneNumber = ph.Number,
                            Extension = ph.Extension,
                            Created = ph.Created,
                            CreatedBy = ph.CreatedBy,
                            LastModified = ph.LastModified,
                            LastModifiedBy = ph.LastModifiedBy
                        }).ToList(),
                        Created = p.Created,
                        CreatedBy = p.CreatedBy,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy
                    }).ToList(),
                    Created = DateTime.Now,
                    CreatedBy = customer.CreatedBy,
                    LastModified = DateTime.Now,
                    LastModifiedBy = customer.LastModifiedBy,
                    Archived = customer.Archived
                };
                return customerEntity;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the Customerss.
        /// </summary>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        public IEnumerable<CustomerModel> GetAll(bool includeArchived)
        {
            var customers = includeArchived ? _dbActions.CustomerRepository.GetAll().ToList()
                                            : _dbActions.CustomerRepository.GetMany(p=>p.Archived == false).ToList();
            if (!customers.Any()) return null;
            List<CustomerModel> customerEntities = new List<CustomerModel>();
            foreach (Customer customer in customers)
            {
                //var lookupType = _dbActions.LookupTypeRepository.GetById(lookup.LookupTypeId);
                CustomerModel customerEntity = new CustomerModel()
                {
                    Id = customer.Id,
                    AccountNumber = customer.AccountNumber,
                    AccountRepId = customer.AccountRep.Id,
                    AccountRep = new ContactModel()
                    {
                        Id = customer.AccountRep.Id,
                        EmailAddress = customer.AccountRep.EmailAddress,
                        FirstName = customer.AccountRep.FirstName,
                        LastName = customer.AccountRep.LastName,
                        Addresses = customer.Addresses?.Select(p => new AddressModel()
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
                            Created = p.Created,
                            CreatedBy = p.CreatedBy,
                            LastModified = p.LastModified,
                            LastModifiedBy = p.LastModifiedBy,
                            Archived = p.Archived
                        }).ToList(),
                        PhoneNumbers = customer.PhoneNumbers?.Select(p => new PhoneModel()
                        {
                            Id = p.Id,
                            AreaCode = p.AreaCode,
                            PhoneTypeId = p.PhoneTypeId,
                            PhoneType = new PhoneTypeModel()
                            {
                                Id = p.PhoneType.Id,
                                PhoneType = p.PhoneType.PhoneType,
                                Archived = p.PhoneType.Archived
                            },
                            PhoneNumber = p.Number,
                            Extension = p.Extension,
                            Created = p.Created,
                            CreatedBy = p.CreatedBy,
                            LastModified = p.LastModified,
                            LastModifiedBy = p.LastModifiedBy
                        }).ToList(),
                        Created = customer.AccountRep.Created,
                        CreatedBy = customer.AccountRep.CreatedBy,
                        LastModified = customer.AccountRep.LastModified,
                        LastModifiedBy = customer.AccountRep.LastModifiedBy
                    },
                    IsParent = customer.IsParentInd,
                    ParentId = customer.ParentId,
                    Name = customer.Name,
                    ShipToBill = customer.ShipToBillInd,
                    WebsiteUrl = customer.WebsiteUrl,
                    Addresses = customer.Addresses?.Select(p => new AddressModel()
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
                        Created = p.Created,
                        CreatedBy = p.CreatedBy,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy,
                        Archived = p.Archived
                    }).ToList(),
                    PhoneNumbers = customer.PhoneNumbers?.Select(p => new PhoneModel()
                    {
                        Id = p.Id,
                        AreaCode = p.AreaCode,
                        PhoneTypeId = p.PhoneTypeId,
                        PhoneType = new PhoneTypeModel()
                        {
                            Id = p.PhoneType.Id,
                            PhoneType = p.PhoneType.PhoneType,
                            Archived = p.PhoneType.Archived
                        },
                        PhoneNumber = p.Number,
                        Extension = p.Extension,
                        Created = p.Created,
                        CreatedBy = p.CreatedBy,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy
                    }).ToList(),
                    Contacts = customer.Contacts.Select(p => new ContactModel()
                    {
                        Id = p.Id,
                        EmailAddress = p.EmailAddress,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Addresses = p.Addresses.Select(a => new AddressModel()
                        {
                            Id = a.Id,
                            AddressTypeId = a.AddressType.Id,
                            AddressType = new AddressTypeModel()
                            {
                                Id = a.AddressType.Id,
                                AddressType = a.AddressType.AddressType,
                                Archived = a.AddressType.Archived
                            },
                            Address1 = a.Address1,
                            Address2 = a.Address2,
                            City = a.City,
                            StateId = a.StateId,
                            State = new StateModel()
                            {
                                Id = a.State.Id,
                                StateAbbreviation = a.State.StateAbbreviation,
                                StateName = a.State.StateName,
                                Archived = a.State.Archived
                            },
                            Zip = a.Zip,
                            Created = a.Created,
                            CreatedBy = a.CreatedBy,
                            LastModified = a.LastModified,
                            LastModifiedBy = a.LastModifiedBy,
                            Archived = a.Archived
                        }).ToList(),
                        PhoneNumbers = p.PhoneNumbers.Select(ph => new PhoneModel()
                        {
                            Id = ph.Id,
                            AreaCode = ph.AreaCode,
                            PhoneTypeId = ph.PhoneTypeId,
                            PhoneType = new PhoneTypeModel()
                            {
                                Id = ph.PhoneType.Id,
                                PhoneType = ph.PhoneType.PhoneType,
                                Archived = ph.PhoneType.Archived
                            },
                            PhoneNumber = ph.Number,
                            Extension = ph.Extension,
                            Created = ph.Created,
                            CreatedBy = ph.CreatedBy,
                            LastModified = ph.LastModified,
                            LastModifiedBy = ph.LastModifiedBy
                        }).ToList(),
                        Created = p.Created,
                        CreatedBy = p.CreatedBy,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy
                    }).ToList(),
                    Created = DateTime.Now,
                    CreatedBy = customer.CreatedBy,
                    LastModified = DateTime.Now,
                    LastModifiedBy = customer.LastModifiedBy,
                    Archived = customer.Archived
                };
                customerEntities.Add(customerEntity);
            }

            return customerEntities;
        }

        /// <summary>
        /// Creates a Customer
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        public int Create(CustomerModel customerModel)
        {
            using (var scope = new TransactionScope())
            {
                var customer = new Customer()
                {
                    AccountNumber = customerModel.AccountNumber,
                    AccountRepId = customerModel.AccountRep.Id,
                    AccountRep = new Contact()
                    {
                        Id = customerModel.AccountRep.Id,
                        EmailAddress = customerModel.AccountRep.EmailAddress,
                        FirstName = customerModel.AccountRep.FirstName,
                        LastName = customerModel.AccountRep.LastName,
                        Created = customerModel.AccountRep.Created,
                        CreatedBy = customerModel.AccountRep.CreatedBy,
                        LastModified = customerModel.AccountRep.LastModified,
                        LastModifiedBy = customerModel.AccountRep.LastModifiedBy
                    },
                    IsParentInd = customerModel.IsParent,
                    ParentId = customerModel.ParentId,
                    //Cannot insert parent through child!!!!!
                    Name = customerModel.Name,
                    ShipToBillInd = customerModel.ShipToBill,
                    WebsiteUrl = customerModel.WebsiteUrl,
                    Addresses = customerModel.Addresses?.Select(p=> new Address()
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
                    PhoneNumbers = customerModel.PhoneNumbers?.Select(p => new PhoneNumber()
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
                    Contacts = customerModel.Contacts.Select(p => new Contact()
                    {
                        Id = p.Id,
                        EmailAddress = p.EmailAddress,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Created = p.Created,
                        CreatedBy = p.CreatedBy,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy
                    }).ToList(),
                    Created = DateTime.Now,
                    CreatedBy = customerModel.CreatedBy,
                    LastModified = DateTime.Now,
                    LastModifiedBy = customerModel.LastModifiedBy,
                    Archived = customerModel.Archived
                };
                if (customer.Addresses != null)
                {
                    foreach (Address addr in customer.Addresses)
                    {
                        _dbActions.Context.Entry(addr).State = addr.Id == 0 ? EntityState.Added : EntityState.Unchanged;
                    }
                }


                if (customer.PhoneNumbers != null)
                {
                    foreach (PhoneNumber phone in customer.PhoneNumbers)
                    {
                        _dbActions.Context.Entry(phone).State = phone.Id == 0 ? EntityState.Added : EntityState.Unchanged;
                    }
                }

                var customerCheck = _dbActions.CustomerRepository.GetSingle(p => p.Name == customer.Name);
                if (customerCheck == null)
                {
                    _dbActions.CustomerRepository.Insert(customer);
                    _dbActions.Save();
                    scope.Complete();
                    return customer.Id;
                }
                scope.Complete();
                return -1;

            }
        }

        /// <summary>
        /// Updates a Customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        public bool Update(int id, CustomerModel customerModel)
        {
            var success = false;
            if (customerModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var customer = _dbActions.CustomerRepository.GetById(id);  //Attached Entity
                    if (customer != null)
                    {
                        customer.AccountNumber = customerModel.AccountNumber;
                        customer.AccountRepId = customerModel.Id;
                        if (customerModel.AccountRep != null)
                        {
                            customer.AccountRep = new Contact()
                            {
                                Id = customerModel.AccountRep.Id,
                                EmailAddress = customerModel.AccountRep.EmailAddress,
                                FirstName = customerModel.AccountRep.FirstName,
                                LastName = customerModel.AccountRep.LastName,
                                Created = customerModel.AccountRep.Id > 0 ? customerModel.AccountRep.Created : DateTime.Now,
                                CreatedBy = customerModel.AccountRep.CreatedBy,
                                LastModified = DateTime.Now,
                                LastModifiedBy = customerModel.AccountRep.LastModifiedBy
                            };
                        }
                        customer.IsParentInd = customerModel.IsParent;
                        customer.ParentId = customerModel.ParentId;
                        //Cannot update parent through child!!!!!
                        customer.Name = customerModel.Name;
                        customer.ShipToBillInd = customerModel.ShipToBill;
                        customer.WebsiteUrl = customerModel.WebsiteUrl;
                        customer.LastModified = DateTime.Now;
                        customer.LastModifiedBy = customerModel.LastModifiedBy;
                        customer.Archived = customerModel.Archived;
                        //Attach / Update Customer's address listing.
                        UpdateCustomerAddresses(customerModel, customer);

                        //Attach / Update Customer's phone number listing.
                        UpdateCustomerPhoneNumbers(customerModel, customer);

                        //Attach / update Customer's Contact listing.
                        UpdateCustomerContacts(customerModel, customer);
                    
                        _dbActions.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular Customer id
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
                    var customer = _dbActions.CustomerRepository.GetById(id);
                    if (customer != null)
                    {
                        if (hardDelete)
                        {
                            _dbActions.CustomerRepository.Delete(customer);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
                        else  // Soft Delete.  Set Archived = true
                        {
                            customer.Archived = true;
                            _dbActions.CustomerRepository.Update(customer);
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
        /// <summary>
        /// Updates any new, deleted, or updated Phone Numbers for the Customer
        /// </summary>
        /// <param name="customerModel"></param>
        /// <param name="customer"></param>
        private void UpdateCustomerPhoneNumbers(CustomerModel customerModel, Customer customer)
        {
            var convertedPhoneNumbers = new List<PhoneNumber>();
            if (customerModel.PhoneNumbers != null)
            {
                convertedPhoneNumbers = customerModel.PhoneNumbers.Select(p => new PhoneNumber()
                {
                    Id = p.Id,
                    PhoneTypeId = p.PhoneTypeId,
                    PhoneType = new PhoneTypeLookup()
                    {
                        Id = p.PhoneType.Id,
                        PhoneType = p.PhoneType.PhoneType,
                        Archived = p.PhoneType.Archived
                    },
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
            }

            if (convertedPhoneNumbers.Count > 0)
            {
                var deletedPhoneNumbers = customer.PhoneNumbers.Except(convertedPhoneNumbers, add => add.Id).ToList();
                var addedPhoneNumbers = convertedPhoneNumbers.Except(customer.PhoneNumbers, add => add.Id).ToList();

                //Remove deleted phone numbers
                deletedPhoneNumbers.ForEach(p => customer.PhoneNumbers.Remove(p));

                // Add new phone numbers
                foreach (PhoneNumber phone in addedPhoneNumbers)
                {
                    if (phone.Id == 0) // New phone number.  Need to insert before we can attach.
                    {
                        phone.Created = DateTime.Now;
                        phone.LastModified = DateTime.Now;
                        _dbActions.Context.Entry(phone.PhoneType).State = EntityState.Unchanged;
                        _dbActions.PhoneNumberRepository.Insert(phone);
                        _dbActions.Save();
                    }

                    // Attach phone numbers from disconnected object
                    if (_dbActions.Context.Entry(phone).State == EntityState.Detached)
                    {
                        _dbActions.Context.PhoneNumbers.Attach(phone);
                    }

                    customer.PhoneNumbers.Add(phone);
                }
            }
        }
        /// <summary>
        /// Updates any new, deleted, or updated Addresses for the Customer
        /// </summary>
        /// <param name="customerModel"></param>
        /// <param name="customer"></param>
        private void UpdateCustomerAddresses(CustomerModel customerModel, Customer customer)
        {
            var convertedAddresses = new List<Address>();
            if (customerModel.Addresses != null)
            {
                convertedAddresses = customerModel.Addresses.Select(p => new Address()
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
            }

            if (convertedAddresses.Count >= 0)
            {
                var deletedAddresses = customer.Addresses.Except(convertedAddresses, add => add.Id).ToList();
                var addedAddresses = convertedAddresses.Except(customer.Addresses, add => add.Id).ToList();

                //Remove deleted addresses
                deletedAddresses.ForEach(a => customer.Addresses.Remove(a));

                // Add new Addresses
                foreach (Address address in addedAddresses)
                {
                    if (address.Id == 0) // New address.  Need to insert before we can attach.
                    {
                        address.Created = DateTime.Now;
                        address.LastModified = DateTime.Now;
                        _dbActions.Context.Entry(address.AddressType).State = EntityState.Unchanged;
                        _dbActions.AddressRepository.Insert(address);
                        _dbActions.Save();
                    }

                    // Attach addresses from disconnected object
                    if (_dbActions.Context.Entry(address).State == EntityState.Detached)
                    {
                        _dbActions.Context.Addresses.Attach(address);
                    }

                    customer.Addresses.Add(address);
                }
            }
        }
        /// <summary>
        /// Updates any new, deleted, or updated Contacts for the Customer
        /// </summary>
        /// <param name="customerModel"></param>
        /// <param name="customer"></param>
        private void UpdateCustomerContacts(CustomerModel customerModel, Customer customer)
        {
            var convertedContacts = new List<Contact>();
            if (customerModel.Contacts != null)
            {
                convertedContacts = customerModel.Contacts.Select(p => new Contact()
                {
                    Id = p.Id,
                    EmailAddress = p.EmailAddress,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Created = p.Created,
                    CreatedBy = p.CreatedBy,
                    LastModified = p.LastModified,
                    LastModifiedBy = p.LastModifiedBy
                }).ToList();
                foreach (Contact contact in convertedContacts)
                {
                    if (contact.Id <= 0) continue;
                    _dbActions.ContactRepository.Update(contact);

                }
            }

            if (convertedContacts.Count > 0)
            {
                var deletedContacts= customer.Contacts.Except(convertedContacts, con => con.Id).ToList();
                var addedContacts = convertedContacts.Except(customer.Contacts, con => con.Id).ToList();

                //Remove deleted contacts
                deletedContacts.ForEach(c => customer.Contacts.Remove(c));

                // Add new phone numbers
                foreach (Contact contact in addedContacts)
                {
                    if (contact.Id == 0) // New contact.  Need to insert before we can attach.
                    {
                        contact.Created = DateTime.Now;
                        contact.LastModified = DateTime.Now;
                        //_dbActions.Context.Entry(contact.PhoneType).State = EntityState.Unchanged;
                        _dbActions.ContactRepository.Insert(contact);
                        _dbActions.Save();
                    }

                    // Attach phone numbers from disconnected object
                    if (_dbActions.Context.Entry(contact).State == EntityState.Detached)
                    {
                        _dbActions.Context.Contacts.Attach(contact);
                    }

                    customer.Contacts.Add(contact);
                }
            }
        }

        #endregion

    }
}
