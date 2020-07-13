using System.IO;

[PacketId(14)]
public class PacketInGameModeJoin : PacketIn
{
    public PacketInGameModeJoin() : base(true)
    {
    }

    public override void HandleData(MemoryStream ms)
    {
        long gameEndTime = ReadLong(ms);
        int redScore = ReadInt(ms);
        int blueScore = ReadInt(ms);
        int stateId = ReadInt(ms);

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            GameModeManager gameModeManager = GameModeManager.Instance();

            gameModeManager.SetGameTimer(gameEndTime);
            gameModeManager.UpdateScore(redScore, blueScore);
            gameModeManager.UpdateState(stateId);
        });
    }
}
