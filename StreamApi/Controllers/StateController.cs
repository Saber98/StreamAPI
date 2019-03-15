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
    public class StateController : ApiController
    {
        private readonly IStateServices _stateServices;

        #region Public Constructor

        public StateController(IStateServices stateServices)
        {
            _stateServices = stateServices;
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
            var states = _stateServices.GetAll(includeArchived);
            var stateEntities = states?.ToList();

            return stateEntities != null && stateEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, stateEntities)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "States not found");

        }

        // GET: api/State/5
        public HttpResponseMessage Get(int id)
        {
            var state = _stateServices.GetById(id);
            return state != null
                ? Request.CreateResponse(HttpStatusCode.OK, state)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No State found for this id");
        }

        // POST: api/State
        public int Post([FromBody]StateModel state)
        {
            return _stateServices.Create(state);
        }

        // PUT: api/State/5
        public bool Put(int id, [FromBody]StateModel state)
        {
            return id > 0 && _stateServices.Update(id, state);
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
            return id > 0 && _stateServices.Delete(id, hardDelete);
        }
    }
}
