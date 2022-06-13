using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jt808Library.Nactive
{
    public class Api
    {
        [DllImport("ntdll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern byte* memcpy(
          byte* dst,
          byte* src,
          int count);

        [DllImport("msvcrt.dll")]
        public static extern unsafe int memcmp(void* src, void* dst, int count);
    }
}
