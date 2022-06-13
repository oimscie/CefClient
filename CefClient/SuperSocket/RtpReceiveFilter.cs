using Jt808Library.Utils;
using SuperSocket.ProtoBase;

using System;
using System.Linq;
using System.Text;

namespace CefClient.SuperSocket
{
    class RtpReceiveFilter : TerminatorReceiveFilter<PackageInfo>
    {
        private static readonly byte[] Terminator = new byte[] { 11, 22, 33, 44 };
        public RtpReceiveFilter()
          : base(Terminator)
        {

        }
        public override PackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            return new PackageInfo(bufferStream.Buffers[0].ToArray());
        }
    }
}
