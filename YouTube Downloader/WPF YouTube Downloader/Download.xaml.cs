using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Net;
using Microsoft.Win32;
using System.IO;

namespace WPF_YouTube_Downloader
{
    /// <summary>
    /// Interaction logic for Download.xaml
    /// </summary>
    public partial class Download : Page
    {
        public bool Error { get; private set; }
        private static Start newStart;
        private List<WBlinkinfo> links;
        private string saveTo;
        private int format;
        private string formatText;
        private YouTubeService yts = null;
        private WebClient wc;
        private string dwnFile, dwnFileConvAud;
        private Page previousPage;
        private ILink iLink;
        private WBlinkinfo li;

        public Download(Page p, List<WBlinkinfo> l, string save, int format, string fText)
        {          
            InitializeComponent();
            previousPage = p;
            this.links = l;
            this.saveTo = save;
            this.format = ++format;
            this.formatText = fText.Substring(fText.LastIndexOf("."), fText.Length - fText.LastIndexOf("."));
            LogFile.VidSavedLoc = " to  " + save;
            InitiateDownload();
        }

        void InitiateDownload()
        {
            try
            {
                li = this.links.Find(c => c.Downloaded == false && c.SelForDload == true);
                if (li != null)
                {
                    lblStatus.Content = "Getting video information . . .";
                    string link;
                    yts = YouTubeService.Create(li.VideoUrl);
                    if (li.VideoTitle == null)
                        li.VideoTitle = yts.VideoTitle;
                    int i = links.Count(x => x.SelForDload == true);
                    if (i > 1)
                    {
                        li.LinkIndex++;
                        string txt = "Downloading file - " + li.LinkIndex + " of " + links.Count + " - " + yts.VideoTitle;
                        //MessageBox.Show (txt + " " + txt.Length);
                        if (txt.Length > 86)
                            txt = txt.Substring(0, 83) + "...";
                        lblStatus.Content = txt;
                        dwnFile = OptionsWindow.AddFileNumToTitle ? saveTo + "\\" + li.LinkIndex.ToString() + ". " + yts.VideoTitle + formatText : saveTo + "\\" + yts.VideoTitle + formatText;
                        li.LinkIndex--;
                    }
                    else
                    {
                        string txt = "Downloading file - " + yts.VideoTitle;
                        //MessageBox.Show(txt + " " + txt.Length);
                        if (txt.Length > 86)
                            txt = txt.Substring(0, 83) + "...";
                        lblStatus.Content = txt;
                        dwnFile = saveTo + "\\" + yts.VideoTitle + formatText;
                    }
                    if (OptionsWindow.Audio)
                    {           
                        link = yts.AvailableAudioFormat[format - 1].VideoUrl;
                        dwnFileConvAud = dwnFile;
                        dwnFile = Path.GetTempFileName();                                
                    }
                    else
                    {
                        link = yts.AvailableVideoFormat[format - 1].VideoUrl;                      
                    }
                    wc = new WebClient();
                    wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                    wc.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wc_DownloadFileCompleted);
                    wc.DownloadFileAsync(new Uri(link), (dwnFile));
                    this.Cursor = Cursors.Arrow;
                }
            }
            catch (Exception exc)
            {
                Error = true;
                System.Windows.Forms.MessageBox.Show("A problem has ocurred, please try again", "YouTube Downloader");
                LogFile.WriteToLogFile(LogFile.Operation.app_error, null, exc.Message);
                return;
            }
        }

