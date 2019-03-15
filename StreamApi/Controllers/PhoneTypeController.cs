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
    public class PhoneTypeController : ApiController
    {
        private readonly IPhoneTypeServices _phoneTypeServices;

        #region Public Constructor

        public PhoneTypeController(IPhoneTypeServices phoneTypeServices)
        {
            _phoneTypeServices = phoneTypeServices;
        }

        #endregion
        // GET: api/LookupType
        public HttpResponseMessage Get()
        {
            HttpRequestHeaders headers = Request.Headers;
            bool includeArchived = false;

            if (headers.Contains("includeArchived"))
            {
                includeArchived = bool.Parse(headers.GetValues("includeArchived").First());
            }
            var phoneTypes = _phoneTypeServices.GetAll(includeArchived);
            var phoneTypeEntities = phoneTypes as List<PhoneTypeModel> ?? phoneTypes.ToList();

            return phoneTypeEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, phoneTypeEntities)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Phone Types not found");

        }

        // GET: api/LookupType/5
        public HttpResponseMessage Get(int id)
        {
            var phoneType = _phoneTypeServices.GetById(id);
            return phoneType != null
                ? Request.CreateResponse(HttpStatusCode.OK, phoneType)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Phone Type found for this id");
        }

        // POST: api/LookupType
        public HttpResponseMessage Post([FromBody]PhoneTypeModel phoneType)
        {
            int phoneTypeId = _phoneTypeServices.Create(phoneType);
            return phoneTypeId > 0
                ? Request.CreateResponse(HttpStatusCode.OK, phoneTypeId)
                : Request.CreateResponse(HttpStatusCode.Conflict, "Phone Type with this name exists.");
        }

        // PUT: api/LookupType/5
        public bool Put(int id, [FromBody]PhoneTypeModel phoneType)
        {
            return id > 0 && _phoneTypeServices.Update(id, phoneType);
        }

        // DELETE: api/LookupType/5
        public bool Delete(int id)
        {
            HttpRequestHeaders headers = Request.Headers;
            bool hardDelete = false;

            if (headers.Contains("hard_delete"))
            {
                hardDelete = bool.Parse(headers.GetValues("hard_detete").First());
            }
            return id > 0 && _phoneTypeServices.Delete(id, hardDelete);
        }
    }
}
