using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class PetsTables
    {

        string ownerName;

        public PetsTables(string ownerName)
        {
            this.ownerName = ownerName;
        }

        public OwnerHasRequest Create(OwnerHasRequest pet)
        {
            string SQLCommand = "INSERT INTO {0}_Pets (Name) VALUES ('{1}');";
            Database.ExecuteSQLCommand(String.Format(SQLCommand, ownerName, pet.Name));
            SQLCommand = "SELECT IDENT_CURRENT('Owners')";
            return Database.ExecuteSQLCommandWithReader<OwnerHasRequest>(SQLCommand, ownerHasGenerator).FirstOrDefault();
        }

        public bool Delete(int ID)
        {
            string SQLCommand = "SELECT * FROM {0}_Pets WHERE ID={1};";
            var result = Database.ExecuteSQLCommandWithReader<OwnerHasRequest>(String.Format(SQLCommand, ownerName, ID), ownerHasGenerator);
            if (result.Count != 0)
            {
                SQLCommand = "DELETE FROM {0} WHERE ID={1}";
                Database.ExecuteSQLCommand(String.Format(SQLCommand, ownerName, ID));
                return true;
            }
            return false;
        }

        public List<OwnerHasRequest> Retrieve()
        {
            string SQLCommand = "SELECT * FROM {0}_Pets;";
            return Database.ExecuteSQLCommandWithReader<OwnerHasRequest>(String.Format(SQLCommand, ownerName), ownerHasGenerator);
        }

        public OwnerHasRequest Retrieve(int ID)
        {
            throw new NotImplementedException();
        }

        public OwnerHasRequest Update(OwnerHasRequest obj)
        {
            throw new NotImplementedException();
        }

        #region ownerHasGenerator
        //behavior of ownerHasGenerator
        static Func<IDataRecord, OwnerHasRequest> ownerHasGenerator = x => new OwnerHasRequest
        {
            ID = x.GetInt32(0),
            Name = x.GetString(1)
        };
        #endregion
    }
}