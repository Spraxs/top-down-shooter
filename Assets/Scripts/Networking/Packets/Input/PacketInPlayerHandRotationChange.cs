using System.IO;

[PacketId(16)]
public class PacketInPlayerHandRotationChange : PacketIn
{

    public PacketInPlayerHandRotationChange() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {

        long playerId = ReadLong(ms);

        double posX = ReadDouble(ms);
        double posY = ReadDouble(ms);
        
        double rotZ = ReadDouble(ms);

        double scaleX = ReadDouble(ms);


        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            ClientManager.Instance.MoveClientHand(playerId, (float) posX, (float) posY, (float) rotZ, (float) scaleX);
        });
    }
}
