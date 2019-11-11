using System;

public class PacketOutPlayerConnect : PacketOut
{
    public override void onDataPrepare()
    {
        id = 0; // TODO set this value with attribute
    }
}
