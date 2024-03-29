﻿using CefClient.CarVideo;
using SuperSocket.ClientEngine;
using System;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading;
using CefSharp;
using CefClient.Camera;
namespace CefClient.SuperSocket
{
    public class OrderSocketClient
    {
        private bool IsConnectioning = false;
        private AsyncTcpSession client;
        private string ip = ConfigurationManager.AppSettings["orderServer"];
        private int port = 8081;
        private string info;
        private byte[] data;
        public OrderSocketClient(string infos)
        {
            info = infos;
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
            Send(Encoding.UTF8.GetBytes(info));
        }

        void client_DataReceived(object sender, DataEventArgs e)
        {
            if (StaticResource.VideoWindowState)
            {
                StaticResource.VideoWindowState = false;
                data = new byte[e.Data.Length];
                Buffer.BlockCopy(e.Data, 0, data, 0, e.Data.Length);
                string[] info = Encoding.UTF8.GetString(data).Split('!');
                switch (info[0])
                {
                    case "vehicleLive":
                        StaticResource.VideoType = "vehicleLive";
                        StaticResource.Sim = info[1];
                        break;
                    case "vehiclePlayBack":
                        StaticResource.VideoType = "vehiclePlayBack";
                        StaticResource.Sim = info[1];
                        break;
                    case "monitorOpen":
                        StaticResource.VideoType = "monitorOpen";
                        StaticResource.CameraInfo = data;
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
                case "vehicleLive":
                    StaticResource.Live = new LiveWindow();
                    StaticResource.Live.ShowDialog();
                    break;
                case "vehiclePlayBack":
                    StaticResource.PlayBack = new PlayBack();
                    StaticResource.PlayBack.ShowDialog();
                    break;
                case "monitorOpen":
                    StaticResource.Camera = new CameraWindow();
                    StaticResource.Camera.ShowDialog();
                    break;
                default:
                    break;
            }
        }
    }
}
