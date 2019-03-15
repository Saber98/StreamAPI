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
    public class CustomerController : ApiController
    {
        private readonly ICustomerServices _customerServices;

        #region Public Constructor

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        #endregion
        // GET: api/Customer
        public HttpResponseMessage Get()
        {
            HttpRequestHeaders headers = Request.Headers;
            bool includeArchived = false;

            if (headers.Contains("includeArchived"))
            {
                includeArchived = bool.Parse(headers.GetValues("includeArchived").First());
            }
            var customer = _customerServices.GetAll(includeArchived);
            var customerEntities = customer?.ToList();

            return customerEntities != null && customerEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, customerEntities)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Customers not found");

        }

        // GET: api/Customer/5
        public HttpResponseMessage Get(int id)
        {
            var customer = _customerServices.GetById(id);
            return customer != null
                ? Request.CreateResponse(HttpStatusCode.OK, customer)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Customer found for this id");
        }

        // POST: api/Customer
        public HttpResponseMessage Post([FromBody]CustomerModel customer)
        {
            int customerId = _customerServices.Create(customer);
            return customerId > 0
                ? Request.CreateResponse(HttpStatusCode.OK, customerId)
                : Request.CreateResponse(HttpStatusCode.Conflict, "Customer with this name exists.");
        }

        // PUT: api/Customer/5
        public bool Put(int id, [FromBody]CustomerModel customer)
        {
            return id > 0 && _customerServices.Update(id, customer);
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
            return id > 0 && _customerServices.Delete(id, hardDelete);
        }
    }
}
