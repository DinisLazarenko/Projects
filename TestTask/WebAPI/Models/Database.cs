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
        
        static string databaseName = HttpContext.Current.Server.MapPath("~/App_Data/") + "TestTaskDB.db";

        #region constructor
        // Check database file (if file does not exist - create file) and initializing variables connection and command
        static Database()
        {
            CheckDatabase();
            connection = new SQLiteConnection(string.Format("DataSource =" + databaseName + ";"));
            command = new SQLiteCommand(connection);
        }
        #endregion

        #region void CheckDatabase()
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

        #region bool CreateTable(string tableName)
        //Create additional tables to new owner
        public static bool CreateTable(string tableName)
        {
            string SQLCommand = "CREATE TABLE IF NOT EXISTS {0} (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT);";
            if (ExecuteSQLCommand(String.Format(SQLCommand, tableName + "_Pets")))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region bool ExecuteSQLCommand(string SQLCommand)
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

        #region List<T> ExecuteSQLCommand<T>(string SQLCommand, Func<IDataRecord, T> generator)
        //Execute SQL command throw reader and return data
        public static List<T> ExecuteSQLCommandWithReader<T>(string SQLCommand, Func<IDataRecord, T> generator)
        {
            command.CommandText = SQLCommand;
            connection.Open();
            SQLiteDataReader reader = command.ExecuteReader();
            List<T> list = new List<T>();
            while (reader.Read())
                 list.Add(generator(reader));
            reader.Close();
            connection.Close();
            return list;
        }
        #endregion
    }
}