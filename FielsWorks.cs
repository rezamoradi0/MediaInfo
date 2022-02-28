using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MediaInfo
{
  public  static class FielsWorks
    {
     
        
        public static List<MovieFile> GetMovies() {
            MovieFile movieFile = new MovieFile();
            List<MovieFile> movies = new List<MovieFile>();
            string Moviesfolder = MovieFile.MovieFolder;
            string[] Movies_Paht = Directory.GetFiles(Moviesfolder, "*.mkv");
            foreach (var item in Movies_Paht)
            {
                MovieFile thisMovie = new MovieFile();
                int deleteIndex = MovieFile.MovieFolder.Length;
                thisMovie.MovieName=item.Remove(0,deleteIndex);
                Console.WriteLine(thisMovie.MovieName);
              //  Console.ReadLine();
                movies.Add(thisMovie);
            }
         return movies;

        }
        public static bool MoveSubtitlesBatch(this List<MovieFile> Movies) {

            foreach (var Movie in Movies)
            {
                Movie.MoveSubtitle();
            }
        return true;

        }
      public  static bool MoveSubtitle(this MovieFile movie) {
            string SubPath = movie.GetBestSubtitle().SubtitlePath;
            string SubSavePath = MovieFile.SubtitleSaveFolder + movie.GetBestSubtitle().GetNameOfSubtitle();
            try
            {
                File.Move(SubPath, SubSavePath);
            }
            catch (Exception)
            {

                File.Move(SubPath, SubSavePath.Replace(".srt",".copy.srt"));
            }

            movie.GetBestSubtitle().SubtitlePath = SubSavePath;
            return true;

        }

        public static void DeleteAllFiles() {
            DirectoryInfo di = new DirectoryInfo(MovieFile.MovieFolder);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

        }
    }
}
