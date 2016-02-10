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
using System.IO;
//using System.Windows.Forms;
using System.Collections.ObjectModel;
using HtmlAgilityPack;
using System.Diagnostics;


namespace WPF_YouTube_Downloader
{
    /// <summary>
    /// Interaction logic for Playlist.xaml
    /// </summary>
    public partial class Playlist : Page
    {
        public bool Error { get; private set; }
        private Page previousPage;
        internal static SaveLocation saveLocation { get; private set; }
        public string PlayListLink { get; set; }
        private List<WBlinkinfo> wb1linkInfo = new List<WBlinkinfo>();
        public ObservableCollection<CheckedListItem<WBlinkinfo>> ObsColWB { get; set; }
          
        public Playlist(Page p, string link)
        {
            InitializeComponent();
            previousPage = p;
            this.PlayListLink = link;
            SetUpPage();
        }

        private void SetUpPage()
        {
            try
            {
                GetHyperlinks.AddMethodToDel((x)=> {if (!x.Value.Contains("Play all") && x.Value.Contains("watch?")) return true; else return false;});
                GetHyperlinks.GetSpecificLinksFromHTML<WBlinkinfo>(GetHyperlinks.HypDel, this.PlayListLink, wb1linkInfo);

                if (wb1linkInfo.Count < 1)
                {
                    MessageBox.Show("No links found, check link and try again", "YouTube Downloader");
                    Error = true;
                    return;
                }
                else
                    PopChckLstBox();
            }
            catch (Exception exc)
            {
                LogFile.WriteToLogFile(LogFile.Operation.app_error, null, exc.Message);
                MessageBox.Show("An error has ocurred, check link and try again", "YouTube Downloader");                                  
                Error = true;
                return;
            }
        }

        void PopChckLstBox()
        {
            ObsColWB = new ObservableCollection<CheckedListItem<WBlinkinfo>>();
            foreach (var i in wb1linkInfo)
            {
                ObsColWB.Add(new CheckedListItem<WBlinkinfo>(i));
            }
            DataContext = this;
        }

        private void btnAddAll_Click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < ObsColWB.Count; x++)
            {
                ObsColWB[x].IsChecked = true;
            }
        }

        private void btnRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < ObsColWB.Count; x++)
            {
                ObsColWB[x].IsChecked = false;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (!ObsColWB.All(x => x.IsChecked == false))
            {
                EnumerateControls.LockGUI<Playlist>(this);
                for (int c = 0; c < ObsColWB.Count; c++)
                {
                    if (ObsColWB[c].IsChecked == true)
                        wb1linkInfo[c].SelForDload = true;
                    else
                        wb1linkInfo[c].SelForDload = false;
                }
                saveLocation = new SaveLocation(this, wb1linkInfo);
                saveLocation.ShowsNavigationUI = false;
                if (saveLocation.Error != true)
                    this.NavigationService.Navigate(saveLocation);

                else
                    saveLocation = null;
                EnumerateControls.UnlockGui<Playlist>(this);
            }
            else
                lblMessage.Content = "No Links Selected!";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(previousPage);
            wb1linkInfo = null;
            WBlinkinfo.TotalCount = 0;
        }

        private void listbox1_GotFocus(object sender, RoutedEventArgs e)
        {
            lblMessage.Content = "";
        }

        private void mnuOptions_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.optWin.ShowDialog();
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
            //Environment.Exit(0);
        }

        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.CreateWindow(MainWindow.abtWin);
        }
    }
}
