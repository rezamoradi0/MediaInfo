using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace MediaInfo
{
    static class UploadManager
    {
        static string UploadPathUrl = "ftp://asiatech.parsaspace.com:21/";
        static string UploadPathHttp = "http://asiatech.parsaspace.com/";
        static string FtpUser = "delta1.parsaspace.com";
        static string FtpPass = "dodota5tanist";

        public static bool CreateUploadBatch(this List<MovieFile> Movies) {
            foreach (var Movie in Movies)
            {
               
                Console.WriteLine("Uploaded : " + Movie.CreateUpload()); 
            }
        return true;
        }
        public static bool CreateUpload(this MovieFile file) {
            if (file.GetFileType()== "Movie")
            {
             string MovieuploadPath=  CreateMoviePath(file);
                string M = UploadFile(file,MovieFile.MovieFolder + file.MovieName, UploadPathUrl+"Movies/"+ MovieuploadPath + file.MovieName);
             Console.WriteLine( M);


            }
            if (file.GetFileType() == "Serial")
            {
                string EpisodeuploadPath =CreateSeriesPath(file);
                string M = UploadFile(file,MovieFile.MovieFolder + file.MovieName, UploadPathUrl + EpisodeuploadPath + file.MovieName);
                Console.WriteLine(M);


            }




            return true;
        }


        public static string CreateSeriesPath(MovieFile Episode)
        {
           
            string FirstAlphabet= Episode.MovieName.Substring(0,1).ToUpper();
            string SerialSeason="S"+ Episode.GetSeasionNumber();
            int IndexOfSeason = Episode.MovieName.IndexOf(SerialSeason);
            string SerialName = Episode.MovieName.Substring(0, IndexOfSeason - 1);
            string SerialQuality=Episode.GetQuality();
            string[] path=new string[4] { FirstAlphabet,SerialName, SerialSeason, SerialQuality };
            string pathUrl = "Series/";
            foreach (var item in path)
            {
                pathUrl += item+"/";
                WebRequest webPathRequest = WebRequest.Create(UploadPathUrl + pathUrl);
                webPathRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                webPathRequest.Credentials = new NetworkCredential(FtpUser, FtpPass);
                try
                {
                    using (var resp = (FtpWebResponse)webPathRequest.GetResponse())
                    {


                        Console.WriteLine(resp.StatusCode.ToString());
                       
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Cant Create Path ! Mybe Existed");
                   
                }
            }
            return pathUrl;

        }
            public static string CreateMoviePath(MovieFile Movie) {


            string directory = Movie.MovieName.Substring(0,1).ToUpper();
            WebRequest webPathRequest1 = WebRequest.Create(UploadPathUrl + directory + "/");
            webPathRequest1.Method = WebRequestMethods.Ftp.MakeDirectory;
            webPathRequest1.Credentials = new NetworkCredential(FtpUser, FtpPass);

            try
            {
                using (var resp = (FtpWebResponse)webPathRequest1.GetResponse())
                {


                    Console.WriteLine(resp.StatusCode.ToString());
                 
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Cant Create Path ! Mybe Existed");
            }

            directory +="/"+Movie.MovieName.Substring(0, Movie.MovieName.IndexOf(Movie.GetYear().ToString())+4)+"/";
           
            Console.WriteLine(directory);

            WebRequest webPathRequest2 = WebRequest.Create(UploadPathUrl+ directory);
            webPathRequest2.Method = WebRequestMethods.Ftp.MakeDirectory;
            webPathRequest2.Credentials = new NetworkCredential(FtpUser, FtpPass);
            try
            {
                using (var resp = (FtpWebResponse)webPathRequest2.GetResponse())
                {


                    Console.WriteLine(resp.StatusCode.ToString());
                    return directory;
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Cant Create Path ! Mybe Existed");
                return directory;
            }

            return "Error";
        }

        public static string UploadFile(MovieFile file,string filePath, string UploadPath) {

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(UploadPath);
            request.Credentials = new NetworkCredential(FtpUser, FtpPass);

            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream fileStream = File.OpenRead(filePath))
            using (Stream ftpStream = request.GetRequestStream())
            {
             
                byte[] buffer = new byte[10240];
                int read;
                string temp = "";
                while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ftpStream.Write(buffer, 0, read);
                    float uploadPercent = (fileStream.Position*100) / fileStream.Length;
                    string Progress = uploadPercent.ToString();
                   
                    if ( uploadPercent%10==0)
                    {
                       
                        if (temp!=Progress)
                        {
                            Console.Clear();    
                            Console.WriteLine("Uploaded : %" +Progress);
                            temp = Progress;
                        }
                       
                        
                    }
                  
                }
                file.MovieUploadPath = UploadPath.Replace(UploadPathUrl, UploadPathHttp);
            }
            return filePath;

        }
    }
}
