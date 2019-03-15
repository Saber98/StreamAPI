using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessServices;
using BusinessServices.Interfaces;


namespace StreamApi.Controllers
{
    public class AddressController : ApiController
    {
        private readonly IAddressServices _addressServices;

        #region Public Constructor

        public AddressController(IAddressServices addressServices)
        {
            _addressServices = addressServices;
        }

        #endregion
        // GET: api/Lookup
        public HttpResponseMessage Get()
        {
            HttpRequestHeaders headers = Request.Headers;
            bool includeArchived = false;
            string predicate = string.Empty;

            if (headers.Contains("includeArchived"))
            {
                includeArchived = bool.Parse(headers.GetValues("includeArchived").First());
            }

            var addresses = _addressServices.GetAll(includeArchived);
            var addressEntities = addresses?.ToList();
            
            return addressEntities != null && addressEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, addressEntities)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Addresses not found");

        }

        // GET: api/Lookup/5
        public HttpResponseMessage Get(int id)
        {
            var address = _addressServices.GetById(id);
            return address != null
                ? Request.CreateResponse(HttpStatusCode.OK, address)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Address found for this id");
        }

        // POST: api/Lookup
        public HttpResponseMessage Post([FromBody]AddressModel address)
        {
            int addressId = _addressServices.Create(address);
            return addressId > 0
                ? Request.CreateResponse(HttpStatusCode.OK, addressId)
                : Request.CreateResponse(HttpStatusCode.Conflict, "Address exists.");
        }

        // PUT: api/Lookup/5
        public bool Put(int id, [FromBody]AddressModel address)
        {
            return id > 0 && _addressServices.Update(id, address);
        }

        // DELETE: api/Lookup/5
        public bool Delete(int id)
        {
            HttpRequestHeaders headers = Request.Headers;
            bool hardDelete = false;

            if (headers.Contains("hard_delete"))
            {
                hardDelete = bool.Parse(headers.GetValues("hard_delete").First());
            }
            return id > 0 && _addressServices.Delete(id, hardDelete);
        }
    }
}
