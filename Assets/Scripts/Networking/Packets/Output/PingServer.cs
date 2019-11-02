using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingServer : PacketOut
{
    public PingServer(long time)
    {
        WriteShort(1);

        WriteLong(time);
    }
}
