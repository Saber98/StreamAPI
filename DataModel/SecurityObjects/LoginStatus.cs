using System;
using System.ComponentModel.DataAnnotations;

namespace DataModel.SecurityObjects
{
    public class LoginStatus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public string JwToken { get; set; }
        [Required]
        public DateTime TokenExpires { get; set; }
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
