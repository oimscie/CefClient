using CefClient.CarVideo.ConnectServer;
using CefClient.OrderMessage;
using CefClient.SuperSocket;
using CefSharp;
using CefSharp.CarVideo;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CefClient.CarVideo
{
    public partial class LiveWindow : Form
    {
        private PacketForm PacketForm;
        public static LiveWindow LiveWindows;
        public delegate void ImageDelegate(Image<Bgr, byte> image);
        private static ImageBox LiveBox;
        private byte[] abort = new byte[] { 11, 22, 33, 44 };
        public static Button audioOpen;
        public delegate void Messboxdelegates(string text);
        public LiveWindow()
        {
            InitializeComponent();
            LiveWindows = this;
            LiveBox = this.imageBox1;
            audioOpen = this.OpenAudio;
            PacketForm = new PacketForm();
            MainWindow.VideoWindow = true;

        }
        private void button_Click(object sender, EventArgs e)
        {
            AudioConnect.AudioStart(PacketForm.Audio(new AudioAndVideo() {
            messageType=OrderMessageType.AudioAndVideo,
                id="6",
                datatype="2",
                datatypes="0",
                sim= StaticResource.Sim,
                version1078= StaticResource.Version1078
            }), 8083);
            OpenAudio.Enabled = false;
            CloseAudio.Enabled = true;
        }

        private void LiveWindow_Load(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(VideoConnect.VideoStart, 8082);
        }

        private void LiveWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            AudioConnect.AudioStop();
            VideoConnect.VideoStop();
        }

        private void LiveWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            StaticResource.VideoWindowState = true;
            MainWindow.VideoWindow = false;
            GC.Collect();
        }
        public static void LivePicChange(Image<Bgr, byte> images)
        {
            try { LiveBox.Image = images; } catch { }
           
        }
        private void CloseAudio_Click(object sender, EventArgs e)
        {
            AudioConnect.AudioStop();
            OpenAudio.Enabled = true;
            CloseAudio.Enabled = false;
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
