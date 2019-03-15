using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Security;
using BusinessEntities;
using BusinessEntities.Lookups;
using BusinessServices;
using BusinessServices.Interfaces;


namespace StreamApi.Controllers
{
    [JwtAuthenticationFilter]
    public class UserSecurityController : ApiController
    {
        private readonly IUserSecurityServices _userSecurityServices;

        #region Public Constructor

        public UserSecurityController(IUserSecurityServices userSecurityServices)
        {
            _userSecurityServices = userSecurityServices;
        }

        #endregion
        // GET: api/State
        
        public HttpResponseMessage Get()
        {
            HttpRequestHeaders headers = Request.Headers;
            bool includeArchived = false;

            if (headers.Contains("includeArchived"))
            {
                includeArchived = bool.Parse(headers.GetValues("includeArchived").First());
            }
            var userSecurity = _userSecurityServices.GetAll(includeArchived);
            var userSecurityEntities = userSecurity?.ToList();

            return userSecurityEntities != null && userSecurityEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, userSecurityEntities)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Security not found");

        }

        // GET: api/State/5
        public HttpResponseMessage Get(int id)
        {
            var userSecurity = _userSecurityServices.GetById(id);
            return userSecurity != null
                ? Request.CreateResponse(HttpStatusCode.OK, userSecurity)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No User Security found for this id");
        }

        [Authorize(Roles = "Admin")]
        // POST: api/State
        public int Post([FromBody]UserSecurityModel userSecurity)
        {
            return _userSecurityServices.Create(userSecurity);
        }

        [Authorize(Roles = "Admin")]
        // PUT: api/State/5
        public bool Put(int id, [FromBody]UserSecurityModel userSecurity)
        {
            return id > 0 && _userSecurityServices.Update(id, userSecurity);
        }

        [Authorize(Roles = "Admin")]
        // DELETE: api/State/5
        public bool Delete(int id)
        {
            HttpRequestHeaders headers = Request.Headers;
            bool hardDelete = false;

            if (headers.Contains("hard_delete"))
            {
                hardDelete = bool.Parse(headers.GetValues("hard_detete").First());
            }
            return id > 0 && (_userSecurityServices).Delete(id, hardDelete);
        }
    }
}
