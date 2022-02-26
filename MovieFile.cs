using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public MovieFile() { 
        Subtitles = new List<SubtitleFile>();
           OutPutMovieFolder= @"G:\SubtitleBotPlugins\OutPutFolder\";
            MovieFolder = @"G:\SubtitleBotPlugins\TestRoom\";


        }
        public  string CreateOutPutPath() {
            string output = OutPutMovieFolder +MovieName;
            return output;


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
                        return Sub;
                        break;
                    }else if (Sub.Format == ".srt" && Sub.Languge != "eng")
                    {
                        if (subtitleFile.Languge!= "per")
                        {
                            subtitleFile = Sub;
                            return Sub;
                            break;
                        }
                      
                    }

                }
                return subtitleFile;

            }
        }
    }
}
