using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InputIp : MonoBehaviour
{
    private const string GameScene = "Test-Scene";

    [SerializeField] private TMP_InputField inputField;

    void Start()
    {
        inputField.text = PlayerPrefs.GetString(ServerAddress.PreferenceKey, ServerAddress.DefaultInput);
    }

    public void TransferIpAndLaunchTestScene()
    {
        string input = inputField.text;

        if (string.IsNullOrWhiteSpace(input)) return;

        // Kept as typed; WebManager turns it into a URL (ServerAddress)
        PlayerPrefs.SetString(ServerAddress.PreferenceKey, input.Trim());

        SceneManager.LoadScene(GameScene);
    }
}
