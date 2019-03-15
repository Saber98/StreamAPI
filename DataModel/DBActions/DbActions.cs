using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using DataModel.DBObjects;
using DataModel.DBObjects.Lookups;
using DataModel.GenericRepository;
using DataModel.SecurityObjects;

namespace DataModel.DBActions
{
    public class DbActions : IDisposable, IDbActions
    {
        #region Private member variables...

        private readonly SIMSDataDBContext _context;
        private GenericRepository<Address> _addressRepository;
        private GenericRepository<AddressTypeLookup> _addressTypeRepository;
        private GenericRepository<StateLookup> _stateRepository;
        private GenericRepository<PhoneNumber> _phoneNumberRepository;
        private GenericRepository<PhoneTypeLookup> _phoneTypeRepository;
        private GenericRepository<Contact> _contactRepository;
        private GenericRepository<Customer> _customerRepository;
        private GenericRepository<User> _userRepository;
        private GenericRepository<UserSecurity> _userSecurityRepository;
        private GenericRepository<Role> _roleRepository;

        private GenericRepository<LoginStatus> _loginStatusRepository;
        #endregion

        public DbActions()
        {
            _context = new SIMSDataDBContext();
        }

        #region Public Repository Creation properties...

        public SIMSDataDBContext Context => _context;

        /// <summary>
        /// Get/Set Property for address repository.
        /// </summary>
        public GenericRepository<Address> AddressRepository
        {
            get
            {
                if (_addressRepository == null)
                    _addressRepository = new GenericRepository<Address>(_context);
                return _addressRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for address type repository.
        /// </summary>
        public GenericRepository<AddressTypeLookup> AddressTypeRepository
        {
            get
            {
                if (_addressTypeRepository == null)
                    _addressTypeRepository = new GenericRepository<AddressTypeLookup>(_context);
                return _addressTypeRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for phone number type repository.
        /// </summary>
        public GenericRepository<PhoneTypeLookup> PhoneTypeRepository
        {
            get
            {
                if (_phoneTypeRepository == null)
                   _phoneTypeRepository = new GenericRepository<PhoneTypeLookup>(_context);
                return _phoneTypeRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for phone number repository.
        /// </summary>
        public GenericRepository<PhoneNumber> PhoneNumberRepository
        {
            get
            {
                if (_phoneNumberRepository == null)
                    _phoneNumberRepository = new GenericRepository<PhoneNumber>(_context);
                return _phoneNumberRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for state repository.
        /// </summary>
        public GenericRepository<StateLookup> StateRepository
        {
            get
            {
                if (_stateRepository == null)
                    _stateRepository = new GenericRepository<StateLookup>(_context);
                return _stateRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for contact repository.
        /// </summary>
        public GenericRepository<Contact> ContactRepository
        {
            get
            {
                if (_contactRepository == null)
                    _contactRepository = new GenericRepository<Contact>(_context);
                return _contactRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for customer repository.
        /// </summary>
        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new GenericRepository<Customer>(_context);
                return _customerRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new GenericRepository<User>(_context);
                return _userRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for user security repository.
        /// </summary>
        public GenericRepository<UserSecurity> UserSecurityRepository
        {
            get
            {
                if (_userSecurityRepository == null)
                    _userSecurityRepository = new GenericRepository<UserSecurity>(_context);
                return _userSecurityRepository;
            }
        }
        /// <summary>
        /// Get/Set Property for user role repository.
        /// </summary>
        public GenericRepository<Role> UserRoleRepository
        {
            get
            {
                if (_roleRepository == null)
                    _roleRepository = new GenericRepository<Role>(_context);
                return _roleRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for login status repository.
        /// </summary>
        public GenericRepository<LoginStatus> LoginStatusRepository
        {
            get
            {
                if (_loginStatusRepository == null)
                    _loginStatusRepository = new GenericRepository<LoginStatus>(_context);
                return _loginStatusRepository;
            }
        }
        #endregion
        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\Temp\errors.txt", outputLines);

                throw;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool _disposed;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("DbActions is being disposed");
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
