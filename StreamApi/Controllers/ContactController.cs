using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;
using BusinessServices.Interfaces;


namespace StreamApi.Controllers
{
    public class ContactController : ApiController
    {
        private readonly IContactServices _contactServices;

        #region Public Constructor

        public ContactController(IContactServices contactServices)
        {
            _contactServices = contactServices;
        }

        #endregion
        // GET: api/Contact
        public HttpResponseMessage Get()
        {
            HttpRequestHeaders headers = Request.Headers;
            bool includeArchived = false;

            if (headers.Contains("includeArchived"))
            {
                includeArchived = bool.Parse(headers.GetValues("includeArchived").First());
            }
            var contact = _contactServices.GetAll(includeArchived);
            var contactEntities = contact?.ToList();

            return contactEntities != null && contactEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, contactEntities)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contacts not found");

        }

        // GET: api/Contact/5
        public HttpResponseMessage Get(int id)
        {
            var contact = _contactServices.GetById(id);
            return contact != null
                ? Request.CreateResponse(HttpStatusCode.OK, contact)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Contact found for this id");
        }

        // POST: api/Contact
        public HttpResponseMessage Post([FromBody]ContactModel contact)
        {
            int contactId = _contactServices.Create(contact);
            return contactId > 0
                    ? Request.CreateResponse(HttpStatusCode.OK,contactId)
                    : Request.CreateResponse(HttpStatusCode.Conflict, "Contact with this email exists.");
        }

        // PUT: api/Contact/5
        public bool Put(int id, [FromBody]ContactModel contact)
        {
            return id > 0 && _contactServices.Update(id, contact);
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
            return id > 0 && _contactServices.Delete(id, hardDelete);
        }
    }
}
