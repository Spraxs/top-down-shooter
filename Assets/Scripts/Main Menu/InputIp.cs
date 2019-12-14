using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InputIp : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    void Start()
    {
        string ip = PlayerPrefs.GetString("ip", "localhost");
        int port = PlayerPrefs.GetInt("port", 25565);

        inputField.text = ip + ":" + port;
    }

    public void TransferIpAndLaunchTestScene()
    {
        string input = inputField.text;

        if (input == null) return;

        if (!input.Contains(":"))
        {
            input += ":25565";
        }

        string[] parts = input.Split(':');

        string ip = parts[0];
        int port = int.Parse(parts[1]);

        PlayerPrefs.SetString("ip", ip);
        PlayerPrefs.SetInt("port", port);

        SceneManager.LoadScene("Test-Scene");
    }
}
