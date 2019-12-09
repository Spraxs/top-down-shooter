using System.IO;

[PacketId(4)]
public class PacketInPlayerPositionChange : PacketIn
{

    public PacketInPlayerPositionChange() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {

        long playerId = ReadLong(ms);

        double posX = ReadDouble(ms);
        double posY = ReadDouble(ms);


        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            ClientManager.Instance.MoveClient(playerId, (float) posX, (float) posY);
        });
    }
}
