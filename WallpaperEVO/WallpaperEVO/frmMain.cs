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

        const string linkpart1 = "https://wallpaperscraft.com/search/?query=";

        public frmMain()
        {
            InitializeComponent();
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

        private void SendRequest(object sender, EventArgs e)
        {
            Network net = new Network();
            string[] tab = net.StartScrape(linkpart1 + txbSearch.Text, "/html/body/div/div[2]/div[2]/div/div[2]/div[1]/ul/li/a/span[1]/img");
            ShowImages(tab);
        }

        private void ShowImages(string[] tab)
        {
            int xOffset = 0;
            int yOffset = 0;

            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] != null)
                {
                    PictureBox img = new PictureBox();
                    pnlContent.Controls.Add(img);
                    img.Load(@tab[i]);
                    img.Location = new Point(xOffset, yOffset);
                    img.SizeMode = PictureBoxSizeMode.AutoSize;
                    xOffset += img.Width;
                }
            }
        }
    }
}
