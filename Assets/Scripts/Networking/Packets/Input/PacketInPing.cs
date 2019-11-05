using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[PacketId(1)]
public class PacketInPing : PacketIn
{

    public long timeInMillis;

    public PacketInPing(MemoryStream memoryStream) : base(memoryStream)
    {

    }
    public override void OnDataHandled()
    {
        WebManager.Instance.SendPacket(new PacketOutPing(timeInMillis));
    }
}
