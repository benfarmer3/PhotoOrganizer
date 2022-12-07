using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Data.SQLite;

namespace PhotoOrganiser.DataBase
{
    public class DataBaseManager
    {
        private string appDataPath;
        private string dbDirectory;
        private string dbPath;

        //SQL Commands
        private string sqlCreateString = "CREATE TABLE Photos (Hash text NOT NULL PRIMARY KEY, Name text NOT NULL, Extension text NOT NULL,Path text NOT NULL);";
        private string sqlInsertString = "insert into Photos ([Hash], [Name], [Extension], [Path]) values(@Hash,@Name,@Extension, @Path)";


        public DataBaseManager()
        {
            appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            dbDirectory = Path.Combine(appDataPath, "BenFarmer\\PhotoOrganiser");
            dbPath = Path.Combine(dbDirectory, "photos.sqlite");

        }

        public void CreateDb()
        {
            if (!DoesDbExist())
            {
                Directory.CreateDirectory(dbDirectory);
                SQLiteConnection.CreateFile(dbPath);

                var connection = GetConnection();
                SQLiteCommand createCommand = new SQLiteCommand(sqlCreateString, connection);
                connection.Open();
                createCommand.ExecuteNonQuery();

            }

        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection($"Data Source={dbPath};Version=3;");
        }

        public void AddImage(string hash, string name, string extension, string path)
        {
            var connection = GetConnection();
            connection.Open();

            using (SQLiteCommand cmd = new SQLiteCommand(sqlInsertString, connection))
            {
                cmd.Parameters.Add("@Hash", System.Data.DbType.String).Value = hash;
                cmd.Parameters.Add("@Name", System.Data.DbType.String).Value = name;
                cmd.Parameters.Add("@Extension", System.Data.DbType.String).Value = extension;
                cmd.Parameters.Add("@Path", System.Data.DbType.String).Value = path;

                int rowsAffected = cmd.ExecuteNonQuery();
                if(rowsAffected > 0)
                {
                    return;
                }
                // Error Writing
            }

        }

        private bool DoesDbExist()
        {
            return File.Exists(dbPath);
        }



    }
}
