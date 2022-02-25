using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaInfo
{
    internal class MovieFile
    {
        //close Folder with BackSlash /
        public static string MovieFolder { get; set; }  
        public string MovieName { get; set; }
        public int MovieLength { get; set; }
        public int MovieFileLength { get; set; }
        public float MovieSize { get; set; }

        public List<SubtitleFile> Subtitles { get; set; }
        public MovieFile() { 
        Subtitles = new List<SubtitleFile>();

        }
    }
}
