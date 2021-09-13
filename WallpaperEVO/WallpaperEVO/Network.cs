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

        public string[] StartScrape(string URL, string xPath)
        {
            HtmlAgilityPack.HtmlDocument doc = RetrieveHtml(URL);
            string[] tab = new string[50];
            int temp = 0;
            HtmlNodeCollection linkNodes = doc.DocumentNode.SelectNodes(xPath);
            ImageList imgs = new ImageList();

            foreach (HtmlNode item in linkNodes)
            {
                tab[temp] = item.GetAttributeValue("src","nothing");
                temp++;
            }

            return tab;
        }

        private HtmlAgilityPack.HtmlDocument RetrieveHtml(string URL)
        {
            return web.Load(URL);
        }
    }
}
