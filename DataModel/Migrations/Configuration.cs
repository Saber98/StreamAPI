using System.Collections.Generic;
using System.Data.Entity.Validation;
using DataModel.DBObjects;
using DataModel.DBObjects.Lookups;

namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataModel.SIMSDataDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataModel.SIMSDataDBContext context)
        {
            #region States Seed
            if (!context.States.Any())
            {
                IList<StateLookup> states = new List<StateLookup>();
                // State Lookups
                states.Add(new StateLookup() { Id = 1, StateName = "Alabama", StateAbbreviation = "AL", Archived = false });
                states.Add(new StateLookup() { Id = 2, StateName = "Alaska", StateAbbreviation = "AK", Archived = false });
                states.Add(new StateLookup() { Id = 3, StateName = "Arizona", StateAbbreviation = "AZ", Archived = false });
                states.Add(new StateLookup() { Id = 4, StateName = "Arkansas", StateAbbreviation = "AR", Archived = false });
                states.Add(new StateLookup() { Id = 5, StateName = "California", StateAbbreviation = "CA", Archived = false });
                states.Add(new StateLookup() { Id = 6, StateName = "Colorado", StateAbbreviation = "CO", Archived = false });
                states.Add(new StateLookup() { Id = 7, StateName = "Connecticut", StateAbbreviation = "CT", Archived = false });
                states.Add(new StateLookup() { Id = 8, StateName = "Delaware", StateAbbreviation = "DE", Archived = false });
                states.Add(new StateLookup() { Id = 9, StateName = "Florida", StateAbbreviation = "FL", Archived = false });
                states.Add(new StateLookup() { Id = 10, StateName = "Georgia", StateAbbreviation = "GA", Archived = false });
                states.Add(new StateLookup() { Id = 11, StateName = "Hawaii", StateAbbreviation = "HI", Archived = false });
                states.Add(new StateLookup() { Id = 12, StateName = "Idaho", StateAbbreviation = "ID", Archived = false });
                states.Add(new StateLookup() { Id = 13, StateName = "Illinois", StateAbbreviation = "IL", Archived = false });
                states.Add(new StateLookup() { Id = 14, StateName = "Indiana", StateAbbreviation = "IN", Archived = false });
                states.Add(new StateLookup() { Id = 15, StateName = "Iowa", StateAbbreviation = "IA", Archived = false });
                states.Add(new StateLookup() { Id = 16, StateName = "Kansas", StateAbbreviation = "KS", Archived = false });
                states.Add(new StateLookup() { Id = 17, StateName = "Kentucky", StateAbbreviation = "KY", Archived = false });
                states.Add(new StateLookup() { Id = 18, StateName = "Louisiana", StateAbbreviation = "LA", Archived = false });
                states.Add(new StateLookup() { Id = 19, StateName = "Maine", StateAbbreviation = "ME", Archived = false });
                states.Add(new StateLookup() { Id = 20, StateName = "Maryland", StateAbbreviation = "MD", Archived = false });
                states.Add(new StateLookup() { Id = 21, StateName = "Massachusetts", StateAbbreviation = "MA", Archived = false });
                states.Add(new StateLookup() { Id = 22, StateName = "Michigan", StateAbbreviation = "MI", Archived = false });
                states.Add(new StateLookup() { Id = 23, StateName = "Minnesota", StateAbbreviation = "MN", Archived = false });
                states.Add(new StateLookup() { Id = 24, StateName = "Mississippi", StateAbbreviation = "MS", Archived = false });
                states.Add(new StateLookup() { Id = 25, StateName = "Missouri", StateAbbreviation = "MO", Archived = false });
                states.Add(new StateLookup() { Id = 26, StateName = "Montana", StateAbbreviation = "MT", Archived = false });
                states.Add(new StateLookup() { Id = 27, StateName = "Nebraska", StateAbbreviation = "NE", Archived = false });
                states.Add(new StateLookup() { Id = 28, StateName = "Nevada", StateAbbreviation = "NV", Archived = false });
                states.Add(new StateLookup() { Id = 29, StateName = "New Hampshire", StateAbbreviation = "NH", Archived = false });
                states.Add(new StateLookup() { Id = 30, StateName = "New Jersey", StateAbbreviation = "NJ", Archived = false });
                states.Add(new StateLookup() { Id = 31, StateName = "New Mexico", StateAbbreviation = "NM", Archived = false });
                states.Add(new StateLookup() { Id = 32, StateName = "New York", StateAbbreviation = "NY", Archived = false });
                states.Add(new StateLookup() { Id = 33, StateName = "North Carolina", StateAbbreviation = "NC", Archived = false });
                states.Add(new StateLookup() { Id = 34, StateName = "North Dakota", StateAbbreviation = "ND", Archived = false });
                states.Add(new StateLookup() { Id = 35, StateName = "Ohio", StateAbbreviation = "OH", Archived = false });
                states.Add(new StateLookup() { Id = 36, StateName = "Oklahoma", StateAbbreviation = "OK", Archived = false });
                states.Add(new StateLookup() { Id = 37, StateName = "Oregon", StateAbbreviation = "OR", Archived = false });
                states.Add(new StateLookup() { Id = 38, StateName = "Pennsylvania", StateAbbreviation = "PA", Archived = false });
                states.Add(new StateLookup() { Id = 39, StateName = "Rhode Island", StateAbbreviation = "RI", Archived = false });
                states.Add(new StateLookup() { Id = 40, StateName = "South Carolina", StateAbbreviation = "SC", Archived = false });
                states.Add(new StateLookup() { Id = 41, StateName = "South Dakota", StateAbbreviation = "SD", Archived = false });
                states.Add(new StateLookup() { Id = 42, StateName = "Tennessee", StateAbbreviation = "TN", Archived = false });
                states.Add(new StateLookup() { Id = 43, StateName = "Texas", StateAbbreviation = "TX", Archived = false });
                states.Add(new StateLookup() { Id = 44, StateName = "Utah", StateAbbreviation = "UT", Archived = false });
                states.Add(new StateLookup() { Id = 45, StateName = "Vermont", StateAbbreviation = "VT", Archived = false });
                states.Add(new StateLookup() { Id = 46, StateName = "Virginia", StateAbbreviation = "VA", Archived = false });
                states.Add(new StateLookup() { Id = 47, StateName = "Washington", StateAbbreviation = "WA", Archived = false });
                states.Add(new StateLookup() { Id = 48, StateName = "West Virginia", StateAbbreviation = "WV", Archived = false });
                states.Add(new StateLookup() { Id = 49, StateName = "Wisconsin", StateAbbreviation = "WI", Archived = false });
                states.Add(new StateLookup() { Id = 50, StateName = "Wyoming", StateAbbreviation = "WY", Archived = false });

                context.States.AddRange(states);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            #endregion

            #region Address Types Seed

            if (!context.AddressTypes.Any())
            {
                IList<AddressTypeLookup> addressTypes = new List<AddressTypeLookup>();

                addressTypes.Add(new AddressTypeLookup() { Id = 1, AddressType = "Billing" });
                addressTypes.Add(new AddressTypeLookup() { Id = 2, AddressType = "Shipping" });
                addressTypes.Add(new AddressTypeLookup() { Id = 3, AddressType = "Other" });

                context.AddressTypes.AddRange(addressTypes);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            #endregion

            #region Phone Number Types Seed

            if (!context.PhoneNumberTypes.Any())
            {
                IList<PhoneTypeLookup> phoneNumberTypes = new List<PhoneTypeLookup>();
                phoneNumberTypes.Add(new PhoneTypeLookup() { Id = 1, PhoneType = "Home" });
                phoneNumberTypes.Add(new PhoneTypeLookup() { Id = 2, PhoneType = "Work" });
                phoneNumberTypes.Add(new PhoneTypeLookup() { Id = 3, PhoneType = "Mobile" });
                phoneNumberTypes.Add(new PhoneTypeLookup() { Id = 4, PhoneType = "Other" });

                context.PhoneNumberTypes.AddRange(phoneNumberTypes);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            #endregion

            #region Address Seed

            if (!context.Addresses.Any())
            {
                IList<Address> addresses = new List<Address>();
                addresses.Add(new Address()
                {
                    Id = 1,
                    AddressTypeId = 3,
                    Address1 = "123 This St.",
                    Address2 = "",
                    City = "AnyTown",
                    Zip = "12345",
                    StateId = 40,
                    CreatedBy = "SETUP",
                    Created = DateTime.Now,
                    LastModifiedBy = "SETUP",
                    LastModified = DateTime.Now,
                    Archived = false
                });
                context.Addresses.AddRange(addresses);
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var errorMessages = e.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
                Console.WriteLine(e);
                throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
            }
            #endregion

            #region Phone Seed

            if (!context.PhoneNumbers.Any())
            {
                IList<PhoneNumber> phoneNumbers = new List<PhoneNumber>();
                phoneNumbers.Add(new PhoneNumber()
                {
                    Id = 1,
                    AreaCode = "555",
                    PhoneTypeId = 4,
                    Number = "555-5555",
                    Extension = "",
                    CreatedBy = "SETUP",
                    Created = DateTime.Now,
                    LastModifiedBy = "SETUP",
                    LastModified = DateTime.Now,
                    Archived = false
                });
                context.PhoneNumbers.AddRange(phoneNumbers);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            #endregion

            #region Contact Seed

            if (!context.Contacts.Any())
            {
                IList<Contact> contacts = new List<Contact>();
                contacts.Add(new Contact()
                {
                    Id = 1,
                    EmailAddress = "user@domain.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserId = 1,
                    Archived = false,
                    CreatedBy = "SETUP",
                    Created = DateTime.Now,
                    LastModifiedBy = "SETUP",
                    LastModified = DateTime.Now
                });
                context.Contacts.AddRange(contacts);
            }
            #endregion

            #region Admin / User Seed

            #region UserRoles
            if (!context.Roles.Any())
            {
                IList<Role> roles = new List<Role>();
                Role userRole = new Role()
                {
                    Id = 1,
                    RoleName = "Admin",
                    Description = "System Administrator Role",
                    Archived = false,
                    CreatedBy = "SETUP",
                    Created = DateTime.Now,
                    LastModifiedBy = "SETUP",
                    LastModified = DateTime.Now
                };
                roles.Add(userRole);
                userRole = new Role()
                {
                    Id = 2,
                    RoleName = "ReadWrite",
                    Description = "Read/Write User Role",
                    Archived = false,
                    CreatedBy = "SETUP",
                    Created = DateTime.Now,
                    LastModifiedBy = "SETUP",
                    LastModified = DateTime.Now
                };
                roles.Add(userRole);
                userRole = new Role()
                {
                    Id = 3,
                    RoleName = "Readonly",
                    Description = "Readonly User Role",
                    Archived = false,
                    CreatedBy = "SETUP",
                    Created = DateTime.Now,
                    LastModifiedBy = "SETUP",
                    LastModified = DateTime.Now
                };
                roles.Add(userRole);
                context.Roles.AddRange(roles);

                context.SaveChanges();
            }

            #endregion
            User adminUser = new User()
            {
                Id = 1,
                ContactId = 1,
                EmailAddress = "support@streamlinesoftware.com",
                UserName = "admin",
                Archived = false,
                CreatedBy = "SETUP",
                Created = DateTime.Now,
                LastModifiedBy = "SETUP",
                LastModified = DateTime.Now
            };

            if (!context.Users.Any())
            {
                // Get Role to add to Admin seed.
                Role adminRole = context.Roles.SingleOrDefault(p => p.Id == 1);
                adminUser.Roles = new List<Role>();
                adminUser.Roles.Add(adminRole);
                IList<User> users = new List<User>();
                users.Add(adminUser);

                context.Users.AddRange(users);
            }
            #region User Security

            if (!context.UserSecurity.Any())
            {
                IList<UserSecurity> userSecurities = new List<UserSecurity>();

                userSecurities.Add(new UserSecurity()
                {
                    Id = 1,
                    LastLogin = DateTime.Now,
                    LockedOut = false,
                    UserId = 1,
                    User = adminUser,
                    IpAddress = "127.0.0.1",
                    Password = "1000:kIiAQqUhEgMsdl+6YEwDlQ==:1+81Ql95yvvIipUSH0E2wK1pomc=",
                    CreatedBy = "SETUP",
                    Created = DateTime.Now,
                    LastModifiedBy = "SETUP",
                    LastModified = DateTime.Now,
                });
                context.UserSecurity.AddRange(userSecurities);
            }
            #endregion

            
            #endregion


            base.Seed(context);
        }
    }
}
