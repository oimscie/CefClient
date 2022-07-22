using CefClient;
using CefClient.Camera;
using CefClient.CarVideo;
using CefClient.OrderMessage;
using Jt808Library.JT808PacketBody;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CefSharp
{
    class StaticResource
    {
        public static object state = new object();
        /// <summary>
        /// 客户端唯一ID
        /// </summary>
        public static string uuid;
        /// <summary>
        /// 视频窗口指示器
        /// </summary>
        public static bool VideoWindowState = true;
        /// <summary>
        /// 终端SIM号
        /// </summary>
        public static string Sim;
        /// <summary>
        /// 终端1078版本
        /// </summary>
        public static string Version1078;
        /// <summary>
        /// 视频类型
        /// </summary>
        public static string VideoType;
        /// <summary>
        /// 视频播放倍速
        /// </summary>
        public static int VideoMultiple;
        /// <summary>
        /// 视频原始rtp数据队列
        /// </summary>
        public static ConcurrentQueue<byte[]> OriginalVideo;
        /// <summary>
        /// 音频原始rtp数据队列
        /// </summary>
        public static ConcurrentQueue<byte[]> OriginalAudio;
        /// <summary>
        ///  H264数据队列
        /// </summary>
        public static ConcurrentQueue<byte[]> H264;
        /// <summary>
        /// 音频帧队列
        /// </summary>1
        public static ConcurrentQueue<byte[]> AudioFrame;
        /// <summary>
        /// 录音帧队列
        /// </summary>1
        public static ConcurrentQueue<byte[]> RecorderQueue;
        public static bool AudioIsEnd = false;
        public static bool VideoIsEnd = false;
        public static MainWindow.Messboxdelegates MessShow = new MainWindow.Messboxdelegates(MainWindow.MessboxShow);
        public static LiveWindow.Messboxdelegates LiveMessShow = new LiveWindow.Messboxdelegates(LiveWindow.MessboxShow);
        public static PlayBack.Messboxdelegates PlayBackMessShow = new PlayBack.Messboxdelegates(PlayBack.MessboxShow);
        public static CameraWindow.Messboxdelegates CameraMessShow = new CameraWindow.Messboxdelegates(CameraWindow.MessboxShow);
        /// <summary>
        /// 监控视频数原始数据
        /// </summary>
        public static ConcurrentQueue<byte[]> CameraOriginalvideo;
        /// <summary>
        /// 监控视频帧视频数据队列
        /// </summary>
        public static ConcurrentQueue<ValueTuple<IntPtr, uint>> video;
        /// <summary>
        /// 监控信息
        /// </summary>
        public static byte[] CameraInfo;
        /// <summary>
        /// 上一段视频流解用时，毫秒
        /// </summary>
        public static int prevDecodingStartTime;
        public static bool CameraVideoIsEnd;
        public static LiveWindow Live;
        public static PlayBack PlayBack;
        public static CameraWindow Camera;
        /// <summary>
        /// 重置监控视频数据队列
        /// </summary>
        public static void CameraVideoQueueReset()
        {
            video = new ConcurrentQueue<ValueTuple<IntPtr, uint>>();
            CameraOriginalvideo = new ConcurrentQueue<byte[]>();
        }
        /// <summary>
        /// 重置视频数据队列
        /// </summary>
        public static void VideoReset()
        {
            OriginalAudio = new ConcurrentQueue<byte[]>();
            OriginalVideo = new ConcurrentQueue<byte[]>();
            AudioFrame = new ConcurrentQueue<byte[]>();
            RecorderQueue = new ConcurrentQueue<byte[]>();
            H264= new ConcurrentQueue<byte[]>();
        }
        /// <summary>
        /// 重置音频数据队列
        /// </summary>
        public static void AudioReset()
        {
            OriginalAudio = new ConcurrentQueue<byte[]>();
            AudioFrame = new ConcurrentQueue<byte[]>();
            RecorderQueue = new ConcurrentQueue<byte[]>();
        }
        public static void ShowMessage(string text) {
            switch (VideoType)
            {
                case OrderMessageType.AudioAndVideo:
                    LiveMessShow(text);
                    break;
                case OrderMessageType.HisVideoAndAudio:
                    PlayBackMessShow(text);
                    break;
                case OrderMessageType.MonitorOpen:
                    CameraMessShow(text);
                    break;
            }
        }
    }
}
