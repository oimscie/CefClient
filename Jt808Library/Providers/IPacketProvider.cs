﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jt808Library.Providers
{
    using Structures;

    public interface IPacketProvider
    {
        byte[] Encode(PacketFrom item);

        PacketMessage Decode(byte[] buffer, int offset, int count);
    }
}
