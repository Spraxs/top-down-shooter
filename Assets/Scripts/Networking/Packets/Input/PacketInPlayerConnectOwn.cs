using System.IO;

using UnityEngine;

[PacketId(2)]
public class PacketInPlayerConnectOwn : PacketIn
{
    public long playerId;

    public string playerName;

    public double posX;

    public double posY;

    public PacketInPlayerConnectOwn(MemoryStream memoryStream) : base(memoryStream)
    {
    }

    public override void OnDataHandled()
    {

        ClientManager.Instance.CreateOwnClient(playerId, playerName, (float)posX, (float)posY);
    }
}
