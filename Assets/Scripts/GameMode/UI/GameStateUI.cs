using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : MonoBehaviour
{
    [Header("Stats components")]
    [SerializeField] private TMP_Text endMessage;

    private Image image;


    private int lastRedScore = -1;
    private int lastBlueScore = -1;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        if (endMessage == null)
        {
            Debug.LogWarning("Not all components has been set!");
        }
    }
    
    public void UpdateGameState(GameModeManager.GameState gameState)
    {
        var color = image.color;
        if (gameState == GameModeManager.GameState.AFTER_MATCH || gameState == GameModeManager.GameState.LOBBY)
        {
            color.a = 150;
        }
        else
        {
            color.a = 0;
            endMessage.text = "";
        }
        image.color = color;
    }

    public void UpdateLose()
    {
        endMessage.text = "LOSER";
    }

    public void UpdateWin()
    {
        endMessage.text = "WINNER";
    }
}
