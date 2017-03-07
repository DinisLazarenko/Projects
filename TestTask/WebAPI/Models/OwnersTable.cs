using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WebAPI.Models
{

    public class OwnersTable : ICRUD<OwnerModel>
    {

        #region OwnerModel Create(OwnerModel owner)
        //Create new owner record in DB, and return new record with ID
        public OwnerModel Create(OwnerModel owner)
        {
            if (Database.CreateTable(owner.Name))
            { 
                string SQLCommand = "INSERT INTO Owners (Name, PetsCount) VALUES ('{0}', 0);";
                Database.ExecuteSQLCommand(String.Format(SQLCommand, owner.Name));
                SQLCommand = "SELECT IDENT_CURRENT('Owners')";
                return Database.ExecuteSQLCommandWithReader<OwnerModel>(SQLCommand, ownerGenerator).FirstOrDefault();
            }
            else
            {
                return new OwnerModel();
            }
        }
        #endregion

        #region bool Delete(int ID)
        //Delete owner additional tables and owner record by ID
        public bool Delete(int ID)
        {
            string SQLCommand = "SELECT * FROM Owners WHERE ID={0};";
            var result = Database.ExecuteSQLCommandWithReader<OwnerModel>(String.Format(SQLCommand, ID), ownerGenerator);
            if (result.Count != 0)
            {
                SQLCommand = "DROP TABLE {0}_Pets;";
                Database.ExecuteSQLCommand(String.Format(SQLCommand, result.First().Name));
                SQLCommand = "DELETE FROM Owners WHERE ID={0}";
                Database.ExecuteSQLCommand(String.Format(SQLCommand, ID));
                return true;
            }
            return false;
        }
        #endregion

        #region List<OwnerModel> Retrieve()
        //Get all owners from DB
        public List<OwnerModel> Retrieve()
        {
            string SQLCommand = "SELECT * FROM Owners;";
            return Database.ExecuteSQLCommandWithReader<OwnerModel>(SQLCommand, ownerGenerator);
        }
        #endregion

        #region OwnerModel Retrieve(int ID)
        //Get single owner from DB by ID
        public OwnerModel Retrieve(int ID)
        {
            string SQLCommand = "SELECT * FROM Owners WHERE ID={0};";
            return Database.ExecuteSQLCommandWithReader<OwnerModel>(String.Format(SQLCommand, ID), ownerGenerator).FirstOrDefault();
        }
        #endregion

        #region OwnerModel Update(OwnerModel owner)
        //Update owner record in DB
        public OwnerModel Update(OwnerModel owner)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ownerGenerator
        //behavior of ownerGenerator
        static Func<IDataRecord, OwnerModel> ownerGenerator = x => new OwnerModel
        {
            ID = x.GetInt32(0),
            Name = x.GetString(1),
            PetsCount = x.GetInt32(2)
        };
        #endregion
    }
}