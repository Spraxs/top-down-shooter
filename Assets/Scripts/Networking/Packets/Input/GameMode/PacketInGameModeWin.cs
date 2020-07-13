using System.IO;

[PacketId(12)]
public class PacketInGameModeWin : PacketIn
{
    public PacketInGameModeWin() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            GameModeManager.Instance().HandleWin();
        });
    }
}
