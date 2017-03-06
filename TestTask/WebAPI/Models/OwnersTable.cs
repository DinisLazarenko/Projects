using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{

    public class OwnersTable : ICRUD<OwnerModel>
    {

        string sqlCommand_createTablesOwnerHas;

        public OwnerModel Create(OwnerModel owner)
        {
            return new OwnerModel();
        }

        public void Delete(OwnerModel obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable Retrieve()
        {
            return Database.ExecuteSQLCommand<OwnerModel>("select * from Owners;");
        }

        public OwnerModel Retrieve(int ID)
        {
            return null;
        }

        public OwnerModel Update(OwnerModel obj)
        {
            throw new NotImplementedException();
        }
    }
}