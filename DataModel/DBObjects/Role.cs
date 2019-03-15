using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModel.DBObjects
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string Description { get; set; }
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


        public virtual ICollection<User> Users { get; set; }

    }
}
