
using System.IO;

[PacketId(15)]
public class PacketInGameModeTeamUpdate : PacketIn
{
    public PacketInGameModeTeamUpdate() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        long objectId = ReadLong(ms);
        int teamId = ReadInt(ms);

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {

            ClientManager.Instance.UpdateClientTeam(objectId, teamId);

        });
    }
}
