using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaInfo
{
    static class SubtitleEdit
    {
        static string SubtitleEditPath = @"C:\Program Files\Subtitle Edit";
        public static bool SubRipBatch(this List<MovieFile> listOFMovies) {
            string MoviesPath = "";
            foreach (var Movie in listOFMovies)
            {
                MoviesPath += MovieFile.MovieFolder + Movie.MovieName+",";
            }
            MoviesPath= MoviesPath.Substring(0, MoviesPath.Length-1);
            string Command = $"SubtitleEdit /conver {MoviesPath} Subrip";
           string Result= CMD.RunCMD(Command,SubtitleEditPath);
            if (Result.Contains("file(s) converted")) 
            {

            }
            return false;
        }

       
    }
}
