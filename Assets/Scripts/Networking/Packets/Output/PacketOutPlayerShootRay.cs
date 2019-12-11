using UnityEngine;

public class PacketOutPlayerShootRay : PacketOut
{
    public double rayPositionX;
    public double rayPositionY;

    public double rayDirectionX;
    public double rayDirectionY;

    public double rayEndPositionX;
    public double rayEndPositionY;

    public byte hit;

    public PacketOutPlayerShootRay(Vector2 rayPosition, Vector2 rayDirection, Vector2 rayEndPosition, bool isHit)
    {
        rayPositionX = rayPosition.x;
        rayPositionY = rayPosition.y;

        rayDirectionX = rayDirection.x;
        rayDirectionY = rayDirection.y;

        rayEndPositionX = rayEndPosition.x;
        rayEndPositionY = rayEndPosition.y;

        hit = isHit ? (byte) 1 : (byte) 0;
    }

    public override void onDataPrepare()
    {
        id = 6;
    }
}
