using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF_YouTube_Downloader
{
    interface ILink
    {
        string VideoTitle { get; set; }
        string VideoUrl { get; set; }       
    }
}
