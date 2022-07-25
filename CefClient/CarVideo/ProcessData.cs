using CefClient;
using CefClient.CarVideo;
using CefClient.CarVideo.ConnectServer;
using CefClient.OrderMessage;
using CefClient.SuperSocket;
using CefSharp.CarVideo.Naudio;
using Jt808Library.JT808PacketBody;
using Jt808Library.Utils;
using NAudio.Codecs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using static Jt808Library.Structures.EquipVersion;

namespace CefSharp.CarVideo
{
    /// <summary>
    /// 音视频数据处理器
    /// </summary>
    public static class ProcessData
    {
        /// <summary>
        /// 包序号
        /// </summary>
        private static ushort index;
        /// <summary>
        /// 时间戳
        /// </summary>
        private static long time;
        /// <summary>
        /// 上一帧接收时间
        /// </summary>
        private static DateTime prevStamp = new DateTime(1970, 1, 1, 0, 0, 0);
        /// <summary>
        /// PCM组
        /// </summary>
        private static byte[] PCM;
        private static byte[] G711A;
        /// <summary>
        /// 海思头
        /// </summary>
        private static readonly byte[] his = new byte[] { 0, 1, 160, 0 };
        /// <summary>
        /// 指令截止符
        /// </summary>
        private static readonly byte[] abort = new byte[] { 11, 22, 33, 44 };
        private static RtpPacket Audiobody;
        private static RtpPacket Videobody;
        private static RtpDecode AudioRtpDecode = new RtpDecode();
        private static RtpDecode VideoRtpDecode = new RtpDecode();
        /// <summary>
        /// 视频数据处理器启动入口，前置初始化视频解析器，连接到服务器，
        /// </summary>
        public static void VideoProcessDataStart()
        {
            Thread thread1 = new Thread(SeparationVideoData)
            {
                IsBackground = true
            };
            thread1.Start();
        }
        /// <summary>
        /// 音频数据处理器启动入口，前置初始化播放器与录音器，连接到服务器
        /// </summary>
        public static void AudioProcessDataStart()
        {
            Thread thread3 = new Thread(SeparationAudioData)
            {
                IsBackground = true
            };
            thread3.Start();
            Thread thread4 = new Thread(Playaudio)
            {
                IsBackground = true
            };
            thread4.Start();
            if (StaticResource.VideoType == OrderMessageType.AudioAndVideo)
            {
                Thread thread5 = new Thread(DecodeAudio)
                {
                    IsBackground = true
                };
                thread5.Start();
            }
        }
        /// <summary>
        /// 音频RTP组装发送
        /// </summary>
        private static void DecodeAudio()
        {
            index = 0;
            //音频基础时间戳
            time = 1000000000000000;
            while (StaticResource.AudioIsEnd)
            {
                if (StaticResource.RecorderQueue.Count > 1)
                {
                    StaticResource.RecorderQueue.TryDequeue(out byte[] AudioByte);
                    StaticResource.RecorderQueue.TryDequeue(out byte[] AudioByte2);
                    try
                    {
                        PCM = AudioByte.Concat(AudioByte2).ToArray();
                        byte[] temp;
                        for (int i = 0; i < 5; i++)
                        {
                            if (StaticResource.Version1078 == Version_1078.Ver_1078_2019)
                            {
                                G711A = new byte[328];
                                temp = Extension.ToBCD(StaticResource.Sim);
                            }
                            else {
                                G711A = new byte[324];
                                temp = Extension.ToBCD(StaticResource.Sim.Substring(8,12));
                            }
                            G711A = his.Concat(Encode(PCM.Skip(i * 640).Take(640).ToArray(), 0, 640)).ToArray();
                            byte[] RtpBuffer = RtpEncode.Encode(new RTPBody()
                            {
                                state = new byte[] { 48, 49, 99, 100 },
                                Vpxc = 129,
                                MPT = 134,
                                index = index,
                                hSimNumber = temp,
                                chanle = 6,
                                type = 51,
                                time = split(time),
                                length = 324,
                                data = G711A
                            });
                            index += 1;
                            time += 20;
                            AudioConnect.AudioSocket.Send(RtpBuffer.Concat(abort).ToArray());
                            Thread.Sleep(19);
                        }
                        byte[] split(long times)
                        {
                            int i = 0;
                            List<byte> list = new List<byte>();
                            string str = times.ToString();
                            while (i < 16)
                            {
                                list.Add(byte.Parse(str.Substring(i, 2)));
                                i += 2;
                            }
                            return list.ToArray();
                        }

                    }
                    catch
                    {
                    }

                }
            }
        }
        /// <summary>
        /// G711转换PCM加入播放队列
        /// </summary>
        private static void Playaudio()
        {
            while (StaticResource.AudioIsEnd)
            {
                if (StaticResource.AudioFrame.Count > 0)
                {
                    try
                    {
                        StaticResource.AudioFrame.TryDequeue(out byte[] AudioByte);
                        if (AudioByte.Length > 10000 || AudioByte == null) continue;
                        byte[] G711data = AudioByte.Skip(4).ToArray();
                        byte[] PCMbuffer = Decode(G711data, 0, G711data.Length);
                        AudioPlay.AddDataToBufferedWaveProvider(PCMbuffer, 0, PCMbuffer.Length);
                    }
                    catch { }

                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
        /// <summary>
        /// 处理原始RTP包,分离G711码流
        /// </summary>
        private static void SeparationAudioData()
        {
            while (StaticResource.AudioIsEnd)
            {
                if (StaticResource.OriginalAudio.Count > 0)
                {
                    StaticResource.OriginalAudio.TryDequeue(out byte[] temp);
                    Audiobody = AudioRtpDecode.Decode(temp);
                    StaticResource.AudioFrame.Enqueue(Audiobody.data);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }

        }
        /// <summary>
        /// 处理原始RTP包,分离H264码流
        /// </summary>
        private static void SeparationVideoData()
        {
            while (StaticResource.VideoIsEnd)
            {
                if (StaticResource.OriginalVideo.Count > 0)
                {
                    StaticResource.OriginalVideo.TryDequeue(out byte[] temp);
                    Videobody = VideoRtpDecode.Decode(temp);
                    //检查是否是实时视频，实时视频直接送入播放队列
                    if (StaticResource.VideoType ==OrderMessageType.AudioAndVideo)
                    {
                        StaticResource.H264.Enqueue(Videobody.data);
                        continue;
                    }
                    string info = BitConvert.ByteToBit(Videobody.type);
                    //判断是否是音频或透传数据，部分设备上传录像时会携带音频包
                    if (info.Substring(0, 4) == "0011"|| info.Substring(0, 4) == "0100")
                    {
                        continue;
                    }
           //判断是否是原子包
                    if (info.Substring(4, 4) == "0000")
                    {
                        StaticResource.H264.Enqueue(Videobody.data);
                        prevStamp = DateTime.Now;
                        continue;
                    }
                    //判断是否是最后一个分包
                    if (info.Substring(4, 4) == "0001")
                    {
                        //本地当前帧与上一帧接收时间差
                        int timeCount = (DateTime.Now - prevStamp).Milliseconds;
                        //判断上一帧解码时间+与本地当前帧与上一帧接收时间差是否小于终端上传的与上一帧的时间差
                        if (timeCount+ StaticResource.prevDecodingStartTime < Videobody.Last_F)
                        {
                            //休眠时间=终端上传的与上一帧时间差-本地当前帧与上一帧接收时间差-上一帧解码用时
                            int time =Videobody.Last_F - timeCount - StaticResource.prevDecodingStartTime;
                            Thread.Sleep((int)Math.Floor((double)time / StaticResource.VideoMultiple));
                        }
                        StaticResource.H264.Enqueue(Videobody.data);
                        prevStamp = DateTime.Now;
                        continue;
                    }
                    StaticResource.H264.Enqueue(Videobody.data);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        /// <summary>
        /// pcm->G711A
        /// </summary>
        /// <param name="g711Buffer"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static byte[] Encode(byte[] data, int offset, int length)
        {
            byte[] encoded = new byte[length / 2];
            int outIndex = 0;
            for (int n = 0; n < length; n += 2)
            {
                encoded[outIndex++] = ALawEncoder.LinearToALawSample(BitConverter.ToInt16(data, offset + n));
            }
            return encoded;
        }

        /// <summary>
        /// G711->pcm
        /// </summary>
        /// <param name="g711Buffer"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static byte[] Decode(byte[] data, int offset, int length)
        {
            byte[] decoded = new byte[length * 2];
            int outIndex = 0;
            for (int n = 0; n < length; n++)
            {
                short decodedSample = ALawDecoder.ALawToLinearSample(data[n + offset]);
                decoded[outIndex++] = (byte)(decodedSample & 0xFF);
                decoded[outIndex++] = (byte)(decodedSample >> 8);
            }
            return decoded;
        }
    }
}
