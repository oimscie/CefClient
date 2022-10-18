using CefClient.CarVideo.ConnectServer;
using CefClient.OrderMessage;
using CefClient.SuperSocket;
using CefSharp;
using CefSharp.CarVideo;
using CefSharp.DevTools.SystemInfo;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Size = System.Drawing.Size;

namespace CefClient.CarVideo
{
    public partial class LiveWindow : Form
    {
        private PacketForm PacketForm;
        public static LiveWindow LiveWindows;

        public delegate void ImageDelegate(Image<Bgr, byte> image);

        public static ImageBox LiveBox;
        public static Button audioOpen;
        public static Panel LivePanel;
        private static VideoWriter vw = null;

        private static bool record = false;

        public delegate void Messboxdelegates(string text);

        public LiveWindow()
        {
            InitializeComponent();
            LiveWindows = this;
            LivePanel = this.panel1;
            LiveBox = this.imageBox1;
            LiveBox.BringToFront();
            panel1.SendToBack();
            audioOpen = this.OpenAudio;
            PacketForm = new PacketForm();
            MainWindow.VideoWindow = true;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (this.OpenAudio.Text == "语音通话")
            {
                AudioConnect.AudioStart(PacketForm.Audio(new AudioAndVideo()
                {
                    messageType = OrderMessageType.AudioAndVideo,
                    id = "6",
                    datatype = "2",
                    datatypes = "0",
                    sim = StaticResource.Sim,
                    version1078 = StaticResource.Version1078
                }), 8083);
                this.OpenAudio.Text = "关闭通话";
            }
            else
            {
                AudioConnect.AudioStop();
                this.OpenAudio.Text = "语音通话";
            }
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
            try
            {
                LiveBox.Image = images;
                if (record && vw != null)
                {
                    vw.Write(images.Mat);
                }
            }
            catch (Exception ex)
            {
                record = false;
                vw.Dispose();
            }
        }

        private void RecordVideo_Click(object sender, EventArgs e)
        {
            if (this.RecordVideo.Text == "录制视频")
            {
                try
                {
                    vw = new VideoWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/" + (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) + ".avi", VideoWriter.Fourcc('M', 'P', '4', 'V'), 30, new Size(this.imageBox1.Width, this.imageBox1.Height), true);
                    record = true;
                    this.RecordVideo.Text = "停止录制";
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("录制错误", ex);
                    MessageBox.Show("录制错误");
                }
            }
            else
            {
                record = false;
                vw.Dispose();
                this.RecordVideo.Text = "录制视频";
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