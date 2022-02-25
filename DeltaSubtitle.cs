using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Chilkat;

namespace MediaInfo
{
    internal class DeltaSubtitle
    {

        const string DeltaSubPath = @"G:\SubtitleBotPlugins\DeltaSub.srt";

        /// <summary>
        /// زیرنویس را به این متد بدید و زیرنویس به صورت خودکار تغییر میکنه 
        /// </summary>
        /// <param name="Subtitle">فایل زیرنویس</param>
        /// <returns> Finshed را به عنوان پایان کار بازمیگرداند </returns>
        public string Subtitle(SubtitleFile Subtitle)
        {
            string[] subtitleLines = File.ReadAllLines(Subtitle.SubtitlePath);
            string[] EditedSubtitle = DeltaSub(subtitleLines);
            string Save_Path = Set_Path(Subtitle.SubtitlePath);
            Subtitle.SubtitlePath = Save_Path;


            string Result = writeSubAsync(EditedSubtitle, Save_Path);
            return Result;

        }
        public static string writeSubAsync(string[] Lines, string save_path)
        {

            File.WriteAllLines(save_path, Lines);
            UTF8TOBOM(save_path);
            return "Finshed";

        }
        public static string writeSubAsync(string AllText, string save_path)
        {

           
            File.WriteAllText(save_path, AllText);
            UTF8TOBOM(save_path);
            return "Finshed";

        }
        public string[] DeltaSub(string[] MovieSub) {
            string[] deltaMoviesSub = File.ReadAllLines(DeltaSubPath);

            string[] ForEditSub = MovieSub;
            ForEditSub = ReplaceSub(ForEditSub);
            string[] newSub = new string[deltaMoviesSub.Length + ForEditSub.Length + 5];
            int a = 0;
            foreach (var item in deltaMoviesSub)
            {
                newSub[a] = item;
                a++;
            }

            foreach (var item in ForEditSub)
            {
            
                newSub[a] = item;
                a++;
            }
            
            return newSub;
        }

