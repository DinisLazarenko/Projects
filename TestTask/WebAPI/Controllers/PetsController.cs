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
    public class PetsController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage GetPets(string owner)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            PetsTables petsTables = new PetsTables(owner);
            result.Content = new StringContent(JsonConvert.SerializeObject(petsTables.Retrieve()));
            return result;
        }

        [HttpPost]
        public HttpResponseMessage AddPet([FromBody]OwnerHasRequest newPet)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            PetsTables petsTables = new PetsTables(newPet.OwnerName);
            result.Content = new StringContent(JsonConvert.SerializeObject(petsTables.Create(newPet)));
            return result;
        }

        [HttpDelete]
        public HttpResponseMessage DeletePet(string owner, int id)
        {
            PetsTables petsTables = new PetsTables(owner);
            if (petsTables.Delete(id))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
