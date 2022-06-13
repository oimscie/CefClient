using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp.CarVideo.HiH264_DEC
{
   public class HiH264_DECS
    {
        /// <summary>
        /// 解码器属性信息。
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct HiH264_DEC_ATTR_S
        {
            /// <summary>
            /// 解码器输出图像格式，目前解码库只支持YUV420图像格式
            /// </summary>
            public uint uPictureFormat;
            /// <summary>
            /// 输入码流格式 0x00: 目前解码库只支持以“00 00 01”为nalu分割符的流式H.264码流 
            /// </summary>
            public uint uStreamInType;
            /// <summary>
            /// 图像宽度
            /// </summary>
            public uint uPicWidthInMB;
            /// <summary>
            /// 图像高度
            /// </summary>
            public uint uPicHeightInMB;
            /// <summary>
            /// 参考帧数目
            /// </summary>
            public uint uBufNum;
            /// <summary>
            /// 解码器工作模式
            /// </summary>
            public uint uWorkMode;
            /// <summary>
            /// 用户私有数据
            /// </summary>
            public IntPtr pUserData;
            /// <summary>
            /// 保留字
            /// </summary>
            public uint uReserved;

        }

        /// <summary>
        /// 解码器输出图像信息数据结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct HiH264_DEC_FRAME_S
        {
            /// <summary>
            /// Y分量地址
            /// </summary>
            public IntPtr pY;
            /// <summary>
            /// U分量地址
            /// </summary>
            public IntPtr pU;
            /// <summary>
            /// V分量地址
            /// </summary>
            public IntPtr pV;
            /// <summary>
            /// 图像宽度(以像素为单位)
            /// </summary>
            public uint uWidth;
            /// <summary>
            /// 图像高度(以像素为单位)
            /// </summary>
            public uint uHeight;
            /// <summary>
            /// 输出Y分量的stride (以像素为单位)
            /// </summary>
            public uint uYStride;
            /// <summary>
            /// 输出UV分量的stride (以像素为单位)
            /// </summary>
            public uint uUVStride;
            /// <summary>
            /// 图像裁减信息:左边界裁减像素数
            /// </summary>
            public uint uCroppingLeftOffset;
            /// <summary>
            /// 图像裁减信息:右边界裁减像素数
            /// </summary>
            public uint uCroppingRightOffset;
            /// <summary>
            /// 图像裁减信息:上边界裁减像素数
            /// </summary>
            public uint uCroppingTopOffset;
            /// <summary>
            /// 图像裁减信息:下边界裁减像素数
            /// </summary>
            public uint uCroppingBottomOffset;
            /// <summary>
            /// 输出图像在dpb中的序号
            /// </summary>
            public uint uDpbIdx;
            /// <summary>
            /// 图像类型：0:帧; 1:顶场; 2:底场 */
            /// </summary>
            public uint uPicFlag;
            /// <summary>
            /// 图像类型：0:帧; 1:顶场; 2:底场 */
            /// </summary>
            public uint bError;
            /// <summary>
            /// 图像是否为IDR帧：0:非IDR帧;1:IDR帧
            /// </summary>
            public uint bIntra;
            /// <summary>
            /// 时间戳
            /// </summary>
            public ulong ullPTS;
            /// <summary>
            /// 图像信号
            /// </summary>
            public uint uPictureID;
            /// <summary>
            /// 保留字
            /// </summary>
            public uint uReserved;
            /// <summary>
            /// 指向用户私有数据
            /// </summary>
            public IntPtr pUserData;

        }
    }
}
