using CefClient;
using CefClient.CarVideo;
using CefClient.CarVideo.HiH264_DEC;
using CefClient.OrderMessage;
using CefSharp.CarVideo.HiH264_DEC;
using CefSharp.DevTools.Page;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Jt808Library.Utils;
using OpenTK.Graphics.ES20;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ZedGraph;
using static CefSharp.CarVideo.HiH264_DEC.HiH264_DECS;
using Capture = Emgu.CV.Capture;

namespace CefSharp.CarVideo
{
    internal class H264ToImage
    {
        private static LiveWindow.ImageDelegate LivePic;
        private static PlayBack.ImageDelegate PlayBackPic;
        private const int HI_H264DEC_OK = 0;
        private const int HI_H264DEC_NEED_MORE_BITS = -1;
        private const int HI_H264DEC_NO_PICTURE = -2;
        private int Width;
        private int hight;

        /// <summary>
        /// 流解码开始时间
        /// </summary>
        private DateTime decodingStartTime;

        #region 解码器相关声明

        /// <summary>
        /// 数据的句柄
        /// </summary>
        /// <summary>
        /// 这是解码器属性信息
        /// </summary>
        private HiH264_DEC_ATTR_S decAttr;

        /// <summary>
        /// 这是解码器输出图像信息
        /// </summary>
        private static HiH264_DEC_FRAME_S _decodeFrame = new HiH264_DEC_FRAME_S();

        /// <summary>
        /// 解码器句柄
        /// </summary>
        private IntPtr _decHandle;

        private readonly double[,] YUV2RGB_CONVERT_MATRIX = new double[3, 3] { { 1, 0, 1.4022 }, { 1, -0.3456, -0.7145 }, { 1, 1.771, 0 } };

