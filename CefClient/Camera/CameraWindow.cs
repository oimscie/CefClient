using CefSharp;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CefClient.Camera
{
    public partial class CameraWindow : Form
    {
        public static PictureBox Picture;
        public static CameraWindow CameraWindows;
        public VideoClient VideoClient;
        private CameraControl control;

        public delegate void Messboxdelegates(string text);

        public CameraWindow()
        {
            InitializeComponent();
            CameraWindows = this;
            Picture = this.picture;
            StaticResource.CameraVideoQueueReset();
            MainWindow.VideoWindow = true;
            StaticResource.CameraVideoIsEnd = true;
        }

        private void CameraWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            StaticResource.CameraVideoIsEnd = false;
            VideoClient.Close();
            StaticResource.CameraVideoQueueReset();
            MainWindow.VideoWindow = false;
        }

        private void CameraWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            StaticResource.VideoWindowState = true;
            GC.Collect();
        }

        private void CameraWindow_Load(object sender, EventArgs e)
        {
            VideoClient = new VideoClient();
            VideoClient.ConnectServer();
        }

        private void picture_DoubleClick(object sender, EventArgs e)
        {
            control = new CameraControl(VideoClient);
            control.ShowDialog();
        }

        /// <summary>
        /// 截图
        /// </summary>
        public static void GetImage()
        {
            try
            {
                Bitmap bit = new Bitmap(Picture.Width, Picture.Height);
                Graphics g = Graphics.FromImage(bit);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.CopyFromScreen(Picture.PointToScreen(Point.Empty), Point.Empty, Picture.Size);
                bit.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + '/' + DateTime.Now.ToString().Replace('/', '-').Replace(' ', '-').Replace(':', '-') + ".png");
                StaticResource.MessShow("截取成功，文件保存在桌面");
            }
            catch
            {
                StaticResource.MessShow("截取失败");
            }
        }

        /// <summary>
        /// 信息弹出
        /// </summary>
        /// <param name="info"></param>
        public static void MessboxShow(string info)
        {
            MessageBox.Show(info);
        }
    }
}