using CefClient.CarVideo;
using SuperSocket.ClientEngine;
using System;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading;
using CefSharp;
using CefClient.Camera;
using CefClient.OrderMessage;

namespace CefClient.SuperSocket
{
    public class OrderSocketClient
    {
        private bool IsConnectioning = false;
        private AsyncTcpSession client;
        private string ip = ConfigurationManager.AppSettings["orderServer"];
        private int port = 8081;
        private byte[] loginBuffer;
        private OrderMessageDecode decode;
        public OrderSocketClient(byte[] buffer)
        {
            decode = new OrderMessageDecode();
            loginBuffer = buffer;
            client = new AsyncTcpSession();
            // 连接断开事件
            client.Closed += client_Closed;
            // 收到服务器数据事件
            client.DataReceived += client_DataReceived;
            // 连接到服务器事件
            client.Connected += client_Connected;
            // 发生错误的处理
            client.Error += client_Error;
            Connect();
        }

        void client_Error(object sender, ErrorEventArgs e)
        {
            Connect();
        }

        void client_Connected(object sender, EventArgs e)
        {
            Send(loginBuffer);
        }

        void client_DataReceived(object sender, DataEventArgs e)
        {
            if (StaticResource.VideoWindowState)
            {
                StaticResource.VideoWindowState = false;
                byte[] buffer = new byte[e.Data.Length];
                Buffer.BlockCopy(e.Data, 0, buffer, 0, e.Data.Length);
                switch (decode.GetMessageHead(buffer))
                {
                    case OrderMessageType.AudioAndVideo:

                        StaticResource.VideoType = OrderMessageType.AudioAndVideo;
                        AudioAndVideo video = decode.AudioAndVideo(buffer);
                        StaticResource.Sim = video.sim;
                        break;
                    case OrderMessageType.HisVideoAndAudio:
                        StaticResource.VideoType = OrderMessageType.HisVideoAndAudio;
                        HisVideoAndAudio hisVideo = decode.HisVideoAndAudio(buffer);
                        StaticResource.Sim = hisVideo.sim;
                        break;
                    case OrderMessageType.MonitorOpen:
                        StaticResource.VideoType = OrderMessageType.MonitorOpen;
                        MonitorOpen open = decode.MonitorOpen(buffer);
                        StaticResource.CameraInfo = buffer;
                        break;
                }
                new Thread(CreatVidoe).Start();
            }
            else
            {
                StaticResource.MessShow("存在视频窗口，请关闭再试");
            }
        }

        void client_Closed(object sender, EventArgs e)
        {
            Connect();
        }

        /// <summary>
        /// 连接到服务器
        /// </summary>
        public void Connect()
        {
            if (IsConnectioning) {
                return;
            }
            IsConnectioning = true;
            try
            {
                client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                while (!client.IsConnected)
                {
                    Thread.Sleep(3000);
                    if (!client.IsConnected)
                    {
                        client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                    }
                }
            }
            catch
            {

            }
            finally {
                IsConnectioning = false;
            }
        }

        /// <summary>
        /// 向服务器发命令行协议的数据
        /// </summary>
        public void Send(byte[] data)
        {
            if (client.IsConnected)
            {
                client.Send(data, 0, data.Length);
            }
        }

        /// <summary>
        /// 执行服务器指令
        /// </summary>
        /// <param name="sim"></param>
        private void CreatVidoe()
        {
            switch (StaticResource.VideoType)
            {
                case OrderMessageType.AudioAndVideo:
                    StaticResource.Live = new LiveWindow();
                    StaticResource.Live.ShowDialog();
                    break;
                case OrderMessageType.HisVideoAndAudio:
                    StaticResource.PlayBack = new PlayBack();
                    StaticResource.PlayBack.ShowDialog();
                    break;
                case OrderMessageType.MonitorOpen:
                    StaticResource.Camera = new CameraWindow();
                    StaticResource.Camera.ShowDialog();
                    break;
                default:
                    break;
            }
        }
    }
}
