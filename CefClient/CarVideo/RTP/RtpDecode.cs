using Jt808Library.JT808PacketBody;
using Jt808Library.Utils;
using System;
using static Jt808Library.Structures.EquipVersion;

namespace CefSharp.CarVideo
{
    public class RtpDecode
    {
        private RtpPacket item;
        private byte[] nullPacket = new byte[] { };
        public RtpPacket Decode(byte[] msgBody)
        {
            item = new RtpPacket();
            try
            {
                int indexOffset = 0;
                item.state = msgBody.ToUInt32(indexOffset += 4);
                item.V_P_X_CC = msgBody[indexOffset];
                item.M_PT = msgBody[indexOffset += 1];
                item.num = msgBody.ToUInt16(indexOffset += 1);
                //兼容粤标10位
                if (StaticResource .Version1078==Version_1078.Ver_1078_2019)
                {
                    item.SIM = msgBody.Copy(indexOffset += 2, 10);
                    item.ID = msgBody[indexOffset += 10];
                }
                else
                {
                    byte[] temp = new byte[10];
                    Buffer.BlockCopy(msgBody, indexOffset += 2, temp, 4, 6);
                    item.SIM = temp;
                    item.ID = msgBody[indexOffset += 6];
                }
                item.type = msgBody[indexOffset += 1];
                item.Time = msgBody.Copy(indexOffset += 1, 8);
                if ((byte)(item.type>>4) == 0b0011)
                {
                    //音频
                    item.length = msgBody.ToUInt16(indexOffset += 8);

                    item.data = msgBody.Copy(indexOffset + 2, item.length);
                }
                else
                {
                    //视频
                    item.Last_I_F = msgBody.ToUInt16(indexOffset += 8);
                    item.Last_F = msgBody.ToUInt16(indexOffset += 2);
                    item.length = msgBody.ToUInt16(indexOffset += 2);
                    item.data = msgBody.Copy(indexOffset + 2, item.length);
                }
                return item;
            }
            catch
            {
                item.data = nullPacket;
                return item;
            }
        }
    }
}
