using System.IO;

[PacketId(0)]
public class PacketInPlayerConnect : PacketIn
{
    public long playerId;

    public string playerName;

    public double posX;

    public double posY;

    public PacketInPlayerConnect(MemoryStream memoryStream) : base(memoryStream)
    {
    }

    public override void OnDataHandled()
    {
        ClientManager.Instance.CreateClient(playerId, playerName, (float) posX, (float) posY);
    }
}
