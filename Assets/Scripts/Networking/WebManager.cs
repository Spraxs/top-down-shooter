using UnityEngine;
using UnityEngine.SceneManagement;

// One WebSocket API for both builds: the browser's own WebSocket under WebGL, WebSocketSharp elsewhere
using HybridWebSocket;

public class WebManager : MonoBehaviour
{
    // Web manager instance.
    public static WebManager Instance;

    private const string MainMenuScene = "Main Menu";

    private bool connected = false;

    // WebSocket
    private WebSocket ws;

    // PacketManager
    private PacketManager packetManager;

    void OnEnable()
    {
        Instance = this;
    }

    void Start()
    {
        string url = ServerAddress.ToUrl(PlayerPrefs.GetString(ServerAddress.PreferenceKey, ServerAddress.DefaultInput));

        PacketManager.Init();

        packetManager = PacketManager.Instance;

        ws = WebSocketFactory.CreateInstance(url);
        RegisterWebSocketListeners(ws);

        ws.Connect();

    }

    private void OnApplicationQuit()
    {
        DisconnectFromServer();
    }

    public void SendPacket(PacketOut packetOut)
    {
        // Prepare field values
        packetOut.onDataPrepare();

        // Write field values to input array
        packetOut.HandlePacketData();

        // Get bytes from input array
        byte[] bytes = packetOut.GetSendableBytes();;

        ws.Send(bytes);
    }

    private void RegisterWebSocketListeners(WebSocket ws)
    {
        // Add OnOpen event listener
        ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");

            connected = true;

            SendPacket(new PacketOutPlayerConnect());
        };

        // Add OnMessage event listener: the server only sends binary packets
        ws.OnMessage += (bytes) =>
        {
            packetManager.HandlePacketIn(bytes);
        };

        // Add OnError event listener
        ws.OnError += (errorMessage) =>
        {
            Debug.Log("ws error: " + errorMessage);
        };

        // Add OnClose event listener
        ws.OnClose += (code) =>
        {
            if (code == WebSocketCloseCode.Abnormal)
            {
                SceneManager.LoadScene(MainMenuScene);
            }
            Debug.Log("WS closed with code: " + code);

            connected = false;
        };
    }

    private void DisconnectFromServer()
    {
        if (connected) {
            ws.Close(WebSocketCloseCode.Normal, "disconnect");
        }
    }

}
