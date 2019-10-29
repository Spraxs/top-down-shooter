using System.Text;
using UnityEngine;

// Use plugin namespace
using HybridWebSocket;

public class WebManager : MonoBehaviour
{
    // Web manager instance.
    public static WebManager Instance;

    // Connection settings.
    string serverIP = "127.0.0.1";
    int serverPort = 26648;
    int connectionTimeOut = 5000;

    private bool connected = false;

    // WebSocket
    private WebSocket ws;

    void OnEnable()
    {
        Instance = this;
    }

    void Start()
    {
        ws = WebSocketFactory.CreateInstance("ws://127.0.0.1:8080//");

        RegisterWebSocketListeners(ws);

        ws.Connect();
    }

    private void OnApplicationQuit()
    {
        DisconnectFromServer();
    }

    private void RegisterWebSocketListeners(WebSocket ws)
    {
        // Add OnOpen event listener
        ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");
            Debug.Log("WS state: " + ws.GetState().ToString());


            connected = true;

            // ws.Send(Encoding.UTF8.GetBytes("Hello from Unity 3D!"));
        };

        // Add OnMessage event listener

        WebSocketListener listener = new WebSocketListener();


        ws.OnMessage += listener.HandlePacketData;

        // Add OnError event listener
        ws.OnError += (string errMsg) =>
        {
            Debug.Log("WS error: " + errMsg);
        };

        // Add OnClose event listener
        ws.OnClose += (WebSocketCloseCode code) =>
        {
            Debug.Log("WS closed with code: " + code.ToString());
        };
    }

    private void DisconnectFromServer()
    {
        if (connected) ws.Close();
    }

}
