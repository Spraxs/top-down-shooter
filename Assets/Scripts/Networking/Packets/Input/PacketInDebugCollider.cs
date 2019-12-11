using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[PacketId(5)]
public class PacketInDebugCollider : PacketIn
{
    public PacketInDebugCollider() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        float x1 = (float) ReadDouble(ms);
        float y1 = (float) ReadDouble(ms);

        float x2 = (float) ReadDouble(ms);
        float y2 = (float) ReadDouble(ms);

        float x3 = (float) ReadDouble(ms);
        float y3 = (float) ReadDouble(ms);

        float x4 = (float) ReadDouble(ms);
        float y4 = (float) ReadDouble(ms);

        Vector2 pointA = new Vector2(x1, y1);

        Vector2 pointB = new Vector2(x2, y2);

        Vector2 pointC = new Vector2(x3, y3);

        Vector2 pointD = new Vector2(x4, y4);

        if (ColliderDebug.Instance != null)
        {
            ColliderDebug.Instance.UpdateDebugDots(pointA, pointB, pointC, pointD);
        }
    }
}
