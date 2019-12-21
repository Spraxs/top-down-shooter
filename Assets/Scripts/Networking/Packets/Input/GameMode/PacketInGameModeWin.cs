using System.IO;
using UnityEngine;

[PacketId(12)]
public class PacketInGameModeWin : PacketIn
{
    public PacketInGameModeWin() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        
    }
}
