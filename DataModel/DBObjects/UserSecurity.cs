using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.DBObjects
{
    [Table("UserSecurity")]
    public class UserSecurity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        [Required]
        public bool LockedOut { get; set; }
        public string IpAddress { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime LastModified { get; set; }
        [Required]
        public string LastModifiedBy { get; set; }

    }
}
