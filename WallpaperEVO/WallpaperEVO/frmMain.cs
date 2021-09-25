using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if(e.KeyCode == Keys.Enter)
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

                List<string> imgTab = GetImages(GetURL(cmbWebsite.Text, txbSearch.Text, index), GetxPath(cmbWebsite.Text));
                imgTab.ForEach(ShowImages);
            }
            else
                MessageBox.Show("Missing arguments : choose a website and a tag");
            ToggleControls();
        }

        bool CheckData()
        {
            if (cmbWebsite.Text != string.Empty && txbSearch.Text != string.Empty)
                return true;
            else
                return false;
        }
        string GetURL(string website, string tag, int index)
        {
            string strToReturn = string.Empty;
            switch (website)
            {
                case "Wallpaperflare":
                    strToReturn += Constants.Websites.Wallpaperflare.START + tag;
                    if (index > 1)
                        strToReturn += Constants.Websites.Wallpaperflare.PAGE + index.ToString();
                    break;
                default:
                    return "error";
            }
            return strToReturn;
        }
        string GetxPath(string website)
        {
            string strToReturn = string.Empty;
            switch (website)
            {
                case "Wallpaperflare":
                    strToReturn = Constants.Websites.Wallpaperflare.XPATH;
                    break;
                default:
                    return "error";
            }
            return strToReturn;
        }
        List<string> GetImages(string website, string xPath)
        {
            Network nw = new Network(website,xPath);
            return nw.Scrape();
        }
        private void ShowImages(string link)
        {
            PictureBox pxb = new PictureBox();
            pxb.LoadAsync(link);
            pnlContent.Controls.Add(pxb);
            pxb.SizeMode = PictureBoxSizeMode.StretchImage;
            pxb.BorderStyle = BorderStyle.Fixed3D;
            pxb.Size = new Size(235,127);
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

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
        }
    }
}
