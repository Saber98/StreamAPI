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
    public class AddressTypeController : ApiController
    {
        private readonly IAddressTypeServices _addressTypeServices;

        #region Public Constructor

        public AddressTypeController(IAddressTypeServices addressTypeServices)
        {
            _addressTypeServices = addressTypeServices;
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
            var addressTypes = _addressTypeServices.GetAll(includeArchived);
            var addressTypeEntities = addressTypes as List<AddressTypeModel> ?? addressTypes.ToList();

            return addressTypeEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, addressTypeEntities)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Address Types not found");

        }

        // GET: api/LookupType/5
        public HttpResponseMessage Get(int id)
        {
            var addressType = _addressTypeServices.GetById(id);
            return addressType != null
                ? Request.CreateResponse(HttpStatusCode.OK, addressType)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Address Type found for this id");
        }

        // POST: api/LookupType
        public HttpResponseMessage Post([FromBody]AddressTypeModel addressType)
        {
            int addressTypeId = _addressTypeServices.Create(addressType);
            return addressTypeId > 0
                ? Request.CreateResponse(HttpStatusCode.OK, addressTypeId)
                : Request.CreateResponse(HttpStatusCode.Conflict, "Address Type with this name exists.");
        }

        // PUT: api/LookupType/5
        public bool Put(int id, [FromBody]AddressTypeModel addressType)
        {
            return id > 0 && _addressTypeServices.Update(id, addressType);
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
            return id > 0 && _addressTypeServices.Delete(id, hardDelete);
        }
    }
}
