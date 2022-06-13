using Jt808Library.JT808PacketBody;
using Jt808Library.Utils;
using System;

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
                item.SIM = msgBody.Copy(indexOffset += 2, 6);
                item.ID = msgBody[indexOffset += 6];
                item.type = msgBody[indexOffset += 1];
                item.Time = msgBody.Copy(indexOffset += 1, 8);
                if (BitConvert.ByteToBit(item.type).Substring(0, 4) == "0011")
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

                    if (msgBody.Length - item.length == 30 || msgBody.Length - item.length == 34)
                    {
                        item.data = msgBody.Copy(indexOffset + 2, item.length);
                    }
                    else {
                        item.data = nullPacket;
                    }
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
