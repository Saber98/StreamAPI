using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamApi.Models
{
    public class LoginUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthTokenModel
    {
        public string Token { get; set; }
        public List<string> Messages { get; set; }
        public List<string> Errors{ get; set; }
    }
}