using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public static class Database
    {
        static SQLiteConnection connection;
        static SQLiteCommand command;

        static string sqlCommand_createTableOwners = "create table if not exists Owners (ID integer PRIMARY KEY autoincrement, Name TEXT, PetsCount int);";
        
        static string databaseName = HttpContext.Current.Server.MapPath("~/App_Data/") + @".\OwnersAndPets.db";

        #region constructor
        // Check database file (if file does not exist - create file) and initializing variables connection and command
        static Database()
        {
            CheckDatabase();
            connection = new SQLiteConnection(string.Format("DataSource =" + databaseName + ";"));
            command = new SQLiteCommand(connection);
        }
        #endregion

        #region CheckDatabase
        //Check that database file exist, if no - create new database file
        private static void CheckDatabase()
        {
            if (!File.Exists(databaseName))
            {
                SQLiteConnection.CreateFile(databaseName);
                ExecuteSQLCommand(sqlCommand_createTableOwners);
            }
        }
        #endregion

        #region ExecuteSQLCommand
        //open connection, execute sql command and close connection
        public static bool ExecuteSQLCommand(string SQLCommand)
        {
            try
            {
                command.CommandText = SQLCommand;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region ExecuteSQLCommand<T>
        //Execute SQL command throw reader and return data
        public static IEnumerable ExecuteSQLCommand<T>(string SQLCommand)
        {
            command.CommandText = SQLCommand;
            connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();
            T checkType = default(T);
            connection.Close();
            if (checkType is OwnerModel)
            {
                var result = ReadList(reader, ownerGenerator);
                connection.Close();
                return result;
            }
            else
            {
                var result = ReadList(reader, ownerHasGenerator);
                connection.Close();
                return result;
            }
        }
        #endregion

        #region ReadList<T>
        //Transform data from reader to Model
        public static IEnumerable<T> ReadList<T>(this IDataReader reader, Func<IDataRecord, T> generator)
        {
            while (reader.Read())
                yield return generator(reader);
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

        #region ownerHasGenerator
        //behavior of ownerHasGenerator
        static Func<IDataRecord, OwnerHasModel> ownerHasGenerator = x => new OwnerHasModel
        {
            ID = x.GetInt32(0),
            Name = x.GetString(1)
        };
        #endregion
    }
}