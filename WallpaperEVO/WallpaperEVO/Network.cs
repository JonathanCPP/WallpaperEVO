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
        private string _URL = string.Empty;
        private string _xPath = string.Empty;

        public Network(string URL, string xPath)
        {
            _URL = URL;
            _xPath = xPath;
        }

        public List<string> Scrape()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(_URL);
            List<string> strTabLinks = RetrieveThumbnails(doc);
            return strTabLinks;
        }

        private List<string> RetrieveThumbnails(HtmlAgilityPack.HtmlDocument web)
        {
            List<string> returnList = new List<string>();

            foreach (HtmlNode node in web.DocumentNode.SelectNodes(_xPath))
            {
                returnList.Add(node.Attributes["data-src"].Value);
            }
            return returnList;
        }


    }
}
