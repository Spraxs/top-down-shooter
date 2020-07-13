using System.IO;

[PacketId(13)]
public class PacketInGameModeLose : PacketIn
{
    public PacketInGameModeLose() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            GameModeManager.Instance().HandleLose();
        });
    }
}
