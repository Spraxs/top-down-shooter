using System.IO;
using UnityEngine;

[PacketId(13)]
public class PacketInGameModeLose : PacketIn
{
    public PacketInGameModeLose() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {

    }
}