        //解码结束
        [DllImport("hi_h264dec_w.dll", EntryPoint = "Hi264DecCreate", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Hi264DecCreate(ref HiH264_DEC_ATTR_S pDecAttr);

        [DllImport("hi_h264dec_w.dll", EntryPoint = "Hi264DecDestroy", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Hi264DecDestroy(IntPtr hDec);

        /// <summary>
        /// 流解码
        /// </summary>
        /// <param name="hDec">解码器句柄</param>
        /// <param name="pStream">码流起始地址</param>
        /// <param name="iStreamLen">码流长度</param>
        /// <param name="ullPTS">时间戳信息</param>
        /// <param name="pDecFrame">图像信息</param>
        /// <param name="uFlags">解码模式 0：正常解码；1、解码完毕并要求解码器输出残留图像</param>
        /// <returns></returns>
        [DllImport("hi_h264dec_w.dll", EntryPoint = "Hi264DecFrame", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Hi264DecFrame(IntPtr hDec, IntPtr pStream, uint iStreamLen, ulong ullPTS, ref HiH264_DEC_FRAME_S pDecFrame, uint uFlags);

        /// <summary>
        /// 完整帧解码
        /// </summary>
        /// <param name="hDec">解码器句柄</param>
        /// <param name="pStream">码流起始地址</param>
        /// <param name="iStreamLen">码流长度</param>
        /// <param name="ullPTS">时间戳信息</param>
        /// <param name="pDecFrame">图像信息</param>
        /// <param name="uFlags">解码模式 0：正常解码；1、解码完毕并要求解码器输出残留图像</param>
        /// <returns></returns>
        [DllImport("hi_h264dec_w.dll", EntryPoint = "Hi264DecAU", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Hi264DecAU(IntPtr hDec, IntPtr pStream, uint iStreamLen, ulong ullPTS, ref HiH264_DEC_FRAME_S pDecFrame, uint uFlags);

        // private readonly int bufferLen = 0x600;
        private Image<Bgr, byte> image;

        #endregion 解码器相关声明

        /// <summary>
        /// 解码初始化
        /// </summary>
        public void HI_H264DECSInit()
        {
            Thread thread = new Thread(ParseH264)
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void Init(SpsNual sps)
        {
            int MonitorWidth = SystemInformation.PrimaryMonitorSize.Width;
            int MonitorHeight = SystemInformation.PrimaryMonitorSize.Height;
            switch (StaticResource.VideoType)
            {
                case OrderMessageType.AudioAndVideo:
                    LivePic = new LiveWindow.ImageDelegate(LiveWindow.LivePicChange);
                    image = new Image<Bgr, byte>((int)Width, (int)hight);
                    //调整窗体大小
                    LiveWindow.LiveBox.Size = new Size(Width, hight);
                    LiveWindow.LiveWindows.ClientSize = new Size(Width, hight + LiveWindow.LivePanel.Height + 2);
                    LiveWindow.LivePanel.Location = new Point((Width - LiveWindow.LivePanel.Width) / 2, hight + 2);
                    LiveWindow.LiveWindows.Location = new Point((MonitorWidth - LiveWindow.LiveWindows.Width) / 2, (MonitorHeight - LiveWindow.LiveWindows.Height) / 2);
                    break;

                case OrderMessageType.HisVideoAndAudio:
                    PlayBackPic = new PlayBack.ImageDelegate(PlayBack.PlayBackPicChange);
                    image = new Image<Bgr, byte>((int)Width, (int)hight);
                    PlayBack.playBackBox.Size = new Size(Width, hight);
                    PlayBack.PlayBacks.ClientSize = new Size(Width, hight + PlayBack.recordPanel.Height + 2); ;
                    PlayBack.recordPanel.Location = new Point((Width - PlayBack.recordPanel.Width) / 2, hight + 2);
                    PlayBack.PlayBacks.Location = new Point((MonitorWidth - PlayBack.PlayBacks.Width) / 2, (MonitorHeight - PlayBack.PlayBacks.Height) / 2);
                    break;
            }
            decAttr = new HiH264_DEC_ATTR_S
            {
                // 这是解码器属性信息
                uPictureFormat = 0,
                uStreamInType = 0,
                /* 解码器最大图像宽高 */
                uPicWidthInMB = /*(uint)width*/(uint)Width,
                uPicHeightInMB = /*(uint)height*/(uint)hight,
                /* 解码器最大参考帧数:*/
                uBufNum = (uint)sps.max_num_ref_frames,
                /* bit0 = 1: 标准输出模式; bit0 = 0: 快速输出模式 */
                /* bit4 = 1: 启动内部Deinterlace; bit4 = 0: 不启动内部Deinterlace */
                uWorkMode = 0x00
            };
            //创建、初始化解码器句柄
            _decHandle = Hi264DecCreate(ref decAttr);
        }

        /// <summary>
        /// 分离SpsNual
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private byte[] GetSpsNual(byte[] buffer)
        {
            byte[] back;
            int copyIndex;
            int index = 0;
            while (index < buffer.Length - 4)
            {
                if (buffer[index] == 0 && buffer[index + 1] == 0 && buffer[index + 2] == 1)
                {
                    copyIndex = index + 3;
                    if ((buffer[index + 3] & 0x1f) == 0b00111)
                    {
                        int temp = index + 3;
                        while (temp < buffer.Length - 2)
                        {
                            if (buffer[temp] == 0 && buffer[temp + 1] == 0 && buffer[temp + 2] == 1)
                            {
                                back = new byte[temp - index];
                                Buffer.BlockCopy(buffer, copyIndex, back, 0, temp - index);
                                return back;
                            }
                            else
                            {
                                temp++;
                            }
                        }
                        index = temp;
                    }
                    else
                    {
                        index += 3;
                        continue;
                    }
                }
                else if (buffer[index] == 0 && buffer[index + 1] == 0 && buffer[index + 2] == 0 && buffer[index + 3] == 1)
                {
                    copyIndex = index + 4;
                    if ((buffer[index + 4] & 0x1f) == 0b00111)
                    {
                        int temp = index + 4;
                        while (temp < buffer.Length - 3)
                        {
                            if (buffer[temp] == 0 && buffer[temp + 1] == 0 && buffer[temp + 2] == 0 && buffer[index + 3] == 1)
                            {
                                back = new byte[temp - index];
                                Buffer.BlockCopy(buffer, copyIndex, back, 0, temp - index);
                                return back;
                            }
                            else
                            {
                                temp++;
                            }
                        }
                        index = temp;
                    }
                    else
                    {
                        index += 4;
                        continue;
                    }
                }
                else
                {
                    index++;
                }
            }
            return null;
        }

        /// <summary>
        /// 解码流
        /// </summary>
        private void ParseH264()
        {
            ///获取sps序列，解码器初始化
            bool H264DECSInit = false;
            while (!H264DECSInit)
            {
                if (StaticResource.H264.Count > 1)
                {
                    StaticResource.H264.TryPeek(out byte[] tempByte);
                    if (tempByte == null || tempByte.Length == 0)
                    {
                        StaticResource.H264.TryDequeue(out _);
                        continue;
                    }
                    byte[] sps = GetSpsNual(tempByte);
                    if (sps == null || sps.Length == 0)
                    {
                        StaticResource.H264.TryDequeue(out _);
                        continue;
                    }
                    Init(new SpsDecoder().H264_decode_sps(sps, ref Width, ref hight));
                    H264DECSInit = true;
                }
                else
                {
                    Thread.Sleep(2);
                }
            }

            while (StaticResource.VideoIsEnd)
            {
                if (StaticResource.H264.Count < 1)
                {
                    Thread.Sleep(2);
                    continue;
                }
                decodingStartTime = DateTime.Now;
                StaticResource.H264.TryDequeue(out byte[] tempByte);
                if (tempByte == null || tempByte.Length == 0) { continue; }
                IntPtr pData = Marshal.AllocHGlobal(tempByte.Length);
                Marshal.Copy(tempByte, 0, pData, tempByte.Length);
                int result = Hi264DecFrame(_decHandle, pData, (uint)tempByte.Length, 0, ref _decodeFrame, 0);
                while (HI_H264DEC_NEED_MORE_BITS != result)
                {
                    if (HI_H264DEC_NO_PICTURE == result)
                    {
                        break;
                    }
                    try
                    {
                        if (HI_H264DEC_OK == result)/* 输出一帧图像 */
                        {
                            byte[] rgbFrame = new byte[3 * Width * hight];
                            //计算 y u v 的长度
                            var yLength = _decodeFrame.uHeight * _decodeFrame.uYStride;
                            var uLength = _decodeFrame.uHeight * _decodeFrame.uUVStride / 2;
                            var vLength = uLength;

                            var yBytes = new byte[yLength];
                            var uBytes = new byte[uLength];
                            var vBytes = new byte[vLength];
                            //_decodeFrame 是解码后的数据对象，里面包含 YUV 数据、宽度、高度等信息
                            Marshal.Copy(_decodeFrame.pY, yBytes, 0, (int)yLength);
                            Marshal.Copy(_decodeFrame.pU, uBytes, 0, (int)uLength);
                            Marshal.Copy(_decodeFrame.pV, vBytes, 0, (int)vLength);
                            //转为yv12格式
                            byte[] yuvBytes = new byte[yBytes.Length + uBytes.Length + vBytes.Length];
                            Buffer.BlockCopy(yBytes, 0, yuvBytes, 0, yBytes.Length);
                            Buffer.BlockCopy(vBytes, 0, yuvBytes, yBytes.Length, vBytes.Length);
                            Buffer.BlockCopy(uBytes, 0, yuvBytes, yBytes.Length + vBytes.Length, uBytes.Length);

                            //更新显示
                            GCHandle handle = GCHandle.Alloc(yuvBytes, GCHandleType.Pinned);
                            using (Image<Bgr, byte> yv12 = new Image<Bgr, byte>((int)Width, ((int)hight >> 1) * 3, (int)Width, handle.AddrOfPinnedObject()))
                            {
                                CvInvoke.CvtColor(yv12, image, ColorConversion.Yuv2BgrYv12);
                            }
                            ChangePic(image);
                            if (handle.IsAllocated) handle.Free();
                        }
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog("解码错误", e);
                    }
                    /* 继续解码剩余H.264码流 */
                    result = Hi264DecFrame(_decHandle, IntPtr.Zero, 0, 0, ref _decodeFrame, 0);
                }
                StaticResource.prevDecodingStartTime = (DateTime.Now - decodingStartTime).Milliseconds;
            }
            /* 销毁解码器 */
            Hi264DecDestroy(_decHandle);
        }

        private void ChangePic(Image<Bgr, byte> image)
        {
            switch (StaticResource.VideoType)
            {
                case OrderMessageType.AudioAndVideo:
                    LivePic(image);
                    break;

                case OrderMessageType.HisVideoAndAudio:
                    PlayBackPic(image);
                    break;
            }
        }
    }
}