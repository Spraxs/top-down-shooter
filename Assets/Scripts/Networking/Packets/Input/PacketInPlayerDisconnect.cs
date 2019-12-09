using System.IO;

[PacketId(3)]
public class PacketInPlayerDisconnect : PacketIn
{

    public PacketInPlayerDisconnect() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        long playerId = ReadLong(ms);

        UnityMainThreadDispatcher.Instance().Enqueue(() => { ClientManager.Instance.RemoveClient(playerId); });
    }
}
