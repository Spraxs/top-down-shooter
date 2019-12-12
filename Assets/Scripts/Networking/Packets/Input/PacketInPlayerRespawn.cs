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

        float reSpawnPositionX = (float) ReadDouble(ms);
        float reSpawnPositionY = (float) ReadDouble(ms);

        Vector2 reSpawnPosition = new Vector2(reSpawnPositionX, reSpawnPositionY);

        Debug.Log("Test respawn.");

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            ClientManager.Instance.HandleClientReSpawn(playerId, reSpawnPosition);
        });
    }
}