        void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            wc = null;
            if (e.Cancelled) // if cancelled
            {
                li.Downloaded = false;
                iLink = yts;
                LogFile.VidSavedLoc = "";
                LogFile.WriteToLogFile(LogFile.Operation.cancelled, null);
                MessageBox.Show("Download Cancelled", "YouTube Downloader");
                try
                {
                    File.Delete(dwnFile);
                }
                catch (Exception) { }
                links = null;
                yts = null;
                wc = null;
                li = null;
                iLink = null;
                WBlinkinfo.TotalCount = 0;
                lblData.Visibility = Visibility.Hidden;
                btnCancel.Visibility = Visibility.Hidden;
                btnOK.Visibility = Visibility.Visible;
                textBlock1.Visibility = Visibility.Visible;
                textBlock2.Visibility = Visibility.Visible;
                newStart = new Start();
                this.NavigationService.Navigate(newStart);
                return;
            }
            else
            {
                if (OptionsWindow.Audio) // if audio
                {
                    try
                    {
                        FileInfo f = new FileInfo(dwnFile); // if error
                        if (f.Length < 4)
                        {
                            MessageBox.Show("Error occured during download.", "YouTube Downloader");
                            f = null;
                            try
                            {
                                if (OptionsWindow.Audio) File.Delete(dwnFile);
                            }
                            catch (Exception exc) { LogFile.WriteToLogFile(LogFile.Operation.app_error, null, exc.Message); }
                        }
                        else
                        {
                            f = null;
                            progressBar1.Value = 0;
                            if (OptionsWindow.Audio)
                            {
                                toMp3(dwnFile);
                                dwnFile = dwnFile.Substring(0, dwnFile.Length - 4) + formatText;
                                if (File.Exists(dwnFileConvAud))
                                    File.Delete(dwnFileConvAud);
                                File.Copy(dwnFile, dwnFileConvAud);
                                try { File.Delete(dwnFile); }
                                catch (Exception exc) { LogFile.WriteToLogFile(LogFile.Operation.app_error, null, exc.Message); }
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error: " + exc.Message, "YouTubeDownloader");
                        LogFile.WriteToLogFile(LogFile.Operation.app_error, null, exc.Message);
                    }
                }             
            }
            int i = li.LinkIndex;
            links[i].Downloaded = true;
            iLink = li;
            LogFile.WriteToLogFile(LogFile.Operation.downloaded, iLink);
            if (links.Any(x => x.Downloaded == false && x.SelForDload == true))     // to download left check
                InitiateDownload(); 
            else                    
            {               
                lblStatus.Content = "Download(s) Completed";
                if (OptionsWindow.ShutdownPC)
                    System.Diagnostics.Process.Start("shutdown", "/s /f /t 060 /c " + (char)34 + "YOUR COMPUTER WILL BE TURNED OFF IN 60 seconds" + (char)34);               
                links = null;
                yts = null;
                wc = null;
                li = null;
                iLink = null;
                WBlinkinfo.TotalCount = 0;
                lblData.Visibility = Visibility.Hidden;
                btnCancel.Visibility = Visibility.Hidden;
                btnOK.Visibility = Visibility.Visible;
                textBlock1.Visibility = Visibility.Visible;
                textBlock2.Visibility = Visibility.Visible;
            }            
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            float bytesIn = float.Parse(e.BytesReceived.ToString());
            float totalBytes = float.Parse(e.TotalBytesToReceive.ToString());
            lblPercentage.Content = e.ProgressPercentage + " %";
            lblData.Content = (bytesIn / 1000000).ToString("0.00") + "MB of " + (totalBytes / 1000000).ToString("0.00") + "MB";
            progressBar1.Value = e.ProgressPercentage;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (wc != null) wc.CancelAsync();
            wc = null;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //newStart = new Start(links.Count(x => x.SelForDload == true).ToString()+" file(s) downloaded successfully, see Log");
            newStart = new Start();
            this.NavigationService.Navigate(newStart);
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            string loc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\YTDlog.txt";
            System.Diagnostics.Process.Start(loc);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            LogFile.WriteToLogFile(LogFile.Operation.app_close, null);
        }

        private void toMp3(string inputPath)
        {
            try
            {
                using (FLVFile flvFile = new FLVFile(Path.GetFullPath(inputPath)))
                {
                    flvFile.ExtractStreams(PromptOverwrite);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message + Environment.NewLine);
                LogFile.WriteToLogFile(LogFile.Operation.app_error, null, exc.Message);
            }
        }

        /// <returns>True if the file must be saved in the path specified</returns>
        private static bool PromptOverwrite(ref string path)
        {
            //try { File.Delete(dwnFile); }
            //catch (Exception) { }
            //path = dwnFile;
            return true;
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.CreateWindow(MainWindow.abtWin);
        }

        private void hypOpenSaveLoc_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(saveTo);
        }
    }
}
