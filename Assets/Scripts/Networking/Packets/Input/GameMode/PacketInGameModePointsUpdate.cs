using System.IO;

[PacketId(10)]
public class PacketInGameModePointsUpdate : PacketIn
{

    public PacketInGameModePointsUpdate() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        int redScore = ReadInt(ms);
        int blueScore = ReadInt(ms);

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            GameModeManager.Instance().UpdateScore(redScore, blueScore);
        });
    }
}
