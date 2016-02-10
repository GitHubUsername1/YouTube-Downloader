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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        internal static Start start {get; private set;}
        internal static OptionsWindow optWin { get; private set; }
        internal static AboutWindow abtWin { get; private set; }
        internal static ExtractAudio extAudio { get; private set; }
        
        public MainWindow()
        {
            InitializeComponent();  
            optWin = new OptionsWindow();
            start = new Start();           
            start.ShowsNavigationUI = false; 
            this.NavigationService.Navigate(start);
        }

        public static void CreateWindow<T>(T win)  where T : Window
        {
            T temp = (T)Activator.CreateInstance<T>();
            temp.ShowDialog();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}
