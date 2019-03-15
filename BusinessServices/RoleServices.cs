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
    public class RoleServices:IRoleServices
        
    {
        private readonly DbActions _dbActions;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public RoleServices(DbActions dbActions)
        {
            _dbActions = dbActions;
        }

        /// <summary>
        /// Fetches user details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleModel GetById(int id)
        {
            var userRole = _dbActions.UserRoleRepository.GetById(id);
            if (userRole != null)
            {
                RoleModel userRoleEntity = new RoleModel()
                {
                    Id = userRole.Id,
                    Name = userRole.RoleName,
                    Description = userRole.Description,
                    AssignedUsers = userRole.Users.Select(p => new UserModel()
                    {
                        Id = p.Id,
                        UserName = p.UserName,
                        EmailAddress = p.EmailAddress,
                        ContactId = p.ContactId,
                        Created = p.Created,
                        CreatedBy = p.CreatedBy,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy,
                        Archived = p.Archived
                    }).ToList(),
                    Created = userRole.Created,
                    CreatedBy = userRole.CreatedBy,
                    LastModified = userRole.LastModified,
                    LastModifiedBy = userRole.LastModifiedBy,
                    Archived = userRole.Archived

                };
                return userRoleEntity;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the users.
        /// </summary>
        /// <param name="includeArchived"></param>
        /// <returns></returns>
        public IEnumerable<RoleModel> GetAll(bool includeArchived)
        {
            var userRoles = includeArchived ? _dbActions.UserRoleRepository.GetAll().ToList() 
                                        : _dbActions.UserRoleRepository.GetMany(p => p.Archived == false).ToList();
            
            if (!userRoles.Any()) return null;
            List<RoleModel> userRoleEntities = new List<RoleModel>();
            foreach (Role userRole in userRoles)
            {
                RoleModel userRoleEntity = new RoleModel()
                {
                    Id = userRole.Id,
                    Name = userRole.RoleName,
                    Description = userRole.Description,
                    AssignedUsers = userRole.Users.Select(p => new UserModel()
                    {
                        Id = p.Id,
                        UserName = p.UserName,
                        EmailAddress = p.EmailAddress,
                        ContactId = p.ContactId,
                        Created = p.Created,
                        CreatedBy = p.CreatedBy,
                        LastModified = p.LastModified,
                        LastModifiedBy = p.LastModifiedBy,
                        Archived = p.Archived
                    }).ToList(),
                    Created = userRole.Created,
                    CreatedBy = userRole.CreatedBy,
                    LastModified = userRole.LastModified,
                    LastModifiedBy = userRole.LastModifiedBy,
                    Archived = userRole.Archived

                };
                userRoleEntities.Add(userRoleEntity);
            }

            return userRoleEntities;
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="userRoleModel"></param>
        /// <returns></returns>
        public int Create(RoleModel userRoleModel)
        {
            using (var scope = new TransactionScope())
            {
                var userRole = new Role()
                {
                    RoleName = userRoleModel.Name,
                    Description = userRoleModel.Description,
                    Created = userRoleModel.Created,
                    CreatedBy = userRoleModel.CreatedBy,
                    LastModified = userRoleModel.LastModified,
                    LastModifiedBy = userRoleModel.LastModifiedBy,
                    Archived = userRoleModel.Archived,

                    //Users = userRoleModel.AssignedUsers.Select(p => new User()
                    //{
                    //    Id = p.Id,
                    //    UserName = p.UserName,
                    //    EmailAddress = p.EmailAddress,
                    //    Created = p.Created,
                    //    CreatedBy = p.CreatedBy,
                    //    LastModified = p.LastModified,
                    //    LastModifiedBy = p.LastModifiedBy,
                    //    Archived = p.Archived,
                    //}).ToList()
                };
                
                var userRoleCheck = _dbActions.UserRoleRepository.GetSingle(p => p.RoleName == userRoleModel.Name);
                if (userRoleCheck == null)
                {
                    _dbActions.UserRoleRepository.Insert(userRole);
                    _dbActions.Save();
                    scope.Complete();
                    return userRole.Id;
                }
                scope.Complete();
                return -1;

            }
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRoleModel"></param>
        /// <returns></returns>
        public bool Update(int id, RoleModel userRoleModel)
        {
            var success = false;
            if (userRoleModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var userRole = _dbActions.UserRoleRepository.GetById(id);
                    if (userRole != null)
                    {
                        userRole.Id = userRoleModel.Id;
                        userRole.RoleName = userRoleModel.Name;
                        userRole.Description = userRoleModel.Description;
                        userRole.Created = userRoleModel.Created;
                        userRole.CreatedBy = userRoleModel.CreatedBy;
                        userRole.LastModified = userRoleModel.LastModified;
                        userRole.LastModifiedBy = userRoleModel.LastModifiedBy;
                        userRole.Archived = userRoleModel.Archived;

                        var userRoleCheck = _dbActions.UserRoleRepository.GetSingle(p => p.RoleName == userRole.RoleName);
                        if (userRoleCheck == null)
                        {
                            _dbActions.UserRoleRepository.Update(userRole);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
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
                    var user = _dbActions.UserRoleRepository.GetById(id);
                    if (user != null)
                    {
                        if (hardDelete)
                        {
                            _dbActions.UserRoleRepository.Delete(user);
                            _dbActions.Save();
                            scope.Complete();
                            success = true;
                        }
                        else  // Soft Delete.  Set Archived = true
                        {
                            user.Archived = true;
                            _dbActions.UserRoleRepository.Update(user);
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
