using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject multiPlayerPanel;

    [SerializeField]
    private GameObject singlePlayerPanel;

    void Start()
    {
        mainPanel.SetActive(true);
        multiPlayerPanel.SetActive(false);
        singlePlayerPanel.SetActive(false);
    }

    public void ShowMainPanel()
    {
        ShowPanel(MenuPanel.MAIN);
    }

    public void ShowSinglePlayerPanel()
    {
        ShowPanel(MenuPanel.SINGLE_PLAYER);
    }

    public void ShowMultiPlayerPanel()
    {
        ShowPanel(MenuPanel.MULTI_PLAYER);
    }

    public void ShowPanel(MenuPanel menuPanel)
    {

        switch (menuPanel)
        {
            case MenuPanel.MAIN:

                multiPlayerPanel.SetActive(false);
                singlePlayerPanel.SetActive(false);

                mainPanel.SetActive(true);

                break;

            case MenuPanel.MULTI_PLAYER:

                singlePlayerPanel.SetActive(false);
                mainPanel.SetActive(false);

                multiPlayerPanel.SetActive(true);

                break;

            case MenuPanel.SINGLE_PLAYER:

                multiPlayerPanel.SetActive(false);
                mainPanel.SetActive(false);

                singlePlayerPanel.SetActive(true);

                break;
        }
    }

    public enum MenuPanel
    {
        MAIN, MULTI_PLAYER, SINGLE_PLAYER
    }
}
