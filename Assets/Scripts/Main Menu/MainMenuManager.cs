using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject multiPlayerPanel;

    void Start()
    {
        mainPanel.SetActive(true);
        multiPlayerPanel.SetActive(false);
    }

    public void ShowMainPanel()
    {
        ShowPanel(MenuPanel.MAIN);
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

                mainPanel.SetActive(true);

                break;

            case MenuPanel.MULTI_PLAYER:
                mainPanel.SetActive(false);

                multiPlayerPanel.SetActive(true);

                break;
        }
    }

    public enum MenuPanel
    {
        MAIN, MULTI_PLAYER
    }
}