        public string Set_Path(string filePath) {

            // این ساب نیم هست دیگه تغییر نمیتونم بدم از کد های قبلی هستش
            string SubtitleName = filePath.Replace(".srt", ".DeltaMovieS.srt");
            SubtitleName = SubtitleName.Replace("[OkMovie]", "-DeltaMovieS");
            SubtitleName = SubtitleName.Replace("[OkMovie.xyz]", "-DeltaMovieS");
            SubtitleName = SubtitleName.Replace("OkMovie.xyz", "DeltaMovieS");
            SubtitleName = SubtitleName.Replace(".OkMovie", ".DeltaMovieS");
            SubtitleName = SubtitleName.Replace("OkMovie", ".DeltaMovieS");
            SubtitleName = SubtitleName.Replace("okMovie", ".DeltaMovieS");
            SubtitleName = SubtitleName.Replace("Okmovie", ".DeltaMovieS");
            SubtitleName = SubtitleName.Replace("okmovie", ".DeltaMovieS");
            SubtitleName = SubtitleName.Replace("[@archive_series]", "-DeltaMovieS");
            SubtitleName = SubtitleName.Replace("@Archive_series]", "-DeltaMovieS");
            SubtitleName = SubtitleName.Replace("@archive_series", "DeltaMovieS");
            SubtitleName = SubtitleName.Replace("@Archive_Series", "DeltaMovieS");
            SubtitleName = SubtitleName.Replace("@archive_Series", "DeltaMovieS");
            SubtitleName = SubtitleName.Replace(".archive_series", ".DeltaMovieS");
            SubtitleName = SubtitleName.Replace("archive_series", ".DeltaMovieS");

            // Mobo
            SubtitleName = SubtitleName.Replace("(1)", "");
            SubtitleName = SubtitleName.Replace("MetaL", "Delta");
            SubtitleName = SubtitleName.Replace(".per", "");
            SubtitleName = SubtitleName.Replace(".und", "");
            SubtitleName = SubtitleName.Replace("mobomovie", "");
            SubtitleName = SubtitleName.Replace(" ", ".");
            SubtitleName = SubtitleName.Replace("-MovieApp.OnGooglePlay", "");
            SubtitleName = SubtitleName.Replace("-MovieApp.OnGooglPlay", "");
            SubtitleName = SubtitleName.Replace(".-", ".");
            SubtitleName = SubtitleName.Replace(".MovieApp", "");
            SubtitleName = SubtitleName.Replace(".Movieapp", "");
            SubtitleName = SubtitleName.Replace("_.", ".");
            SubtitleName = SubtitleName.Replace("%20", "");
            SubtitleName = SubtitleName.Replace("_", ".");

            //DibaMovie
            SubtitleName = SubtitleName.Replace("(DibaMovie)", "");
            SubtitleName = SubtitleName.Replace("DibaMovie", "");
            SubtitleName = SubtitleName.Replace("dibamovie", "");
            SubtitleName = SubtitleName.Replace("Diba", "");
            SubtitleName = SubtitleName.Replace("diba", "");


            //topmoviez
            SubtitleName = Regex.Replace(SubtitleName, "TopMoviez", "", RegexOptions.IgnoreCase);
            SubtitleName = Regex.Replace(SubtitleName, "TopMoviez", "", RegexOptions.IgnoreCase);
            SubtitleName = Regex.Replace(SubtitleName, "TopMovies", "", RegexOptions.IgnoreCase);
            SubtitleName = Regex.Replace(SubtitleName, "TopMovie", "", RegexOptions.IgnoreCase);





            //seasons

            SubtitleName = SubtitleName.Replace("s01e", "S01E").Replace("s02e", "S02E").Replace("s03e", "S03E").Replace("s04e", "S04E").Replace("s05e", "S05E").Replace("s06e", "S06E").Replace("s07e", "S07E").Replace("s08e", "S08E").Replace("s09e", "S09E");
            SubtitleName = SubtitleName.Replace("s10e", "S10E").Replace("s11e", "S11E").Replace("s12e", "S12E").Replace("s13e", "S13E").Replace("s14e", "S14E").Replace("s15e", "S15E").Replace("s16e", "S16E").Replace("s17e", "S17E").Replace("s18e", "S18E").Replace("s19e", "S19E");
            SubtitleName = SubtitleName.Replace("s20e", "S20E").Replace("s21e", "S21E").Replace("s22e", "S22E").Replace("s23e", "S23E").Replace("s24e", "S24E").Replace("s25e", "S25E").Replace("s26e", "S26E").Replace("s27e", "S27E").Replace("s28e", "S28E").Replace("s29e", "S29E");



            SubtitleName = SubtitleName.Replace("psa", "PSA");
            SubtitleName = SubtitleName.Replace("rmt", "RMT");
            SubtitleName = SubtitleName.Replace("rmteam", "RMT"); SubtitleName = SubtitleName.Replace("SoftSub", "Sub");
            SubtitleName = Regex.Replace(SubtitleName, "DigiMoviez", "Navar-App", RegexOptions.IgnoreCase);
            if (!SubtitleName.ToLower().Contains("Navar"))
            {
                SubtitleName = SubtitleName.Replace(".mkv", "-MetaLMovieS.mkv");

            }
            return SubtitleName;
        }
        /// <summary>
        /// این متد برای تبدیل فرمت UTF-8 به BOM هست
        /// </summary>
        /// <param name="SavedPath"> محل زیرنویس برای تبدیل</param>
        public  static void UTF8TOBOM(string SavedPath) {
            
            Charset charset = new Charset();
            charset.FromCharset = "UTF-8";
            charset.ToCharset = "BOM-UTF-8";
            bool a = charset.ConvertFile(SavedPath, SavedPath);
            Console.WriteLine(a+" File Converted To UTF8-DOM");
        }

