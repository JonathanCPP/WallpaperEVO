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
        private string _xPathThumb = string.Empty;
        private string _xPathLink = string.Empty;
        private string _xPathHD = string.Empty;

        public Network(string URL, string xPathThumb, string xPathLink)
        {
            _URL = URL;
            _xPathThumb = xPathThumb;
            _xPathLink = xPathLink;
        }

        public Image Scrape()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(_URL);
            Image img = new Image;
            img.thumbList = RetrieveThumbnails(doc);
            return img;
        }

        private List<string> RetrieveThumbnails(HtmlAgilityPack.HtmlDocument web)
        {
            List<string> returnList = new List<string>();

            HtmlNodeCollection nodes = web.DocumentNode.SelectNodes(_xPathThumb);

            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    returnList.Add(node.Attributes["data-src"].Value);
                }
            }
            return returnList;
        }


    }
}
