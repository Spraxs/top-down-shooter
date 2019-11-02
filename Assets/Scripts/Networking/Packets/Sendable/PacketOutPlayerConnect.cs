using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketOutPlayerConnect : SendablePacket
{
    public PacketOutPlayerConnect()
    {
        WriteShort(0);

        WriteInt(5);
    }
}
