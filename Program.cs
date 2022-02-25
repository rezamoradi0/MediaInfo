using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace MediaInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {


            List<MovieFile> files = new List<MovieFile>();
            MovieFile movie0 = new MovieFile();
            MovieFile movie1 = new MovieFile();
            MovieFile movie2 = new MovieFile();
            movie0.MovieName = "Arcane.S01E01.720p.10bit.WEB-DL.x265.SoftSub.DigiMoviez.mkv";
            movie1.MovieName = "Arcane.S01E02.720p.10bit.WEB-DL.x265.SoftSub.DigiMoviez.mkv";
            movie2.MovieName = "Arcane.S01E01.720p.10bit.WEB-DL.x265.SoftSub.DigiMoviez.per.mkv";
            MovieFile.MovieFolder = @"G:\SubtitleBotPlugins\TestRoom\";
            files.Add(movie0); files.Add(movie1); files.Add(movie2);
            SubtitleEdit.SubRipBatch(files);
            foreach (var item in files)
            {
                foreach (var subtile in item.Subtitles)
                {
                    Console.WriteLine(subtile.SubtitlePath);
                }
            }
            Console.ReadLine();


            // SQLiteConnection sqlCon=SqlSide.CreateConnection();

            // ساخت دیتابیس و وارد کردن زیرنویس ها و اسم فیلم ها  به دیتابیس
            //   SqlSide.CreateTable(sqlCon);
            //  string insertCmd= "INSERT INTO Subtitle  (Duration, MovieName, SubtitlePath, MovieYear) VALUES(201334, 'God Of War' , 'C:\\Users\\DeltaGroup\\Desktop\\scream-2022_farsi_persian-2698572\\Scream.2022.720p.WEBRip.800MB.x264-GalaxyRG-[Farsi] - Copy.DeltaMovieS.srt',2013);";
            //  SqlSide.InsertData(sqlCon, insertCmd);


            /*
             * برای مدیا اینفو و دیدن مدت زمان فیلم
            MovieFile movieFile = new MovieFile();
            MovieFile.MovieFolder = @"G:\SubtitleBotPlugins\MediaInfo.Windows.x64\test\";
            movieFile.MovieName = "test.mp4";


            Console.Write(MediaInformation.GetDuration(movieFile));
            Console.ReadLine();
            */
        }
    }
}
