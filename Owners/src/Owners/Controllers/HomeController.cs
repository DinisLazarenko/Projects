using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Owners.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net;

namespace Owners.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {

        private AppDbContext ctx = new AppDbContext();

        // GET api/home
        [HttpGet]
        public IEnumerable<Models.Owners> Get()
        {
            //AddOwner();
            var result = ctx.Owners.Include(o => o.Pets).ToList();
            List<Models.Owners> owners = new List<Models.Owners>();
            foreach (Owner owner in result)
            {
                Models.Owners ownerAndPetsCount = new Models.Owners
                {
                    Id = owner.Id,
                    Name = owner.Name,
                    PetsCount = owner.Pets.Count
                };
                owners.Add(ownerAndPetsCount);
            }
            return owners;
        }

        // GET api/home/5
        [HttpGet("{id}")]
        public Owner Get(int id)
        {
            return ctx.Owners.Include(o => o.Pets).Where(o => o.Id == id).First();
        }

        // POST api/home
        [HttpPost]
        public void Post([FromBody]string value)
        {
            AddOwner();
        }

        // PUT api/home/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/home/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Owner owner = new Owner { Id = id };
            ctx.Owners.Attach(owner);
            ctx.Owners.Remove(owner);
            ctx.SaveChanges();
        }

        public void AddOwner() {
            int ownersCount = ctx.Owners.Count();
            var owner = new Owner
            {
                Name = "owner" + ownersCount.ToString()
            };

            ctx.Owners.Add(owner);
            ctx.SaveChanges();

            var Pets = new List<string>();
            for (int i = 0; i < 2; i++)
            {
                Pets.Add("Pet" + i.ToString());
            }

            foreach (var name in Pets)
            {
                var pet = new Pet
                {
                    Name = name,
                    OwnerId = owner.Id
                };

                ctx.Pets.Add(pet);
                ctx.SaveChanges();
            }
        }
    }
}
