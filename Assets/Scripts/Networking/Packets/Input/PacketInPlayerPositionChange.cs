using System.IO;

[PacketId(4)]
public class PacketInPlayerPositionChange : PacketIn
{
    public long playerId;

    public double posX;
    public double posY;

    public PacketInPlayerPositionChange(MemoryStream _memoryStream) : base(_memoryStream)
    {
    }

    public override void OnDataHandled()
    {
        ClientManager.Instance.MoveClient(playerId, (float) posX, (float) posY);
    }
}
