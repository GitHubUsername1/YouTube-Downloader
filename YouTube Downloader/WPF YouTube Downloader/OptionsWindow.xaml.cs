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

namespace WPF_YouTube_Downloader
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public static bool AddFileNumToTitle { get; set; }
        public static bool ShutdownPC { get;  set; }
        public static bool Audio { get; set; }
        
        public OptionsWindow()
        {
            InitializeComponent();
            AddFileNumToTitle = true;
            this.DataContext = this;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }


    }
}
