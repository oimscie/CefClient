using SuperSocket.ProtoBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefClient.SuperSocket
{
   public class PackageInfo : IPackageInfo
    {
        public PackageInfo(byte[] bodyBuffer)
        {    
            Data = bodyBuffer;
        }
        /// <summary>
        /// 服务器返回的字节数据
        /// </summary>
        public byte[] Data { get; set; }

    }
}
