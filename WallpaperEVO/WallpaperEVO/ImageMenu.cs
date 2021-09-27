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
    public partial class ImageMenu : Form
    {
        #region DragMove variables
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion
        private void DragMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public ImageMenu(string link)
        {
            InitializeComponent();
            pxbHDImage.LoadAsync(link);
            pxbHDImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void QuitApplication(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizeApplication(object sender, EventArgs e)
        {

        }
    }
}
