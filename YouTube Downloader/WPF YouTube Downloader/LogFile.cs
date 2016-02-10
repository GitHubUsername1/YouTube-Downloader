using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WPF_YouTube_Downloader
{
    class LogFile
    {
        private static string LogSaveLoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\YTDlog.txt";
        public static string VidSavedLoc { get; set; }
        public enum Operation { app_close, app_error, app_start, downloaded, cancelled };
        private static DateTime now;

        public static void WriteToLogFile(Operation op, ILink data, string msg = "")
        {
            now = DateTime.Now;
            if (data != null)
            {
                using (StreamWriter sw = File.AppendText(LogSaveLoc))
                {
                    sw.WriteLine("{0} {1} {2} from {3} {4}", now, op, data.VideoTitle, data.VideoUrl, VidSavedLoc);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(LogSaveLoc))
                {
                    sw.WriteLine("{0}  {1}  {2}", now, op, msg);
                }
            }
        }

        public static void DeleteLog()
        {
            if (File.Exists(LogSaveLoc))
                File.Delete(LogSaveLoc);
        }


    }
}
