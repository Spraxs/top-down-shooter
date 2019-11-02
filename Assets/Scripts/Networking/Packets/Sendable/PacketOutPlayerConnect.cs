using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketOutPlayerConnect : SendablePacket
{
    public PacketOutPlayerConnect()
    {
        WriteShort(0);

        WriteInt(5);

        WriteDouble(1.0D);

        WriteDouble(2.5D);

        WriteLong(CurrentTimeMillis());
    }

    private static readonly DateTime Jan1st1970 = new DateTime
    (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static long CurrentTimeMillis()
    {
        return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
    }
}
