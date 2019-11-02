using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingServer : SendablePacket
{
    public PingServer(long time)
    {
        WriteShort(1);

        WriteLong(time);
    }
}
