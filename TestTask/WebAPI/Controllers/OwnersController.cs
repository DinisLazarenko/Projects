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
        public HttpResponseMessage GetOwners()
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            OwnersTable ownersTable = new OwnersTable();
            result.Content = new StringContent(JsonConvert.SerializeObject(ownersTable.Retrieve()));
            return result;
        }
    }
}
