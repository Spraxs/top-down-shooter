using UnityEngine;

public class PacketOutPlayerPositionChange : PacketOut
{

    public double posX;
    public double posY;

    public PacketOutPlayerPositionChange(double _posX, double _posY)
    {
        posX = _posX;
        posY = _posY;
    }

    public override void onDataPrepare()
    {
        id = 4;
    }
}
