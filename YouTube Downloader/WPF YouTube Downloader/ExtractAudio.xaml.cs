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
using System.Windows.Shapes;
using JDP;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace WPF_YouTube_Downloader
{
    /// <summary>
    /// Interaction logic for ExtractAudio.xaml
    /// </summary>
    public partial class ExtractAudio : Window
    {
        bool error;
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();

        public ExtractAudio()
        {
            InitializeComponent();
            textBox1.Text = "Drop a multimedia file here i.e. flv mp4 avi";
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgrounderWorker1_ProgressChanged);
            backgroundWorker1.DoWork +=new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (FileInformation.copiedFileName != null)
                File.Delete(FileInformation.copiedFileName);
            if (e.Cancelled == true)
                lblStatus.Content = "Cancelled!";
            else if (error == true)
            {
                lblStatus.Content = "An Error occurred, select a valid file";
                return;
            }
            else
            {
                lblStatus.Visibility = System.Windows.Visibility.Hidden;
                textBlock1.Visibility = System.Windows.Visibility.Visible;
            }         
        }

        void backgrounderWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblStatus.Content = e.UserState.ToString();
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string fn = (string)e.Argument;
            while (true)
            {
                Process proc = new Process();
                proc.StartInfo.FileName = System.IO.Directory.GetCurrentDirectory() + "\\ffmpeg.exe";
                proc.StartInfo.Arguments = "-i " + fn + " -y " + fn.Substring(0, fn.Length - (fn.Length - fn.LastIndexOf("."))) + "_Audio.mp3";
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                StreamReader reader = proc.StandardError;

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    if (line.Contains("No such file or directory"))
                    {
                        error = true;
                        break;
                    }
                    worker.ReportProgress(0, line);
                }
                proc.Close();
                break;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if ((backgroundWorker1.WorkerSupportsCancellation == true) && (backgroundWorker1.IsBusy == true))
            {
                backgroundWorker1.CancelAsync();
            }
            else
                this.Close();
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {
            error = false;
            FileInformation.copiedFileName = null;
            lblStatus.Visibility = System.Windows.Visibility.Visible;
            textBlock1.Visibility = System.Windows.Visibility.Hidden;
            try
            {
                if (System.IO.Path.GetExtension(FileInformation.fileName[0]) == ".flv")
                {
                    JDP.FLVFile f = new JDP.FLVFile(FileInformation.fileName[0]);
                    f.ExtractStreams(true, false, false, null);
                    lblStatus.Visibility = System.Windows.Visibility.Hidden;
                    textBlock1.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    if (FileInformation.fileName[0].Contains(" "))
                    {
                        FileInformation.FN fnDel = (x) =>
                        {
                            FileInformation.copiedFileName = FileInformation.fileName[0].Replace(" ", "_");
                            return FileInformation.copiedFileName;
                        };
                        FileInformation.CopyFile(fnDel);
                        backgroundWorker1.RunWorkerAsync(FileInformation.copiedFileName);
                    }
                    else
                        backgroundWorker1.RunWorkerAsync(FileInformation.fileName[0]);                   
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message + "\nCannot extract audio from the " + System.IO.Path.GetExtension(FileInformation.fileName[0]) + " filetype", "Error"); 
            }
        }

        void DeleteCopiedFile()
        {
            if (FileInformation.copiedFileName != null)
                File.Delete(FileInformation.copiedFileName);
        }

        private void textBox1_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox1_Drop(object sender, DragEventArgs e)
        {
            if (textBox1.Text.Contains("Drop a multimedia file here"))
                textBox1.Text = "";
            FileInformation.fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (FileInformation.fileName.Length > 1)
            {
                MessageBox.Show("Drop only 1 file", "Error");
                textBox1.Text = "Drop a multimedia file here";
                return;
            }
            textBox1.Text = FileInformation.fileName[0];
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effects = DragDropEffects.All;
            else
                e.Effects = DragDropEffects.None;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(FileInformation.fileName[0]));
        }
    }
}
