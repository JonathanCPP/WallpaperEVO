using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace WallpaperEVO
{
    class Network
    {
        HtmlWeb web = new HtmlWeb();

        public void StartScrape(string URL, string xPath)
        {
            HtmlAgilityPack.HtmlDocument doc = RetrieveHtml(URL);
            HtmlNode[] links = RetrieveURLs(xPath, doc);
        }

        private HtmlAgilityPack.HtmlDocument RetrieveHtml(string URL)
        {
            return web.Load(URL);
        }

        private HtmlNode[] RetrieveURLs(string xPath, HtmlAgilityPack.HtmlDocument doc)
        {
            return doc.DocumentNode.SelectNodes(xPath).ToArray();
        //https://stackoverflow.com/questions/4835868/how-to-get-img-src-or-a-hrefs-using-html-agility-pack c'est mieux
        }
    }
}
