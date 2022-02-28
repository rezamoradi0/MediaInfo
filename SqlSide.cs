using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace MediaInfo
{
    internal static class SqlSide
    {

      public  static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source= database.db; Version = 3; New = True; Compress = True; ");
           // Open the connection:
         try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }
        public static bool InsertMovieBatch(this List<MovieFile> Movies)
        {

            foreach (var Movie in Movies)
            {
                Movie.InsertMovie();
            }
            return true;
                    
        }
            public static bool InsertMovie(this MovieFile Movie)
        {
            SQLiteConnection sqlCon = SqlSide.CreateConnection();
            
            string insertCmd = "INSERT INTO Subtitle  (Duration, MovieName, SubtitlePath, MovieYear, FileType, Seasion, Episode, MovieFullName) VALUES(DURATION, 'MOVIENAME' , 'SUBTITLEPATH',YEAR ,'FILETYPE' ,'SEASION' ,'EPISODE' ,'MOVIEFULLNAME')";
            insertCmd = insertCmd.Replace("DURATION", Movie.MovieFileLength.ToString());
            insertCmd = insertCmd.Replace("MOVIENAME", Movie.MovieNameOrginal());
            insertCmd = insertCmd.Replace("SUBTITLEPATH", Movie.GetBestSubtitle().SubtitlePath);
            insertCmd = insertCmd.Replace("MOVIEFULLNAME", Movie.MovieName);
            insertCmd = insertCmd.Replace("YEAR", Movie.GetYear().ToString());
            insertCmd = insertCmd.Replace("FILETYPE", Movie.GetFileType());
            insertCmd = insertCmd.Replace("SEASION", Convert.ToInt32(Movie.GetSeasionNumber()).ToString());
            insertCmd = insertCmd.Replace("EPISODE", Movie.GetEpisodeNumber());

            SqlSide.InsertData(sqlCon, insertCmd);
            sqlCon.Close();
            return true;


        }
        public static void CreateTable(SQLiteConnection conn)
        {

            SQLiteCommand sqlite_cmd;
           

            string Createsql ="Create TABLE Subtitle  (MovieName VARCHAR(255), MovieYear Int, Duration Int, SubtitlePath VARCHAR(255));";
            //"ALTER TABLE Customers Email varchar(255);"
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
         

        }

      public  static void InsertData(SQLiteConnection conn,string SqlCommand)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = SqlCommand;
           sqlite_cmd.ExecuteNonQuery();
         




        }

        public static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }
       
    }

}
