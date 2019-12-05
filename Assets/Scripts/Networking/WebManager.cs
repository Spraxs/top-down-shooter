using UnityEngine;

// Use plugin namespace
using WebSocketSharp;

public class WebManager : MonoBehaviour
{
    // Web manager instance.
    public static WebManager Instance;

    // Connection settings.
    [SerializeField]
    string serverIP = "51.68.175.9";
    [SerializeField]
    int serverPort = 8080;
    int connectionTimeOut = 5000;

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

        PacketManager.Init();

        packetManager = PacketManager.Instance;

        ws = new WebSocket("ws://" + serverIP + ":" + serverPort + "//");
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
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WS connected!");

            connected = true;

            SendPacket(new PacketOutPlayerConnect());
        };

        // Add OnMessage event listener

        ws.OnMessage += (sender, e) =>
        {
            if (e.IsPing)
            {
                //TODO Ping back
                return;
            }

            if (e.IsText) return;
            if (!e.IsBinary) return;

            byte[] bytes = e.RawData;
            packetManager.HandlePacketIn(bytes);
        };

        //ws.EmitOnPing = true;

        // Add OnError event listener
        ws.OnError += (sender, e) =>
        {
            var ex = e.Exception;
            Debug.Log("ws error: " +  ex.StackTrace);
        };

        // Add OnClose event listener
        ws.OnClose += (sender, e) =>
        {
            Debug.Log("WS closed with code: " + e.Code);
            Debug.Log("WS closed with reason: " + e.Reason);

            connected = false;
        };
    }

    private void DisconnectFromServer()
    {
        if (connected) {
            ws.Close(CloseStatusCode.Normal);
        }
    }

}
