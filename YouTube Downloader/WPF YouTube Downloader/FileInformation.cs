using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WPF_YouTube_Downloader
{
    struct FileInformation
    {
        public static string[] fileName { get; set; }
        public static string copiedFileName { get; set; }
        public delegate string FN(string file);

        public static void CopyFile(FN del)
        {
            del(fileName[0]);
            if (File.Exists(copiedFileName))
                File.Delete(copiedFileName);

            File.Copy(fileName[0], copiedFileName);
        }
    }
}
