using System.IO;

using UnityEngine;

[PacketId(2)]
public class PacketInPlayerConnectOwn : PacketIn
{

    public PacketInPlayerConnectOwn() : base(true)
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
            ClientManager.Instance.CreateOwnClient(playerId, playerName, (float) posX, (float) posY);
        });
    }
}
