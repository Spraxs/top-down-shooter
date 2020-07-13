using System;
using UnityEngine;

using TMPro;

public class GameStats : MonoBehaviour
{

    [Header("Stats components")]
    [SerializeField] private TMP_Text redScoreText;
    [SerializeField] private TMP_Text blueScoreText;

    [SerializeField] private TMP_Text timerText;


    private int lastRedScore = -1;
    private int lastBlueScore = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (redScoreText == null || blueScoreText == null || timerText == null)
        {
            Debug.LogWarning("Not all components has been set!");
        }
    }

    public void UpdateScores(int redScore, int blueScore)
    {
        if (redScore != lastRedScore)
        {
            redScoreText.text = redScore.ToString();
        }

        if (blueScore != lastBlueScore)
        {
            blueScoreText.text = blueScore.ToString();
        }

        lastRedScore = redScore;
        lastBlueScore = blueScore;
    }

    public void UpdateTimer(long gameEndTimeMillis)
    {
        long time = gameEndTimeMillis - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        TimeSpan t = TimeSpan.FromMilliseconds(time);

        string answer = string.Format("{0:D2}:{1:D2}",
           t.Minutes,
            t.Seconds);

        timerText.text = "" + answer;
    }
}
