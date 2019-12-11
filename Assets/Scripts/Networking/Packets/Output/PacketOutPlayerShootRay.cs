using UnityEngine;

public class PacketOutPlayerShootRay : PacketOut
{
    public double rayPositionX;
    public double rayPositionY;

    public double rayDirectionX;
    public double rayDirectionY;

    public PacketOutPlayerShootRay(Vector2 rayPosition, Vector2 rayDirection)
    {
        rayPositionX = rayPosition.x;
        rayPositionY = rayPosition.y;

        rayDirectionX = rayDirection.x;
        rayDirectionY = rayDirection.y;
    }

    public override void onDataPrepare()
    {
        id = 6;
    }
}
