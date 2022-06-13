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
    public class AudioSocketClient
    {
        private EasyClient<PackageInfo> client;
        private string ip = ConfigurationManager.AppSettings["orderServer"];
        private int ports;
        private byte[] infos;
        public async void ConnectServer(int port,byte[] info)
        {
            ports = port;
            infos = info;
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
                StaticResource.ShowMessage("语音连接失败...");
            }
        }

        private void OnPackageReceived(object sender, PackageEventArgs<PackageInfo> e)
        {
            if (e.Package.Data.Length > 30)
            {
                StaticResource.OriginalAudio.Enqueue(e.Package.Data);
            }
        }

        private void OnClientConnected(object sender, EventArgs e)
        {
            Send(infos);
        }

        private void OnClientClosed(object sender, EventArgs e)
        {
            if (StaticResource.VideoIsEnd && StaticResource.AudioIsEnd)
            {
                StaticResource.ShowMessage("语音通话中断...");
            }
        }

        private void OnClientError(object sender, ErrorEventArgs e)
        {
            if (StaticResource.VideoIsEnd&& StaticResource.AudioIsEnd)
            {
                  StaticResource.ShowMessage("语音通话中断...");
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
                if (StaticResource.VideoIsEnd && StaticResource.AudioIsEnd)
                {
                    StaticResource.ShowMessage("发送失败...");
                }
            }
        }

        public void Close()
        {
            client.Close();
        }


    }
}
