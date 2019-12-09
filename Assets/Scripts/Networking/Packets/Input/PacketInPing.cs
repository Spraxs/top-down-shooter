using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[PacketId(1)]
public class PacketInPing : PacketIn
{

    public PacketInPing() : base(false)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        long timeInMillis = ReadLong(ms);

        WebManager.Instance.SendPacket(new PacketOutPing(timeInMillis));
    }
}
