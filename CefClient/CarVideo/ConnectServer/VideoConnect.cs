using CefClient;
using CefClient.CarVideo;
using CefClient.SuperSocket;
using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CefSharp.CarVideo
{
    public static class VideoConnect
    {

        private static VideoSocketClient Client;
        private static H264ToImage H264ToImage;
        /// <summary>
        /// 视频启动入口
        /// </summary>
        public static void VideoStart(object obj)
        {
            StaticResource.VideoReset();
            StaticResource.VideoIsEnd = true;
            H264ToImage = new H264ToImage();
            H264ToImage.HI_H264DECSInit();
            ProcessData.VideoProcessDataStart();
            VideoConnectServer((int)obj);
        }
        /// <summary>
        /// 视频关闭
        /// </summary>
        public static void VideoStop()
        {
            StaticResource.VideoIsEnd = false;
            StaticResource.VideoReset();
            H264ToImage = null;
            // Client.Send(Encoding.UTF8.GetBytes(StaticResource.Sim + ",0x9102,0"));
            if(Client!=null) {  Client.Close();} 
            Client = null;
        }
        private static void VideoConnectServer(int port)
        {
            Client = new VideoSocketClient();
            Client.ConnectServer(port);
        }

        public static void hisVideoControl(string info) {
            Client.Send(Encoding.UTF8.GetBytes(info));
        }
    }
}
