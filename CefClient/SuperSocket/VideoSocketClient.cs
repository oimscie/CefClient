using CefClient.CarVideo;
using CefClient.OrderMessage;
using CefClient.SuperSocket;
using CefSharp;
using CefSharp.CarVideo;
using Jt808Library.JT808PacketBody;
using SuperSocket.ClientEngine;
using System;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading;

namespace CefClient
{
    public class VideoSocketClient
    {
        private EasyClient<PackageInfo> client;
        private string ip = ConfigurationManager.AppSettings["orderServer"];
        private PacketForm PacketForm;

        public async void ConnectServer(int port)
        {
            PacketForm = new PacketForm();
            client = new EasyClient<PackageInfo>
            {
                ReceiveBufferSize = 1024 * 10,
                NoDelay = true
            };
            client.Initialize(new RtpReceiveFilter());
            client.Connected += OnClientConnected;
            client.NewPackageReceived += OnPackageReceived;
            client.Error += OnClientError;
            client.Closed += OnClientClosed;
            var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(ip), port));
            if (!connected)
            {
                StaticResource.ShowMessage("连接失败：原因（1）服务器离线");
            }
        }

        private void OnPackageReceived(object sender, PackageEventArgs<PackageInfo> e)
        {
            if (e.Package.Data.Length > 30)
            {
                StaticResource.OriginalVideo.Enqueue(e.Package.Data);
            }
            else
            {
                StaticResource.ShowMessage(Encoding.UTF8.GetString(e.Package.Data, 0, e.Package.Data.Length - 4));
            }
        }

        private void OnClientConnected(object sender, EventArgs e)
        {
            switch (StaticResource.VideoType)
            {
                case OrderMessageType.AudioAndVideo:
                    Send(PacketForm.Video(StaticResource.AudioAndVideo));
                    break;

                case OrderMessageType.HisVideoAndAudio:
                    StaticResource.HisVideoAndAudio.StartTime = PlayBack.StartTimes.Value.ToString();
                    StaticResource.HisVideoAndAudio.OverTime = PlayBack.StopTime.Value.ToString();
                    Send(PacketForm.HisVideo(StaticResource.HisVideoAndAudio));
                    break;

                default:
                    break;
            }
        }

        private void OnClientClosed(object sender, EventArgs e)
        {
            switch (StaticResource.VideoType)
            {
                case OrderMessageType.AudioAndVideo:
                    if (StaticResource.VideoIsEnd)
                    {
                        StaticResource.ShowMessage("通信终止：可能原因（1）：设备离线（2）：网络中断");
                    }
                    break;

                case OrderMessageType.HisVideoAndAudio:
                    if (StaticResource.VideoIsEnd)
                    {
                        StaticResource.ShowMessage("通信终止：可能原因（1）：设备离线（2）：录像通道被占用");
                    }
                    break;

                default:
                    break;
            }
        }

        private void OnClientError(object sender, ErrorEventArgs e)
        {
            if (StaticResource.VideoIsEnd)
            {
                StaticResource.ShowMessage("信号中断，关闭后重试..");
            }
        }

        public void Send(byte[] data)
        {
            if (client.IsConnected)
            {
                client.Send(data);
            }
            else
            {
                if (StaticResource.VideoIsEnd)
                {
                    StaticResource.ShowMessage("发送失败");
                }
            }
        }

        public void Close()
        {
            client.Close();
        }
    }
}