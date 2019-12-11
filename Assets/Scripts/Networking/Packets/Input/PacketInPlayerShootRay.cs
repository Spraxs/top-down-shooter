using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[PacketId(6)]
public class PacketInPlayerShootRay : PacketIn
{
    public PacketInPlayerShootRay() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        long playerId = ReadLong(ms);

        float beginPositionX = (float) ReadDouble(ms);
        float beginPositionY = (float) ReadDouble(ms);

        float endPositionX = (float) ReadDouble(ms);
        float endPositionY = (float) ReadDouble(ms);

        bool hit = ReadByte(ms) != 0;

        Vector2 beginPosition = new Vector2(beginPositionX, beginPositionY);
        Vector2 endPosition = new Vector2(endPositionX, endPositionY);

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            ClientManager.Instance.PlayShootRayEffect(playerId, beginPosition, endPosition, hit);
        });
    }
}
