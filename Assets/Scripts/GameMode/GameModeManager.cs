using System.Collections;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    private static GameModeManager instance;

    [SerializeField] private GameStats gameStats;
    [SerializeField] private GameStateUI gameStateUi;

    private long gameEndTimeMillis = -1;
    private long currentTimeMillis = -2;

    public static GameModeManager Instance()
    {
        return instance;
    }

    void OnEnable()
    {
        instance = this;
    }

    public void SetGameTimer(long gameEndTime)
    {
        gameEndTimeMillis = gameEndTime;

        StartCoroutine(UpdateTimerUI());

    }

    private IEnumerator UpdateTimerUI()
    {
        while (currentTimeMillis < gameEndTimeMillis)
        {
            gameStats.UpdateTimer(gameEndTimeMillis);

            yield return new WaitForSeconds(1f);

        }
        
    }

    public void UpdateScore(int redScore, int blueScore)
    {
        gameStats.UpdateScores(redScore, blueScore);
    }

    public void UpdateState(int stateId)
    {
        GameState gameState = (GameState) stateId;
        gameStateUi.UpdateGameState(gameState);
    }

    public void HandleWin()
    {
        gameStateUi.UpdateWin();
    }

    public void HandleLose()
    {
        gameStateUi.UpdateLose();
    }

    public enum GameState
    {
        LOBBY = 0, IN_GAME = 1, AFTER_MATCH = 2 
    }
}
