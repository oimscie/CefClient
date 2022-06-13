using NAudio.Wave;
using System;
using System.Threading;

namespace CefSharp.CarVideo.Naudio
{
    public static class AudioRecoder
    {
        //创建输入对象
        private static readonly WaveInEvent waveIn = new WaveInEvent();
        private static WaveFormat WaveFormat = null;
        private static bool bools = true;
        /// <summary>
        /// 录音器注册
        /// </summary>
        private static void WaveInInit()
        {
            //输入音频参数设置 8k/16位/通道1
            WaveFormat = new WaveFormat(8000, 16, 1);
            //为wavein的回调函数添加事件，用于操作数据
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(WaveIn_DataAvailable);

        }
        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            // e.Buffer为用于操作的数组，类型为byte,长度1600
            StaticResource.RecorderQueue.Enqueue(e.Buffer);
        }
        /// <summary>
        /// 录音器初始化
        /// </summary>
        public static void StartRecording()
        {
            if (StaticResource.VideoType == "vehicleLive") {
                Thread thread = new Thread(RecordingInit)
                {
                    IsBackground = true
                };
                thread.Start();
            }         
        }
        /// <summary>
        /// 录音初始化
        /// </summary>
        private static void RecordingInit()
        {
            try
            {
                if (bools)
                {
                    WaveInInit();
                    bools = false;
                }
                waveIn.StartRecording();
            }
            catch
            {
                StaticResource.MessShow("未能找到麦克风");
            }
        }
        /// <summary>
        /// 结束录音
        /// </summary>
        public static void waveInCancellation()
        {
            try
            {
                waveIn.StopRecording();
            }
            catch
            {

            }

        }
    }
}
