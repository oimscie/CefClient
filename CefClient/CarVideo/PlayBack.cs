using CefClient.CarVideo.ConnectServer;
using CefSharp;
using CefSharp.CarVideo;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CefClient.CarVideo
{
    public partial class PlayBack : Form
    {
        public static PlayBack PlayBacks = null;
        public static DateTimePicker StartTimes;
        public static DateTimePicker StopTime;
        private static ImageBox playBackBox;
        public delegate void ImageDelegate(Image<Bgr, byte> image);
        public delegate void Messboxdelegates(string text);
        public PlayBack()
        {
            InitializeComponent();
            MainWindow.VideoWindow = true;
            PlayBacks = this;
            StartTimes = this.StartTime;
            StopTime = this.OverTime;
            playBackBox = this.imageBox2;
            StaticResource.VideoMultiple = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "1";
            ThreadPool.QueueUserWorkItem(VideoConnect.VideoStart, 8084);
            //* AudioConnect.AudioStart(Encoding.UTF8.GetBytes("$audio!" + StaticResource.Sim + "!1!" + StartTimes.Value.ToString() + "!" + StopTime.Value.ToString() + "!1$"), 8085);历史音频，需要摄像头能录音*//*
            PlayBacks.button2.Enabled = true;
            PlayBacks.button1.Enabled = false;
            PlayBacks.comboBox1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VideoConnect.VideoStop();
            AudioConnect.AudioStop();
            PlayBacks.button1.Enabled = true;
            PlayBacks.button2.Enabled = false;
            PlayBacks.comboBox1.Enabled = false;
        }
        public static void PlayBackPicChange(Image<Bgr, byte> images)
        {
            try
            {
                playBackBox.Image = images;
            }
            catch
            {

            }

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            VideoConnect.VideoStop();
            AudioConnect.AudioStop();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            StaticResource.VideoWindowState = true;
            MainWindow.VideoWindow = false;
            GC.Collect();
        }
        /// <summary>
        /// 信息弹出
        /// </summary>
        /// <param name="info"></param>
        public static void MessboxShow(string info)
        {
            MessageBox.Show(info);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StaticResource.VideoMultiple = int.Parse(comboBox1.SelectedItem.ToString());
        }
    }
}
