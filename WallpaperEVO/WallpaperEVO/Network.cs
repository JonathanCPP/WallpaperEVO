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

        public Network(string URL, string xPathThumb, string xPathLink)
        {
            _URL = URL;
            _xPathThumb = xPathThumb;
            _xPathLink = xPathLink;
        }

        public Image Scrape()
        {
            HtmlWeb web = new HtmlWeb();
            Image img = new Image();

            HtmlAgilityPack.HtmlDocument doc = web.Load(_URL);
            img.thumbList = RetrieveTagData(doc, _xPathThumb, "src"); //thumbnail images
            img.linkList = RetrieveTagData(doc, _xPathLink, "href"); //hd links

            for (int i = 0; i < img.linkList.Count; i++)
            {
                string substr = string.Empty;
                substr = "https://wallpaperscraft.com" + img.linkList[i];
                img.linkList[i] = substr;
            }
            return img;
        }
        public List<string> GetHDImage(string link, string xPath, string tag)
        {
            List<string> linkToReturn = new List<string>();
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(link);
            linkToReturn = RetrieveTagData(doc, xPath, tag);

            return linkToReturn;

        }

        private List<string> RetrieveTagData(HtmlAgilityPack.HtmlDocument web, string xPath, string tag)
        {
            List<string> returnList = new List<string>();
            HtmlNodeCollection nodes = web.DocumentNode.SelectNodes(xPath);

            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    returnList.Add(node.Attributes[tag].Value);
                }
            }

            return returnList;
        }

    }
}
