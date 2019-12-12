using System.IO;
using UnityEngine;

[PacketId(8)]
public class PacketInPlayerDeath : PacketIn
{
    public PacketInPlayerDeath() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        long playerId = ReadLong(ms);

        float deathPositionX = (float) ReadDouble(ms);
        float deathPositionY = (float)ReadDouble(ms);

        Vector2 deathPosition = new Vector2(deathPositionX, deathPositionY);
    }
}
