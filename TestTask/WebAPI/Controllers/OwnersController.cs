using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class OwnersController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetOwners()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            OwnersTable ownersTable = new OwnersTable();
            result.Content = new StringContent(JsonConvert.SerializeObject(ownersTable.Retrieve()));
            return result;
        }

        [HttpGet]
        public HttpResponseMessage GetOwners(int ID)
        {
            OwnersTable ownersTable = new OwnersTable();
            var owner = ownersTable.Retrieve(ID);
            if (owner != null)
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject(owner));
                return result;
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);  
        }

        [HttpPost]
        public HttpResponseMessage AddOwner([FromBody]OwnerModel newOwner)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            OwnersTable ownersTable = new OwnersTable();
            result.Content = new StringContent(JsonConvert.SerializeObject(ownersTable.Create(newOwner)));
            return result;
        }

        [HttpDelete]
        public HttpResponseMessage DeleteOwner(int ID)
        {
            OwnersTable ownersTable = new OwnersTable();
            if (ownersTable.Delete(ID))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
