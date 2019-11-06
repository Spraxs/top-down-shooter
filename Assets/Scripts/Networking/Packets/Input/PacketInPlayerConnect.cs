using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[PacketId(0)]
public class PacketInPlayerConnect : PacketIn
{

    public int level;

    public double speed;

    public double damage;

    public long time;

    public PacketInPlayerConnect(MemoryStream memoryStream) : base(memoryStream)
    {
    }

    public override void OnDataHandled()
    {
        Debug.Log("Level: " + level);
        Debug.Log("Speed: " + speed);
        Debug.Log("Damage: " + damage);
        Debug.Log("Time: " + (CurrentTimeMillis() - time));
    }

    private static readonly DateTime Jan1st1970 = new DateTime
(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static long CurrentTimeMillis()
    {
        return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
    }
}
