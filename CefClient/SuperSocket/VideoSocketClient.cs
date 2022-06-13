using CefClient.CarVideo;
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
        private int ports;
        public async void ConnectServer(int port)
        {
            ports = port;
            client = new EasyClient<PackageInfo>
            {
                ReceiveBufferSize =1024*10,
                NoDelay = true
            };
            client.Initialize(new RtpReceiveFilter());
            client.Connected += OnClientConnected;
            client.NewPackageReceived += OnPackageReceived;
            client.Error += OnClientError;
            client.Closed += OnClientClosed;
            var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(ip), ports));
            if (!connected)
            {
                StaticResource.ShowMessage("连接失败：原因（1）服务器离线");
            }
        }

        private void OnPackageReceived(object sender, PackageEventArgs<PackageInfo> e)
        {
            if (e.Package.Data.Length >30)
            {
                StaticResource.OriginalVideo.Enqueue(e.Package.Data);
            }
        }

        private void OnClientConnected(object sender, EventArgs e)
        {
            switch (StaticResource.VideoType)
            {
                case "vehicleLive":
                    Send(Encoding.UTF8.GetBytes("$video!" + StaticResource.Sim + "!1!1$"));
                    break;
                case "vehiclePlayBack":
                    Send(Encoding.UTF8.GetBytes("$hisVideo!" + StaticResource.Sim + "!2!" + PlayBack.StartTimes.Value.ToString() + "!" + PlayBack.StopTime.Value.ToString() + "!1$"));
                    break;
                default:
                    break;
            }
        }

        private void OnClientClosed(object sender, EventArgs e)
        {
            switch (StaticResource.VideoType)
            {
                case "vehicleLive":
                    StaticResource.ShowMessage("通信终止：可能原因（1）：设备离线（2）：网络中断");
                    break;
                case "vehiclePlayBack":
                    StaticResource.ShowMessage("通信终止：可能原因（1）：设备离线（2）：录像通道被占用");
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
