using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using BusinessEntities;
using BusinessServices;
using BusinessServices.Interfaces;
using Microsoft.Ajax.Utilities;
using SecurityGenerator;
using SecurityServices;
using StreamApi.Models;

namespace StreamApi.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"POST")]
    public class AuthController : ApiController
    {
        private readonly IAuthenticationServices _authenticationServices;
        #region Public Constructor

        public AuthController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        #endregion
        [Route("api/Auth/Login")]
        [HttpPost]
        public HttpResponseMessage Login(LoginUserModel userLogin)
        {
            HttpRequestHeaders headers = Request.Headers;
            // var userName = string.Empty;
            //var password = string.Empty;

            //var userName = Request.GetHeader("userName");
            //if (headers.Contains("userName"))
            //{
            //    userName = headers.GetValues("userName").First();
            //}

            //if (headers.Contains("password"))
            //{
            //    password = headers.GetValues("password").First();
            //}

            UserSecurityModel userSecurity = _authenticationServices.GetUserByUserName(userLogin.Email);

            if (userSecurity != null)
            {
                AuthenticationFunctions authenticationFunctions = new AuthenticationFunctions();

                bool successful = authenticationFunctions.ValidatePassword(userLogin.Password, userSecurity.Password);

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
                string token = authentication.GenerateTokenForUser(userSecurity, roles.ToArray());

                // Save the Security Token to the database for Audit purposes.
                _authenticationServices.UpdateLoginStatus(userSecurity.UserId, token, DateTime.Now.AddMinutes(int.Parse(InternalSettings.TokenExpirationMinutes)));

                AuthTokenModel returnToken = new AuthTokenModel()
                {
                    Token = token,
                    Errors = new List<string>(),
                    Messages = new List<string>() {"Success"}
                };


                return Request.CreateResponse(HttpStatusCode.OK, returnToken, Configuration.Formatters.JsonFormatter);
            }

            AuthTokenModel badReturnToken = new AuthTokenModel
            {
                Token = "",
                Errors = new List<string>() {"Invalid User Name or Password"},
                Messages = new List<string>() {"Invalid Request or Missing Parameters"}
            };
            return Request.CreateResponse(HttpStatusCode.BadRequest, badReturnToken);

        }

        [Route("api/Auth/Logout")]
        [HttpPost]
        public HttpResponseMessage Logout()
        {
            AuthTokenModel returnToken = new AuthTokenModel
            {
                Token = "",
                Errors = new List<string>(),
                Messages = new List<string>() { "User Logged Out" }
            };

            return Request.CreateResponse(HttpStatusCode.OK, returnToken);
        }
    }
}
