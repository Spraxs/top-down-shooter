using System.IO;

[PacketId(3)]
public class PacketInPlayerDisconnect : PacketIn
{
    public long playerId;

    public PacketInPlayerDisconnect(MemoryStream memoryStream) : base(memoryStream)
    {
    }

    public override void OnDataHandled()
    {
        ClientManager.Instance.RemoveClient(playerId);
    }
}
