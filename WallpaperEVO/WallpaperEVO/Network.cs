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
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(_URL);
                List<string> strTabLinks = RetrieveLinks(doc);
                return strTabLinks;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        private List<string> RetrieveLinks(HtmlAgilityPack.HtmlDocument web)
        {
            List<string> returnList = new List<string>();
            HtmlNodeCollection imgs = web.DocumentNode.SelectNodes(_xPath+"[@src]");

            foreach (HtmlNode img in imgs)){
                HtmlAttribute src = img
            }
            return returnList;
        }
    }
}
