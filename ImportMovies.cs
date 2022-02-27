using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MediaInfo
{
    internal class ImportMovies
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


    }
}
