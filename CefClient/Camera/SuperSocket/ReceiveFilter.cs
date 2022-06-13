using Jt808Library.Utils;
using SuperSocket.ProtoBase;
using System;
using System.Linq;

namespace CefClient.SuperSocket
{
    class ReceiveFilter : FixedSizeReceiveFilter<PackageInfo>
    {
        public ReceiveFilter()
          : base(2048)
        {

        }
        public override PackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            byte[] temp = new byte[(int)bufferStream.Length];
            bufferStream.Read(temp,0,(int)bufferStream.Length);
            return new PackageInfo(temp);
        }
    }
}
