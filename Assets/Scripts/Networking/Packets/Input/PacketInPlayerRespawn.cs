using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[PacketId(9)]
public class PacketInPlayerRespawn : PacketIn
{
    public PacketInPlayerRespawn() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        long playerId = ReadLong(ms);

        float respawnPositionX = (float) ReadDouble(ms);
        float respawnPositionY = (float) ReadDouble(ms);

        Vector2 respawnPosition = new Vector2(respawnPositionX, respawnPositionY);
    }
}
