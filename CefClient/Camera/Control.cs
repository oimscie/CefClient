using CefClient.Camera.ConnectServer;
using CefClient.OrderMessage;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CefClient.Camera
{
    public partial class CameraControl : Form
    {
        private readonly byte[] mark = new byte[] { 11, 22, 33, 44 };
        public static bool IsControl = false;
        public static CameraControl CamControls;
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
            VideoClient.Send(new PacketForm().MonitorControl(
                new MonitorControl()
                {
                    messageType = OrderMessageType.MonitorControl,
                    OperationType = MonitorOperationType.up,
                    StartOrStop = "0"
                }));
        }

        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
                new MonitorControl()
                {
                    messageType = OrderMessageType.MonitorControl,
                    OperationType = MonitorOperationType.up,
                    StartOrStop = "1"
                }));
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
                 new MonitorControl()
                 {
                     messageType = OrderMessageType.MonitorControl,
                     OperationType = MonitorOperationType.down,
                     StartOrStop = "0"
                 }));
        }

        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
             new MonitorControl()
             {
                 messageType = OrderMessageType.MonitorControl,
                 OperationType = MonitorOperationType.down,
                 StartOrStop = "1"
             }));
        }

        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
                 new MonitorControl()
                 {
                     messageType = OrderMessageType.MonitorControl,
                     OperationType = MonitorOperationType.left,
                     StartOrStop = "0"
                 }));
        }

        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
            new MonitorControl()
            {
                messageType = OrderMessageType.MonitorControl,
                OperationType = MonitorOperationType.left,
                StartOrStop = "1"
            }));
        }

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
                 new MonitorControl()
                 {
                     messageType = OrderMessageType.MonitorControl,
                     OperationType = MonitorOperationType.right,
                     StartOrStop = "0"
                 }));
        }

        private void btnRight_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
                new MonitorControl()
                {
                    messageType = OrderMessageType.MonitorControl,
                    OperationType = MonitorOperationType.right,
                    StartOrStop = "1"
                }));
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
               new MonitorControl()
               {
                   messageType = OrderMessageType.MonitorControl,
                   OperationType = MonitorOperationType.amplification,
                   StartOrStop = "0"
               }));
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
               new MonitorControl()
               {
                   messageType = OrderMessageType.MonitorControl,
                   OperationType = MonitorOperationType.amplification,
                   StartOrStop = "1"
               }));
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
                new MonitorControl()
                {
                    messageType = OrderMessageType.MonitorControl,
                    OperationType = MonitorOperationType.narrow,
                    StartOrStop = "0"
                }));
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
               new MonitorControl()
               {
                   messageType = OrderMessageType.MonitorControl,
                   OperationType = MonitorOperationType.narrow,
                   StartOrStop = "1"
               }));
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
                new MonitorControl()
                {
                    messageType = OrderMessageType.MonitorControl,
                    OperationType = MonitorOperationType.forward,
                    StartOrStop = "0"
                }));
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
                 new MonitorControl()
                 {
                     messageType = OrderMessageType.MonitorControl,
                     OperationType = MonitorOperationType.forward,
                     StartOrStop = "1"
                 }));
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
               new MonitorControl()
               {
                   messageType = OrderMessageType.MonitorControl,
                   OperationType = MonitorOperationType.back,
                   StartOrStop = "0"
               }));
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            VideoClient.Send(new PacketForm().MonitorControl(
               new MonitorControl()
               {
                   messageType = OrderMessageType.MonitorControl,
                   OperationType = MonitorOperationType.back,
                   StartOrStop = "1"
               }));
        }

        private void btnjpg_Click(object sender, EventArgs e)
        {
            Camera.CameraWindow.GetImage();
        }
    }
}