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
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;

namespace WPF_YouTube_Downloader
{
    /// <summary>
    /// Interaction logic for SaveLocation.xaml
    /// </summary>
    public partial class SaveLocation : Page
    {
        public bool Error { get; private set; }
        private FolderBrowserDialog folderBrowserDialog1;
        internal static Download download { get; private set; }
        private Page previousPage;
        public List<WBlinkinfo> PlayListLinks { get; set; }
        private string saveLoc;
        private static Run run;
        private YouTubeService yts;
        
        public SaveLocation(Page p, List<WBlinkinfo> links)
        {
            InitializeComponent();
            previousPage = p;
            PlayListLinks = links;
            run = new Run(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            saveLoc = run.Text;
            hypSaveLoc.Inlines.Clear();
            hypSaveLoc.Inlines.Add(run);
            GetSaveInfo();
        }
        
        void GetSaveInfo()
        {   
            try
            {
                yts = YouTubeService.Create(PlayListLinks[0].VideoUrl);
                if ((OptionsWindow.Audio && yts.AvailableAudioFormat.Length == 0) || (!OptionsWindow.Audio && yts.AvailableVideoFormat.Length == 0))
                {
                    Error = true;
                    System.Windows.Forms.MessageBox.Show("A problem has ocurred, please try again");
                    throw new Exception("No vid to download");
                }
                PopFormatCmbBox(yts);
            }
            catch (Exception exc)
            {
                Error = true;
                System.Windows.Forms.MessageBox.Show("A problem has ocurred, please try again", "YouTube Downloader");
                LogFile.WriteToLogFile(LogFile.Operation.app_error, null, exc.Message);
                return;
            }
        }

        void PopFormatCmbBox(YouTubeService yts )
        {
            string fileType = "";
            if (OptionsWindow.Audio)
            {
                string t = "";
                foreach (YouTubeVideoFile v in yts.AvailableVideoFormat)
                {
                    fileType = MapFormatCodeToFilter(v.FormatCode);
                    t += fileType;
                    if (fileType != "")
                        cmbFormat.Items.Add(fileType);
                }
                cmbFormat.SelectedIndex = 1;
            }
            else
            {
                foreach (YouTubeVideoFile v in yts.AvailableVideoFormat)
                {
                    fileType = MapFormatCodeToFilter(v.FormatCode);
                    cmbFormat.Items.Add(fileType);
                }
                if (cmbFormat.Items.Contains("HQ Flash Video 480p *.flv"))
                    cmbFormat.SelectedIndex = cmbFormat.Items.IndexOf("Standard Youtube Quality 360p *.mp4");
                if (cmbFormat.SelectedIndex == 0)
                    cmbFormat.SelectedIndex = cmbFormat.Items.IndexOf("HQ Flash Video 480p *.flv");
                if (cmbFormat.SelectedIndex == -1)
                    cmbFormat.SelectedIndex = 1;
                if (fileType.Length > 0) fileType = fileType.Substring(0, fileType.Length - 1);
            }
        }

        private string MapFormatCodeToFilter(byte formatCode)
        {
            string formatDescription = "";

            switch (formatCode)
            {
                case 43:
                    formatDescription = (OptionsWindow.Audio) ? "" : "WebM Video 360p *.webm";
                    break;

                case 44:
                    formatDescription = (OptionsWindow.Audio) ? "" : "WebM Video 480p *.webm";
                    break;

                case 45:
                    formatDescription = (OptionsWindow.Audio) ? "" : "WebM HD Video 720p *.webm";
                    break;

                case 46:
                    formatDescription = (OptionsWindow.Audio) ? "" : "WebM Full HD Video 1080p *.webm";
                    break;

                case 38:
                    formatDescription = (OptionsWindow.Audio) ? "" : "4K Resolution *.mp4";
                    break;

                case 37:
                    formatDescription = (OptionsWindow.Audio) ? "" : "Full HD 1080p *.mp4";
                    break;

                case 22:
                    formatDescription = (OptionsWindow.Audio) ? "" : "HD 720p *.mp4";
                    break;

                case 82:
                    formatDescription = (OptionsWindow.Audio) ? "" : "3D Standard Youtube Quality 360p *.mp4";
                    break;

                case 84:
                    formatDescription = (OptionsWindow.Audio) ? "" : "3D HD 720p *.mp4";
                    break;

                case 35:
                    formatDescription = (OptionsWindow.Audio) ? "[HQ] Advanced Audio Coding [44KHz] *.aac" : "HQ Flash Video 480p *.flv";
                    break;

                case 34:
                    formatDescription = (OptionsWindow.Audio) ? "[HQ] Advanced Audio Coding [22KHz] *.aac" : "LQ Flash Video 360p [AAC] *.flv";
                    break;

                case 18:
                    formatDescription = (OptionsWindow.Audio) ? "" : "Standard Youtube Quality 360p *.mp4";
                    break;

                case 6:
                    formatDescription = (OptionsWindow.Audio) ? "MP3 Audio [44KHz] *.mp3" : "LQ Flash Video [MP3.44KHz] *.flv";
                    break;

                case 5:
                    formatDescription = (OptionsWindow.Audio) ? "MP3 Audio [22KHz] *.mp3" : "LQ Flash Video [MP3.22KHz] (*.flv";
                    break;

                case 13:
                    formatDescription = (OptionsWindow.Audio) ? "" : "Mobile Video XX-Small *.3gp";
                    break;

                case 17:
                    formatDescription = (OptionsWindow.Audio) ? "" : "Mobile Video X-Small *.3gp";
                    break;

                case 36:
                    formatDescription = (OptionsWindow.Audio) ? "" : "Mobile Video Small *.3gp";
                    break;

                default:
                    // New Format?
                    formatDescription = "";
                    break;
            }
            return formatDescription;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.Description = "Select Location to Save";
            folderBrowserDialog1.ShowNewFolderButton = true;
            this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Personal; //or mycomputer
            this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;

            DialogResult diagResult = folderBrowserDialog1.ShowDialog();
            if (diagResult == DialogResult.OK)
            {
                run = new Run(folderBrowserDialog1.SelectedPath);
                hypSaveLoc.Inlines.Clear();
                hypSaveLoc.Inlines.Add(run);
                SaveLoc = run.Text;
            }    
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            EnumerateControls.LockGUI<SaveLocation>(this);
            int format = cmbFormat.SelectedIndex;
            string formatText = (string)cmbFormat.SelectedItem;
          
            download = new Download(this, PlayListLinks, SaveLoc, format, formatText);
            download.ShowsNavigationUI = false;
            if (download.Error != true)
                this.NavigationService.Navigate(download);
            else
                download = null;
            EnumerateControls.UnlockGui<SaveLocation>(this);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(previousPage);
        }

        private void hypSaveLoc_Click(object sender, RoutedEventArgs e)
        {
            var runExplorer = new System.Diagnostics.ProcessStartInfo();
            runExplorer.FileName = "explorer.exe";
            runExplorer.Arguments = SaveLoc;
            System.Diagnostics.Process.Start(runExplorer);
        }

        public string SaveLoc
        {
            get { return this.saveLoc; }
            set
            {
                if (Directory.Exists(value))
                    saveLoc = value;
                else
                    lblMessageSave.Content = "Choose a valid Save location";
            }
        }

        private void mnuOptions_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.optWin.ShowDialog();
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.CreateWindow(MainWindow.abtWin);
        }

    }
}