        public static string[] ReplaceSub(string[] SubLines) {
            string[] newSub = new string[SubLines.Length];
            string FullSubString = "";

            foreach (var item in SubLines)
            {
                FullSubString += item + "~";
            }
         
                newSub = WordToReplace(FullSubString);
            
            return newSub;
        }
        static string[] WordToReplace(string Line) {
            string output1 = Line;
            output1 = output1.Replace("کافیست 1 بار از سایت ما دانلود کنید مطمعن باشید کاربر همیشگی ما میشوید", "با ما هم آهنگ فیلم ببینید ! متال موویز");
            output1 = output1.Replace("::. ارائه شده توسط وبسايت موبوفيلم .::", "دانلود شده از وبسایت متال موویز");
            output1 = output1.Replace("WwW.MoboMovie.net", "MetalMovieS");
            output1 = output1.Replace("مرجع دانلود فيلم و سريال با لينک مستقيم", "دانلود فیلم و سریال با زیرنویس چسبیده از متال موویز");
            output1 = output1.Replace("موبوفيلم", "متال موویز");
            output1 = output1.Replace("موبو فيلم", "متال موویز");
            output1 = output1.Replace("MoboMovie.net", "MetaLMovieS");
            output1 = output1.Replace("MoboMovie.pw", "MetalMovieS");
            output1 = output1.Replace("MoboMovie.xyz", "MetalMovieS");
            output1 = output1.Replace("MoboMovie.com", "MetalMovieS");
            output1 = output1.Replace("MOBOMOVIE.NET", "MetalMovieS");
            output1 = output1.Replace("MoboMovie.info", "MetalMovieS");
            output1 = output1.Replace("MOBOMOVIES", "MetalMovieS");
            output1 = output1.Replace("MOBOMOVIE", "MetalMovieS");
            output1 = output1.Replace("MOBOFILMS", "MetalMovieS");
            output1 = output1.Replace("MOBOMOVIES", "MetalMovieS");
            output1 = output1.Replace("Mobomovie", "MetalMovieS");
            output1 = output1.Replace("mobomovie", "MetalMovieS");
            output1 = output1.Replace(" اولين سايت زيرنويس چسبيده در ايران ايده اي نو را تجربه کنيد", "دانلود رایگان فیلم و سریال با زیرنویس فارسی چسبیده از اپیکیشن نوار - @Navar_App");
            output1 = output1.Replace("اولين سايت زيرنويس چسبيده در ايران ايده اي نو را تجربه کنيد", "دانلود رایگان فیلم و سریال با زیرنویس فارسی چسبیده از اپیکیشن نوار - @Navar_App");
            output1 = output1.Replace("اولین سایت زیرنویس چسبیده در ایران ایده ای نو را تجربه کنید", "دانلود رایگان فیلم و سریال با زیرنویس فارسی چسبیده از اپیکیشن نوار - @Navar_App");
            output1 = output1.Replace("موبو فیلم", "متال موویز");
            output1 = output1.Replace("موبو", "متال");





            output1 = output1.Replace("Download: MovieApp", "MetaLMovieS");
            output1 = output1.Replace("Free On Google Playe", "Www.MetaLMovieS");
            output1 = output1.Replace("MovieApp.Apk", "متال موویز");
            output1 = output1.Replace("@#Movie_Apk ما", "WwW.MetaLMovieS");
            output1 = output1.Replace("@#Movie_Apk", "@MetaLMovieS / #MetaLMovieS");
            output1 = output1.Replace("DeltaMovieS", "MetaLMovieS");
            output1 = output1.Replace("@#DeltaMovieS", "@MetaLMovieS");
            output1 = output1.Replace("Delta", "Metal");
            output1 = output1.Replace("دیجی", "متال");
            output1 = output1.Replace("دلتا", "متال");
            output1 = output1.Replace("متالموویز", "متال موویز");
            output1 = output1.Replace("MetaL-Movies", "MetaLMovieS");
            output1 = output1.Replace("MetaL-MovieS", "MetaLMovieS");
            output1 = output1.Replace("اپیکیشن", "وبسایت");

            output1 = output1.Replace("@#MovieApp", "WwW.MetaLMovieS");
            output1 = output1.Replace("@#Movie_App", "WwW.MetaLMovieS");
            output1 = output1.Replace("@MovieApp", "WwW.MetaLMovieS");
            output1 = output1.Replace("#MovieApp", "WwW.MetaLMovieS");

            output1 = output1.Replace("@#Movie_Apk ما", "WwW.MetaLMovieS");
            output1 = output1.Replace("@#Movie_Apk", "@MetaLMovieS / #MetaLMovieS");

            Regex.Replace(output1, "mobomovies", "MetaLMovieS", RegexOptions.IgnoreCase);
            Regex.Replace(output1, "mobomovie", "MetaLMovieS", RegexOptions.IgnoreCase);

            output1 = output1.Replace(".::WWW.MetalMovieS::.", "•• WwW.MetaLMovieS ••");

            output1 = output1.Replace(".::WWW.MetalMovieS::.", "@Navar_Apk | اپیکیشن نوار");

            output1 = output1.Replace("   .:: ارائه شده توسط وب سايت ديجي موويز ::.", "ارائه شده توسط دلتا موویز");
            output1 = output1.Replace("•• WwW.MetaLMovieS ••", "@Navar_Apk | اپیکیشن نوار");
            output1 = output1.Replace("MetaLMovieS", "@Navar_Apk | اپیکیشن نوار");
            output1 = output1.Replace("متال موویز", "@Navar_Apk | اپیکیشن نوار");
            output1 = output1.Replace("ديجي", "دلتا");
            output1 = output1.Replace("DIGIMOVIEZ", "DeltaMovieS");
            output1 = output1.Replace("DIGIMOVIES", "DeltaMovieS");
            output1 = Regex.Replace(output1, @"DigiMoviez.Com", "MetalMovies | DeltaMovieS", RegexOptions.IgnoreCase);
            output1 = Regex.Replace(output1, @"DigiMoviez.Co", "MetalMovies | DeltaMovieS", RegexOptions.IgnoreCase);


            //top movies

            output1 = Regex.Replace(output1, "WwW.TopMoviez.net", "MetalMovies | DeltaMovieS", RegexOptions.IgnoreCase);
            output1 = Regex.Replace(output1, "TopMoviez", "MetalMovies | DeltaMovieS", RegexOptions.IgnoreCase);
            output1 = Regex.Replace(output1, "TopMovies", "MetalMovies | DeltaMovieS", RegexOptions.IgnoreCase);
            output1 = Regex.Replace(output1, "TopMovie", "MetalMovies | DeltaMovieS", RegexOptions.IgnoreCase);
            output1 = Regex.Replace(output1, "topmovieznet", "MetalMovies | DeltaMovieS", RegexOptions.IgnoreCase);

            output1 = Regex.Replace(output1, "تاپ مُویز", "متال مُویز | دلتا مُویز", RegexOptions.IgnoreCase);
            output1 = Regex.Replace(output1, "تاپ مویز", "متال مُویز | دلتا مُویز", RegexOptions.IgnoreCase);
            output1 = Regex.Replace(output1, "تاپ موویز", "متال مُویز | دلتا مُویز", RegexOptions.IgnoreCase);
            output1 = Regex.Replace(output1, "تاپ مُویز", "متال مُویز | دلتا مُویز", RegexOptions.IgnoreCase);
            output1 = Regex.Replace(output1, "تاپ مووی", "متال مُویز | دلتا مُویز", RegexOptions.IgnoreCase);
            output1 = Regex.Replace(output1, "تاپ مُوی", "متال مُویز | دلتا مُویز", RegexOptions.IgnoreCase);


            try
            {
                output1 = Regex.Replace(output1, @"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+[\w\-\.,@?^=%&amp;:\/~‌ \+#]*[\w\-\@?^=%&amp‌ ;\/~\+#]", "Www.MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", "Www.MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:t.me.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:telegram.me.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:instagram |[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:insta |[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:telegram |[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:telegram : |[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:telegram :|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:telegram: |[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:telegram:|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:telegram :|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:telegram|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:t.me |[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:t.me/ |[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);
                output1 = Regex.Replace(output1, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:@|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", " MetalMovies", RegexOptions.IgnoreCase);


            }
            catch (Exception)
            {

              
            }
            string[] outPutArray = output1.Split('~');
            return outPutArray;

        }
    }
}
