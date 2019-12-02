using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketOutPlayerDisconnect : PacketOut
{
    public long playerId;

    public double posX;
    public double posY;

    public PacketOutPlayerDisconnect(double posX, double posY)
    {
        this.posX = posX;
        this.posY = posY;
    }

    public override void onDataPrepare()
    {
        id = 2;
    }
}
