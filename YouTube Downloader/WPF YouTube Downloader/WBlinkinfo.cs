using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Windows.Forms;

namespace WPF_YouTube_Downloader
{
        public class WBlinkinfo : ILink
        {
            public string VideoTitle { get; set; }
            public string VideoUrl {get; set;}
            public bool Downloaded { get; set; }
            public int LinkIndex { get; set; }
            public static int TotalCount { get; set; }
            public bool SelForDload { get; set; }

            public WBlinkinfo()
            {               
                TotalCount++;
                this.LinkIndex = TotalCount - 1;
            }

            public WBlinkinfo(string t, string l, bool sel = false)
                : this()
            {
                VideoTitle = t;
                VideoUrl = l;
                Downloaded = false;
                SelForDload = sel;
            }

            public WBlinkinfo(string t, string l)
                :this()
            {
                VideoTitle = t;
                VideoUrl = l;
                Downloaded = false;
                SelForDload = false;
            }
        }    
}
