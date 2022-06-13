using NAudio.Wave;
using System.Threading;

namespace CefSharp.CarVideo.Naudio
{
    public static class AudioPlay
    {
        private static Thread AudioThread;
        private static WaveOut waveOut;            //播放器
        private static BufferedWaveProvider bufferedWaveProvider;       //5s缓存区
        private static WaveFormat wf;
        private static bool bools = true;
        /// <summary>
        /// Naudio播放器初始化
        /// </summary>
        /// 
        private static void WaveOutInit()
        {
            try
            {
                if (bools)
                {
                    waveOut = new WaveOut();
                    wf = new WaveFormat(8000, 16, 1);
                    bufferedWaveProvider = new BufferedWaveProvider(wf);
                    waveOut.Init(bufferedWaveProvider);
                    bools = false;
                }
                waveOut.Play();
            }
            catch
            {
                StaticResource.ShowMessage("未能找到播放器");
            }
        }
        /// <summary>
        /// 音频播放初始化
        /// </summary>
        public static void AudioPaly()
        {
            AudioThread = new Thread(WaveOutInit)
            {
                IsBackground = true
            };
            AudioThread.Start();
        }
        /// <summary>
        /// 向音频缓存区中添加数据，不要将缓存区填满
        /// </summary>
        /// <param name="data">要填入的数据</param>
        /// <param name="position">数据的起始位置</param>
        /// <param name="len">数据的长度</param>
        public static void AddDataToBufferedWaveProvider(byte[] data, int position, int len)
        {
            bufferedWaveProvider.AddSamples(data, position, len);
        }

        /// <summary>
        /// 播放器关闭
        /// </summary>
        public static void waveOutCancellation()
        {
            try
            {
                waveOut.Stop();
                bufferedWaveProvider.ClearBuffer();
            }
            catch
            {
            }
        }


    }
}
