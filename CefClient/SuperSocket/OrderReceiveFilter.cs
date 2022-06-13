
using SuperSocket.ProtoBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefClient.SuperSocket
{
    class OrderReceiveFilter : BeginEndMarkReceiveFilter<PackageInfo>
    {
        private readonly static byte[] Mark =Encoding.UTF8.GetBytes("$");
        public OrderReceiveFilter() : base(Mark, Mark) { }

        public override PackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            return new PackageInfo(bufferStream.Buffers[0].ToArray());
        }
    }
}
