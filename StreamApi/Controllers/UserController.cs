using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using BusinessEntities;
using BusinessEntities.Lookups;
using BusinessServices;
using BusinessServices.Interfaces;


namespace StreamApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserServices _userServices;

        #region Public Constructor

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
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
            var users = _userServices.GetAll(includeArchived);
            var userEntities = users?.ToList();

            return userEntities != null && userEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, userEntities)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Users not found");

        }

        // GET: api/State/5
        public HttpResponseMessage Get(int id)
        {
            var user = _userServices.GetById(id);
            return user != null
                ? Request.CreateResponse(HttpStatusCode.OK, user)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No User found for this id");
        }

        // POST: api/State
        public int Post([FromBody]UserModel user)
        {
            return _userServices.Create(user);
        }

        // PUT: api/State/5
        public bool Put(int id, [FromBody]UserModel user)
        {
            return id > 0 && _userServices.Update(id, user);
        }

        // DELETE: api/State/5
        public bool Delete(int id)
        {
            HttpRequestHeaders headers = Request.Headers;
            bool hardDelete = false;

            if (headers.Contains("hard_delete"))
            {
                hardDelete = bool.Parse(headers.GetValues("hard_detete").First());
            }
            return id > 0 && (_userServices).Delete(id, hardDelete);
        }
    }
}
