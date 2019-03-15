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
    public class PhoneController : ApiController
    {
        private readonly IPhoneServices _phoneServices;

        #region Public Constructor

        public PhoneController(IPhoneServices phoneServices)
        {
            _phoneServices = phoneServices;
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
            IEnumerable<PhoneModel> phones = _phoneServices.GetAll(includeArchived);
            var phoneEntities = phones == null ? new List<PhoneModel>() : phones.ToList();

            return phoneEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, phoneEntities)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Phone Numbers not found");

        }

        // GET: api/LookupType/5
        public HttpResponseMessage Get(int id)
        {
            var phone = _phoneServices.GetById(id);
            return phone != null
                ? Request.CreateResponse(HttpStatusCode.OK, phone)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Phone Number found for this id");
        }

        // POST: api/LookupType
        public HttpResponseMessage Post([FromBody]PhoneModel phone)
        {
            int phoneId = _phoneServices.Create(phone);
            return phoneId > 0
                ? Request.CreateResponse(HttpStatusCode.OK, phoneId)
                : Request.CreateResponse(HttpStatusCode.Conflict, "Phone Number exists.");
        }

        // PUT: api/LookupType/5
        public bool Put(int id, [FromBody]PhoneModel phone)
        {
            return id > 0 && _phoneServices.Update(id, phone);
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
            return id > 0 && _phoneServices.Delete(id, hardDelete);
        }
    }
}
