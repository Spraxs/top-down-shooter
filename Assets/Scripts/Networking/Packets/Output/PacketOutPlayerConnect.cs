using System;

public class PacketOutPlayerConnect : PacketOut
{

    public string playerName;

    public double sizeX;
    public double sizeY;

    public override void onDataPrepare()
    {
        id = 0; // TODO set this value with attribute

        playerName = "Guest";

        sizeX = 0.64f;
        sizeY = 1.28f;
    }
}
