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
            Database.ExecuteSQLCommand(String.Format("INSERT INTO owners (Name, PetsCount) VALUES ('{0}', 0)", owner.Name));

            IEnumerable result = Database.ExecuteSQLCommand<OwnerModel>("SELECT * FROM Owners WHERE ID = (SELECT MAX(ID) FROM Owners);");
            List<OwnerModel> list = new List<OwnerModel>();
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