using System.Text;
using UnityEngine;

// Use plugin namespace
using HybridWebSocket;
using System;

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

    public void SendPacket(SendablePacket sendablePacket)
    {
        ws.Send(sendablePacket.GetSendableBytes());
    }

    private void RegisterWebSocketListeners(WebSocket ws)
    {


        // Add OnOpen event listener
        ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");
            Debug.Log("WS state: " + ws.GetState().ToString());


            connected = true;

            SendPacket(new PacketOutPlayerConnect());
        };

        // Add OnMessage event listener

        WebSocketListener listener = new WebSocketListener();


        ws.OnMessage += (byte[] bytes) =>
        {

            ws.Close();
            /*
            ReceivablePacket receivablePacket = new ReceivablePacket(bytes);

            ReceivablePacketManager.Handle(receivablePacket);

    */
        };

        // Add OnError event listener
        ws.OnError += (string errMsg) =>
        {
            Debug.Log("WS error: " + errMsg);
        };

        // Add OnClose event listener
        ws.OnClose += (WebSocketCloseCode code) =>
        {
            Debug.Log("WS closed with code: " + code.ToString());

            connected = false;
        };
    }

    private void DisconnectFromServer()
    {
        if (connected) ws.Close();
    }

}
