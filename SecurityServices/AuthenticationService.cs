using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BusinessEntities;
using BusinessEntities.Lookups;
using DataModel;
using DataModel.DBActions;
using DataModel.DBObjects;
using DataModel.SecurityObjects;
using BusinessServices.Interfaces;

namespace BusinessServices
{
    public class AuthenticationService:IAuthenticationServices
    {
        //private readonly SIMSDataDBContext _dbContext;
        private readonly DbActions _dbActions;

        public AuthenticationService()
        {
            //_dbContext = new SIMSDataDBContext();
            _dbActions = new DbActions();
        }

        public UserSecurityModel GetUserByUserName(string userName)
        {
            UserSecurity userSecurity = _dbActions.UserSecurityRepository.GetSingle(p => p.User.UserName == userName);

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
                            Created = p.Created,
                            CreatedBy = p.CreatedBy,
                            LastModified = p.LastModified,
                            LastModifiedBy = p.LastModifiedBy,
                            Archived = p.Archived
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

        public bool UpdateLoginStatus(int userId, string jwToken, DateTime tokenExpiration)
        {
            try
            {
                var loginStatus = _dbActions.LoginStatusRepository.GetSingle(p => p.UserId == userId);

                if (loginStatus != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        loginStatus.Id = loginStatus.Id;
                        loginStatus.UserId = userId;
                        loginStatus.JwToken = jwToken;
                        loginStatus.TokenExpires = tokenExpiration;
                        loginStatus.CreatedBy = loginStatus.CreatedBy;
                        loginStatus.Created = loginStatus.Created;
                        loginStatus.LastModified = DateTime.Now;
                        loginStatus.LastModifiedBy = "SYSTEM";

                        _dbActions.LoginStatusRepository.Update(loginStatus);
                        _dbActions.Save();
                        scope.Complete();
                        return true;
                    }
                }

                using (var scope = new TransactionScope())
                {
                    LoginStatus newLoginStatus = new LoginStatus
                    {
                        UserId = userId,
                        JwToken = jwToken,
                        TokenExpires = tokenExpiration,
                        CreatedBy = "SYSTEM",
                        Created = DateTime.Now,
                        LastModified = DateTime.Now,
                        LastModifiedBy = "SYSTEM"
                    };

                    _dbActions.LoginStatusRepository.Insert(newLoginStatus);
                    _dbActions.Save();
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
            

        }
    }
}
