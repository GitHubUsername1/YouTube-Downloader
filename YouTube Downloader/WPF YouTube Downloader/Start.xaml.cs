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

namespace WPF_YouTube_Downloader
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Page
    {
        internal static Playlist playlist { get; private set; }
        internal static SaveLocation saveLocation { get; private set; }
        public delegate int CheckUrlLogic(string url);
        private CheckUrlLogic checkUrlLogic;
        private string url;
               
        public Start()
        {
            InitializeComponent();

            checkUrlLogic = (x) =>
            {
                int result = 0;
                if (!Uri.IsWellFormedUriString(x, UriKind.Absolute))
                    return 0;
                if (x.StartsWith("https://")) x = "http://" + x.Substring(8);
                else if (!x.StartsWith("http://")) x = "http://" + x;
                x = x.Replace("www.youtube.com", "youtube.com");
                if (x.StartsWith("http://youtube.com/v/"))
                    x = x.Replace("youtube.com/v/", "youtube.com/watch?v=");
                else if (x.StartsWith("http://youtube.com/watch#"))
                    x = x.Replace("youtube.com/watch#", "youtube.com/watch?");
                if (!x.StartsWith("http://youtube.com"))
                    return 0;
                if (x.Contains("playlist?")) result = 1;
                if (x.Contains("watch?")) result = 2;
                return result;
            };

            txtLink.Focus();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            EnumerateControls.LockGUI<Start>(this);
            this.url = txtLink.Text.Trim(); 
            int result = checkUrlLogic(url);

            switch (result)
            {
                case 0: MessageBox.Show("Invalid youtube url!", "YouTube Downloader");
                    break;
                case 1: playlist = new Playlist(this, url);
                    playlist.ShowsNavigationUI = false;
                    if (playlist.Error != true)
                        this.NavigationService.Navigate(playlist);
                    else
                        playlist = null;
                    break;
                case 2: List<WBlinkinfo> singleLink = new List<WBlinkinfo>();
                    singleLink.Add(new WBlinkinfo(null, url, true));                    
                    SaveLocation saveLocation = new SaveLocation(this, singleLink);
                    saveLocation.ShowsNavigationUI = false;
                    if (saveLocation.Error != true)
                        this.NavigationService.Navigate(saveLocation);
                    else
                        saveLocation = null;
                    break;
            }
            EnumerateControls.UnlockGui<Start>(this);
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

        private void mnuExtractAudio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.CreateWindow(MainWindow.extAudio);
        }
        
    }
}
