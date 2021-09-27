using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace WallpaperEVO
{
    public partial class frmMain : Form
    {
        #region DragMove variables
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        int index = 1;
        int yImage = 0;
        int xImage = 0;
        int temp = 0;

        public const string START = "https://wallpaperscraft.com/search/?order=&page=";
        public const string QUERY = "&query=";
        public const string END = "&size=1920x1080";
        public const string XPATH_THUMB = "/html/body/div/div[2]/div[2]/div/div[2]/div[1]/ul/li/a/span[1]/img";
        public const string XPATH_LINK = "/html/body/div/div[2]/div[2]/div/div[2]/div[1]/ul/li/a";
        public const string XPATH_HD = "/html/body/div/div[2]/div[2]/div/div[2]/div[1]/div[1]/a/img";

        public frmMain()
        {
            InitializeComponent();
        }

        void ToggleControls()
        {
            pnlSearch.Enabled = !pnlSearch.Enabled;
        }

        private void DragMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void QuitApplication(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizeApplication(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void AttemptSearchButton(object sender, EventArgs e)
        {
            AttemptSearch();
        }
        private void AttemptSearchEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AttemptSearch();
            }
        }
        private void AttemptSearch()
        {
            ToggleControls();
            if (CheckData())
            {
                pnlContent.Controls.Clear();
                xImage = 0;
                yImage = 0;
                temp = 0;
                Image c_img = new Image();

                c_img = GetImages(GetURL(txbSearch.Text, index), XPATH_THUMB, XPATH_LINK, XPATH_HD);
                ShowImages(c_img);
            }
            else
                MessageBox.Show("Missing arguments : choose a website and a tag");
            ToggleControls();
        }

        bool CheckData()
        {
            if (txbSearch.Text != string.Empty)
                return true;
            else
                return false;
        }
        string GetURL(string tag, int index)
        {
            string strToReturn = string.Empty;
            strToReturn = START + index + QUERY + tag + END;
            return strToReturn;
        }
        Image GetImages(string website, string xPathThumb, string xPathLink, string xPathHD)
        {
            Network nw = new Network(website, xPathThumb, xPathLink);
            return nw.Scrape();
        }
        private void ShowImages(Image c_img)
        {
            for (int i = 0; i < c_img.thumbList.Count; i++)
            {
                PictureBox pxb = new PictureBox();
                pxb.LoadAsync(c_img.thumbList[i]);
                pxb.Click += new EventHandler(showlink);
                pxb.Tag = c_img.linkList[i];
                pnlContent.Controls.Add(pxb);
                pxb.SizeMode = PictureBoxSizeMode.StretchImage;
                pxb.BorderStyle = BorderStyle.FixedSingle;
                pxb.Size = new Size(235, 127);
                pxb.Location = new Point(xImage, yImage);

                temp++;
                if (temp < 4)
                    xImage += 245;
                else
                {
                    temp = 0;
                    xImage = 0;
                    yImage += 138;
                }
            }

        }

        private void showlink(object sender, EventArgs e)
        {
            PictureBox pxb = sender as PictureBox;
            Network nw = new Network("", "", "");
            string imgLink = string.Empty;

            List<string> SingleImageLink = nw.GetHDImage(pxb.Tag.ToString(), XPATH_HD, "src");
            if (SingleImageLink.Count > 0)
            {
                imgLink = SingleImageLink[0];

                ImageMenu imgM = new ImageMenu(imgLink);
                imgM.ShowDialog();
            }
            else
            {
                MessageBox.Show("Cannot open image");
            }
        }

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
        }

        int clrBrightness = 130;
        bool upDown;
        Random rdm = new Random();
        private void tmrRGB_Tick(object sender, EventArgs e)
        {
            Color clr = new Color();

            if (clrBrightness == 130 || clrBrightness == 255)
                upDown = !upDown;

            if (upDown)
                clrBrightness++;
            else
                clrBrightness--;

            clr = Color.FromArgb(clrBrightness, clrBrightness, clrBrightness);

            lblQuote.ForeColor = clr;
            lblLogo.ForeColor = clr;
            lblTime.ForeColor = clr;
        }
    }
}
