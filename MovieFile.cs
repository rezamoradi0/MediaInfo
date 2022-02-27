using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace MediaInfo
{
    public class MovieFile
    {
        //close Folder with BackSlash /
        public static string MovieFolder { get; set; }
        public static string OutPutMovieFolder { get; set; }
        
        public string MovieName { get; set; }
        public int MovieLength { get; set; }
        public int MovieFileLength { get; set; }
        public float MovieSize { get; set; }

        public List<SubtitleFile> Subtitles { get; set; }
        public SubtitleFile ChosedSubtitle=new SubtitleFile();
        public MovieFile() { 
        Subtitles = new List<SubtitleFile>();
           OutPutMovieFolder= "G:\\SubtitleBotPlugins\\OutPutFolder\\";
            MovieFolder = @"G:\SubtitleBotPlugins\OriginalMovies\";


        }
        public  string CreateOutPutPath() {
            string output = OutPutMovieFolder+ ReplaceName(MovieName);
            return output;


        }
        static string ReplaceName(string name) {

            string MovieName = name;
            MovieName = MovieName.Replace("[OkMovie]", "-DeltaMovieS");
            MovieName = MovieName.Replace("[OkMovie.xyz]", "-DeltaMovieS");
            MovieName = MovieName.Replace("OkMovie.xyz", "DeltaMovieS");
            MovieName = MovieName.Replace(".OkMovie", ".DeltaMovieS");
            MovieName = MovieName.Replace("OkMovie", ".DeltaMovieS");
            MovieName = MovieName.Replace("okMovie", ".DeltaMovieS");
            MovieName = MovieName.Replace("Okmovie", ".DeltaMovieS");
            MovieName = MovieName.Replace("okmovie", ".DeltaMovieS");
            MovieName = MovieName.Replace("[@archive_series]", "-DeltaMovieS");
            MovieName = MovieName.Replace("@Archive_series]", "-DeltaMovieS");
            MovieName = MovieName.Replace("@archive_series", "DeltaMovieS");
            MovieName = MovieName.Replace("@Archive_Series", "DeltaMovieS");
            MovieName = MovieName.Replace("@archive_Series", "DeltaMovieS");
            MovieName = MovieName.Replace(".archive_series", ".DeltaMovieS");
            MovieName = MovieName.Replace("archive_series", ".DeltaMovieS");

            // Mobo
            MovieName = MovieName.Replace("(1)", "");
            MovieName = MovieName.Replace("Delta", "MetaL");
            MovieName = MovieName.Replace(".per", "");
            MovieName = MovieName.Replace(".und", "");
            MovieName = MovieName.Replace("mobomovie", "");
            MovieName = MovieName.Replace(" ", ".");
            MovieName = MovieName.Replace("-MovieApp.OnGooglePlay", "");
            MovieName = MovieName.Replace("-MovieApp.OnGooglPlay", "");
            MovieName = MovieName.Replace(".-", ".");
            MovieName = MovieName.Replace(".MovieApp", "");
            MovieName = MovieName.Replace(".Movieapp", "");
            MovieName = MovieName.Replace("_.", ".");
            MovieName = MovieName.Replace("%20", "");
            MovieName = MovieName.Replace("_", ".");

            //DibaMovie
            MovieName = MovieName.Replace("(DibaMovie)", "");
            MovieName = MovieName.Replace("DibaMovie", "");
            MovieName = MovieName.Replace("dibamovie", "");
            MovieName = MovieName.Replace("Diba", "");
            MovieName = MovieName.Replace("diba", "");


            //topmoviez
            MovieName = Regex.Replace(MovieName, "TopMoviez", "", RegexOptions.IgnoreCase);
            MovieName = Regex.Replace(MovieName, "TopMoviez", "", RegexOptions.IgnoreCase);
            MovieName = Regex.Replace(MovieName, "TopMovies", "", RegexOptions.IgnoreCase);
            MovieName = Regex.Replace(MovieName, "TopMovie", "", RegexOptions.IgnoreCase);





            //seasons

            MovieName = MovieName.Replace("s01e", "S01E").Replace("s02e", "S02E").Replace("s03e", "S03E").Replace("s04e", "S04E").Replace("s05e", "S05E").Replace("s06e", "S06E").Replace("s07e", "S07E").Replace("s08e", "S08E").Replace("s09e", "S09E");
            MovieName = MovieName.Replace("s10e", "S10E").Replace("s11e", "S11E").Replace("s12e", "S12E").Replace("s13e", "S13E").Replace("s14e", "S14E").Replace("s15e", "S15E").Replace("s16e", "S16E").Replace("s17e", "S17E").Replace("s18e", "S18E").Replace("s19e", "S19E");
            MovieName = MovieName.Replace("s20e", "S20E").Replace("s21e", "S21E").Replace("s22e", "S22E").Replace("s23e", "S23E").Replace("s24e", "S24E").Replace("s25e", "S25E").Replace("s26e", "S26E").Replace("s27e", "S27E").Replace("s28e", "S28E").Replace("s29e", "S29E");



            MovieName = MovieName.Replace("psa", "PSA");
            MovieName = MovieName.Replace("rmt", "RMT");
            MovieName = MovieName.Replace("rmteam", "RMT"); MovieName = MovieName.Replace("SoftSub", "Sub");
            MovieName = Regex.Replace(MovieName, "DigiMoviez", "DeltaMovieS", RegexOptions.IgnoreCase);
            if (!MovieName.ToLower().Contains("Delta"))
            {
                MovieName = MovieName.Replace(".mkv", "-DeltaMovieS.mkv");

            }
            MovieName = MovieName.Replace("DeltaMovieS", "");
            MovieName = MovieName.Replace("DeltaMovies", "");
            MovieName = MovieName.Replace(".mkv", ".DeltaMovieS.mkv");
            MovieName = MovieName.Replace(".-.", ".");
            return MovieName;

        }
        public string CreateInPutPath()
        {
            string input = MovieFolder + MovieName;
            return input;


        }
        public SubtitleFile GetBestSubtitle() {
            if (Subtitles.Count == 0)
            {
                Console.WriteLine("No Subtitle For : {0}", MovieName);
                Console.ReadLine();
                return null;
            }
            else if (Subtitles.Count == 1)
            {
                return Subtitles[0];
            }
            else { 
          SubtitleFile subtitleFile = new SubtitleFile();
                foreach (var Sub in Subtitles)
                {
                    if (Sub.Format==".srt"&&Sub.Languge=="per")
                    {
                        subtitleFile=Sub;
                        ChosedSubtitle = Sub;
                        return Sub;
                        break;
                    }else if (Sub.Format == ".srt" && Sub.Languge != "eng")
                    {
                        if (subtitleFile.Languge!= "per")
                        {
                            subtitleFile = Sub;
                            ChosedSubtitle = Sub;
                            return Sub;
                            break;
                        }
                      
                    }

                }
                ChosedSubtitle = subtitleFile;
                return subtitleFile;

            }
        }
    }
}
