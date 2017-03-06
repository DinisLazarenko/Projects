using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class OwnerModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PetsCount { get; set; }
    }
}