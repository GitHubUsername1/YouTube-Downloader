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
using JDP;
using System.ComponentModel;

namespace WPF_YouTube_Downloader
{
    /// <summary>
    /// Interaction logic for ExtractAudio.xaml
    /// </summary>
    public partial class ExtractAudio : Page
    {
        //BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        //Page previousPage;
        //string[] files;

        public ExtractAudio(Page p)
        {
            // InitializeComponent();
        //    previousPage = p;
        //    listBox1.Items.Add("Drop a Valid Multimedia File Here");
        //    backgroundWorker1.WorkerReportsProgress = true;
        //    backgroundWorker1.WorkerSupportsCancellation = true;
        //    backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
        //    backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        //    backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        //void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    lblExtract.Content = "Done";
        //}

        //void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    lblExtract.Content = "Extracting at " + e.ProgressPercentage + " FPS";
        //}

        //void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{                         
        //    BackgroundWorker worker = sender as BackgroundWorker;
        //    foreach (string fn in files)
        //    {
        //        if (worker.CancellationPending == true)
        //        {
        //            e.Cancel = true;
        //            break;
        //        }

        //        int number;
        //        JDP.FLVFile f = new JDP.FLVFile(fn);
        //        f.ExtractStreams(true, false, false, null);
        //        FractionUInt32 i = (FractionUInt32)f.AverageFrameRate;
        //        string s = i.ToString();
        //        s = s.Substring(0, 2);
        //        bool result = Int32.TryParse(s, out number);
        //        if (result)
        //            worker.ReportProgress(number);
        //        System.Threading.Thread.Sleep(5000);
        //    }
        //}

        //private void listBox1_DragEnter(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
        //        e.Effects = DragDropEffects.All;
        //    else
        //        e.Effects = DragDropEffects.None;
        //}

        //private void listBox1_DragDrop(object sender, DragEventArgs e)
        //{
        //    if (listBox1.Items.Contains("Drop a Valid Multimedia File Here"))
        //        listBox1.Items.Remove("Drop a Valid Multimedia File Here");

        //    files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        //    foreach (string fileName in files)
        //    {
        //        string ext = System.IO.Path.GetExtension(fileName);
        //        if (ext == ".flv")
        //            listBox1.Items.Add(fileName);
        //        else
        //            MessageBox.Show("Cannot Extract Audio from the " + ext + " filetype", "Invalid File Type");
        //    }
        //}

        //private void btnExtract_Click(object sender, RoutedEventArgs e)
        //{
        //    if (backgroundWorker1.IsBusy != true)
        //    {
        //        // Start the asynchronous operation.
        //        backgroundWorker1.RunWorkerAsync();
        //    }
        //}

        //private void btnCancel_Click(object sender, RoutedEventArgs e)
        //{
        //    if (backgroundWorker1.WorkerSupportsCancellation == true)
        //    {
        //        // Cancel the asynchronous operation.
        //        backgroundWorker1.CancelAsync();
        //    }
        //}

    }
}
