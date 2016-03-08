using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Collections;

namespace WPF_YouTube_Downloader
{
    class GetHyperlinks
    {
        public static string pageUri { get; private set; }
        static Uri baseUri;
        static Uri actual;
        static string path;
        public delegate bool HyperDel(HtmlAttribute link);
        public static HyperDel HypDel { get; private set; }
        static ArrayList dupes = new ArrayList(); 

        public static void AddMethodToDel(HyperDel method)
        {
            HypDel += method;
        }

        public static void GetSpecificLinksFromHTML<T>(HyperDel del, string playLstUrl, List<T> arg) where T : ILink, new()
        {
            List<string> titles = new List<string>();
            int i = 0;
            pageUri = playLstUrl;
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(pageUri);

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//tr"))
            {
                HtmlAttribute title = node.Attributes["data-title"];
                titles.Add(title.Value);
            }

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute haLink = link.Attributes["href"];

                if (dupes.Contains(haLink.Value))
                {
                    haLink = null;
                }
                else
                    dupes.Add(haLink.Value);

                if (haLink != null && i < titles.Count())
                {
                    if (del(haLink))
                    {
                        path = haLink.Value.ToString();
                        path = path.Replace("amp;", "");
                        baseUri = new Uri(pageUri, UriKind.Absolute);
                        actual = new Uri(baseUri, path);
                        arg.Add(new T() { VideoTitle = titles[i], VideoUrl = actual.AbsoluteUri.ToString() });
                        i++;
                    }
                }
            }
        }
    }
}




