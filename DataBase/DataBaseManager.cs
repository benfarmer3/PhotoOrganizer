using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Data.SQLite;

namespace PhotoOrganiser.DataBase
{
    internal static class DataBaseManager
    {
        private static string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string dbDirectory = Path.Combine(appDataPath, "BenFarmer\\PhotoOrganiser");
        private static string dbPath = Path.Combine(dbDirectory, "photos.sqlite");

        //SQL Commands
        private static string sqlCreateString = "create table highscores (name varchar(20), score int)";
        private static string sqlInsertString = "INSERT INTO TableName(ColumnName) SELECT '" + value + "' WHERE NOT EXISTS ( SELECT ColumnName from TableName WHERE Name = '" + value + "')";



        public static void CreateDb()
        {
            if (!DoesDbExist())
            {
                Directory.CreateDirectory(dbDirectory);
                SQLiteConnection.CreateFile(dbPath);
                SQLiteConnection dbConnection = new SQLiteConnection($"Data Source={dbPath};Version=3;");
                dbConnection.Open();

                SQLiteCommand createCommand = new SQLiteCommand(sqlCreateString, dbConnection);
                createCommand.ExecuteNonQuery();

            }

        }

        private static bool DoesDbExist()
        {
            return File.Exists(dbPath);
        }



    }
}
