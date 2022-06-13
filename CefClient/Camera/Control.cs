using CefClient.Camera.ConnectServer;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CefClient.Camera
{
    public partial class CameraControl : Form
    {
        private readonly byte[] mark = new byte[] {11,22,33,44};
        public static bool IsControl=false;
        public static  CameraControl CamControls;
        private VideoClient VideoClient;
        public CameraControl(VideoClient VideoClients)
        {
            VideoClient = VideoClients;
            InitializeComponent();
            CamControls = this;
            IsControl = true;
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send(Encoding.UTF8.GetBytes("Control!up,0,end").Concat(mark).ToArray());
        }

        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.UTF8.GetBytes("Control!up,1,end").Concat(mark).ToArray());
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.UTF8.GetBytes("Control!down,0,end").Concat(mark).ToArray());
        }

        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.UTF8.GetBytes("Control!down,1,end").Concat(mark).ToArray());
        }

        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.ASCII.GetBytes("Control!left,0,end").Concat(mark).ToArray());
        }

        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.ASCII.GetBytes("Control!left,1,end").Concat(mark).ToArray());
        }

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.ASCII.GetBytes("Control!right,0,end").Concat(mark).ToArray());
        }

        private void btnRight_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.ASCII.GetBytes("Control!right,1,end").Concat(mark).ToArray());
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.ASCII.GetBytes("Control!amplification,0,end").Concat(mark).ToArray());
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send(Encoding.ASCII.GetBytes("Control!amplification,1,end").Concat(mark).ToArray());
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.ASCII.GetBytes("Control!narrow,0,end").Concat(mark).ToArray());
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.ASCII.GetBytes("Control!narrow,1,end").Concat(mark).ToArray());
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send(Encoding.ASCII.GetBytes("Control!forward,0,end").Concat(mark).ToArray());
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.ASCII.GetBytes("Control!forward,1,end").Concat(mark).ToArray());
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send( Encoding.ASCII.GetBytes("Control!back,0,end").Concat(mark).ToArray());
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send(Encoding.ASCII.GetBytes("Control!back,1,end").Concat(mark).ToArray());
        }

        private void btnjpg_Click(object sender, EventArgs e)
        {
            Camera.CameraWindow.GetImage();
        }
    }
}
