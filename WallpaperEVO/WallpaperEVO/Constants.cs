using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallpaperEVO
{
    class Constants
    {
        public readonly struct Websites
        {
            public readonly struct Wallpaperflare
            {
                public const string START = "https://www.wallpaperflare.com/search?wallpaper=";
                public const string PAGE = "&page=";
                public const string XPATH = "/html/body/main/section/ul/li/figure/a/img";
            }
        }
    }
}
