using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Data.SQLite;

namespace PhotoOrganiser.DataBase
{
    public class DataBaseManager
    {
        private static string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string dbDirectory = Path.Combine(appDataPath, "BenFarmer\\PhotoOrganiser");
        private static string dbPath = Path.Combine(dbDirectory, "photos.sqlite");

        //SQL Commands
        private static string sqlCreateString = "CREATE TABLE Photos (Name text NOT NULL, Extension text NOT NULL,Path text NOT NULL, Duplicate bit NOT NULL);";
        private static string sqlInsertString = "insert into Photos ([Name], [Extension], [Path], [Duplicate]) values(@Name,@Extension, @Path, @Duplicate)";
        
        private static SQLiteConnection connection;

        public static void CreateDb()
        {
            if (!DoesDbExist())
            {
                Directory.CreateDirectory(dbDirectory);
                SQLiteConnection.CreateFile(dbPath);
                connection = new SQLiteConnection($"Data Source={dbPath};Version=3;");
                connection.Open();
                SQLiteCommand createCommand = new SQLiteCommand(sqlCreateString, connection);
                createCommand.ExecuteNonQuery();
            }
            else
            {
                connection = new SQLiteConnection($"Data Source={dbPath};Version=3;");
                connection.Open();
            }
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection($"Data Source={dbPath};Version=3;");
        }

        public static void AddImage(string hash, string name, string extension, string path, bool duplicate)
        {
            

            using (SQLiteCommand cmd = new SQLiteCommand(sqlInsertString, connection))
            {
                //cmd.Parameters.Add("@Hash", System.Data.DbType.String).Value = hash;
                cmd.Parameters.Add("@Name", System.Data.DbType.String).Value = name;
                cmd.Parameters.Add("@Extension", System.Data.DbType.String).Value = extension;
                cmd.Parameters.Add("@Path", System.Data.DbType.String).Value = path;
                cmd.Parameters.Add("@Duplicate", System.Data.DbType.Boolean).Value = duplicate;

                int rowsAffected = cmd.ExecuteNonQuery();
                if(rowsAffected > 0)
                {
                    return;
                }
                // Error Writing
            }

        }

        private static bool DoesDbExist()
        {
            return File.Exists(dbPath);
        }



    }
}
