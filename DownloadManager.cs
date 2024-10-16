﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;
namespace MediaInfo
{
    public static class DownloadManager
    {

        public static int QueuCount = 5;
       public static bool InProgress = false;
        static string IDMPath = @"C:\Program Files (x86)\Internet Download Manager\";
        static string DonwloadLinksTxt = @"G:\SubtitleBotPlugins\DownloadLinks.txt";
        public static List<Link> GetLinksFromText() {
            List<Link> links = new List<Link>();
            string[] LINKS = File.ReadAllLines(DonwloadLinksTxt);
            foreach (var item in LINKS)
            {
                Link _LINK = new Link();
                _LINK.Url = item;
                links.Add(_LINK);
            }
        return links;
        
        }
        public static bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        public static bool AddToQueu(this List<Link> AllLink)
        {
            int i = 0;
            int allLinkNum = AllLink.Count;
            while (AllLink.Count>0)
            {
               
                if (RemoteFileExists(AllLink[0].Url))
                {
                    if (!Idm_open())
                    {
                        StartIDM();
                        Thread.Sleep(3000);
                    }
                 
                    AddIDM(AllLink[0].Url);
                    AllLink.RemoveAt(0);
                }
                else {
                    File.AppendText(AllLink[0].Url + Environment.NewLine);
                AllLink.RemoveAt(0);
                }

                if (i== QueuCount -1|| i== allLinkNum)
                {
                    
                  
                    StartQueu();
                    InProgress = true;
                    while (Idm_open())
                    {
                    Thread.Sleep(3000);
                        Console.WriteLine("In Idm open");
                        // wait here
                    }
                    Program.Job();
                    while (InProgress)
                    {
                        
                        Console.WriteLine("In Progress");
                    }

                    allLinkNum -= QueuCount;
                      i = 0;
                }
                Console.WriteLine(i);
                i++;
            }
            return true;

        }
        public static void AddIDM(string URL) {
            string AddLinkCommand = @"idman.exe /n /q /a /d FileLink  /f FileName";
            string[] MovieParts = URL.Split('/');
            string MovieName=MovieParts[MovieParts.Length-1];
            AddLinkCommand= AddLinkCommand.Replace("FileName", MovieName);
            AddLinkCommand= AddLinkCommand.Replace("FileLink", URL);
            Console.WriteLine(AddLinkCommand);
          
            CMD.RunCMD(AddLinkCommand, IDMPath);
           
        
        }

        public static void StartQueu() {

            string startCommand = @"idman.exe /s";
            CMD.RunCMD(startCommand,IDMPath);
        }
        public static void StartIDM()
        {

            string startCommand = @"idman.exe ";
            CMD.RunCMDIDM(startCommand, IDMPath);
        }
        public static bool Idm_open()
        {
            Process[] myProcesses = Process.GetProcesses();
            bool is_OPEN = false;


            foreach (Process P in myProcesses)
            {
                
                if (P.ProcessName.ToLower().Contains("idm") || P.ProcessName.ToLower().Contains("idm.exe") || P.ProcessName.Contains("Internet Download Manager 6.29") || P.ProcessName.Contains("Internet Download Manager (IDM)") /*&&true|| P.MainWindowTitle.Contains(".720p") || P.MainWindowTitle.Contains(".x265") || P.MainWindowTitle.Contains(".x264") || P.MainWindowTitle.Contains(".480p") || P.MainWindowTitle.Contains(".1080p") || P.MainWindowTitle.Contains(".2160p") || P.MainWindowTitle.Contains(".1440p") || P.MainWindowTitle.Contains(".720p") || P.MainWindowTitle.ToLower().Contains("webdl") || P.MainWindowTitle.ToLower().Contains("bluray") || P.MainWindowTitle.Contains(".480.") || P.MainWindowTitle.Contains(".1080.") || P.MainWindowTitle.Contains(".2160.")*/)
                {
                    is_OPEN = true;


                    return is_OPEN;
                }
               

            }
            return false;
        }
        }
}
