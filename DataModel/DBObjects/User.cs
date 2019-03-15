using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime LastModified { get; set; }
        [Required]
        public string LastModifiedBy { get; set; }
        [Required]
        public bool Archived { get; set; }


        public virtual ICollection<UserSecurity> UserSecurities { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

    }
}
