using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;
using BusinessServices.Interfaces;
using Microsoft.Ajax.Utilities;
using SecurityGenerator;
using SecurityServices;

namespace StreamApi.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IAuthenticationServices _authenticationServices;
        #region Public Constructor

        public AuthController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        #endregion
        [HttpPost]
        public HttpResponseMessage Post()
        {
            HttpRequestHeaders headers = Request.Headers;
            // var userName = string.Empty;
            var password = string.Empty;

            var userName = Request.GetHeader("userName");
            //if (headers.Contains("userName"))
            //{
            //    userName = headers.GetValues("userName").First();
            //}

            if (headers.Contains("password"))
            {
                password = headers.GetValues("password").First();
            }

            UserSecurityModel userSecurity = _authenticationServices.GetUserByUserName(userName);

            if (userSecurity != null)
            {
                AuthenticationFunctions authenticationFunctions = new AuthenticationFunctions();

                bool successful = authenticationFunctions.ValidatePassword(password, userSecurity.Password);

                if (!successful)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid User or Password", Configuration.Formatters.JsonFormatter);
                }

                AuthenticationModule authentication = new AuthenticationModule();
                List<string> roles = new List<string>();
                foreach (RoleModel role in userSecurity.User.Roles)
                {
                    roles.Add(role.Name);
                }
                string token = authentication.GenerateTokenForUser(userName, userSecurity.UserId, roles.ToArray());

                // Save the Security Token to the database for Audit purposes.
                _authenticationServices.UpdateLoginStatus(userSecurity.UserId, token, DateTime.Now.AddMinutes(int.Parse(InternalSettings.TokenExpirationMinutes)));

                return Request.CreateResponse(HttpStatusCode.OK, token, Configuration.Formatters.JsonFormatter);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request or Missing Parameters");

        }
    }
}
