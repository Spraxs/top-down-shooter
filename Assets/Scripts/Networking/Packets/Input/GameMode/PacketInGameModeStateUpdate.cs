using System.IO;

[PacketId(11)]
public class PacketInGameModeStateUpdate : PacketIn
{

    public PacketInGameModeStateUpdate() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        int stateId = ReadInt(ms);

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            GameModeManager.Instance().UpdateState(stateId);
        });
    }
}
