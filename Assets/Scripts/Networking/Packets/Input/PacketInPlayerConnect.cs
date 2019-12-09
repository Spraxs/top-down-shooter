using System.IO;

[PacketId(0)]
public class PacketInPlayerConnect : PacketIn
{

    public PacketInPlayerConnect() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        long playerId = ReadLong(ms);
        string playerName = ReadString(ms);

        double posX = ReadDouble(ms);
        double posY = ReadDouble(ms);

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            ClientManager.Instance.CreateClient(playerId, playerName, (float)posX, (float)posY);
        });
    }
}
