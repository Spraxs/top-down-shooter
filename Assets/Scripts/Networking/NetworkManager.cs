using System;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    // Network manager instance.
    public static NetworkManager Instance;

    // Connection settings.
    string serverIP = "116.203.114.52";
    int serverPort = 25565;
    int connectionTimeOut = 5000;

    // For socket read.
    Thread readThread;
    bool readThreadStarted = false;

    // For socket write.
    Socket socket;
    bool socketConnected = false;

    // Used for kicked to login screen message.
    public bool kicked = false;

    void OnEnable()
    {
        Instance = this;
    }

    private void Start()
    {
        //bool connect = ConnectToServer();

       // Debug.Log(connect);

       //StartCoroutine(GetText());
       StartCoroutine(SendText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://spraxs.nl:25565/");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // 接收文本数据，并打印到日志中
            Debug.Log(www.downloadHandler.text);

            // 接收二进制数据
            byte[] results = www.downloadHandler.data;
        }
    }

    IEnumerator SendText()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", "test");

        UnityWebRequest www = UnityWebRequest.Post("http://spraxs.nl:25565/", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void OnApplicationQuit()
    {
        DisconnectFromServer();
    }

    public void DisconnectFromServer()
    {
        if (socket != null && socket.Connected)
        {
            socket.Close();
        }
        socketConnected = false;
        readThreadStarted = false;

        // Clear stored variables.
        // PlayerManager.instance.accountName = null;
        // PlayerManager.instance.characterList = null;
        // PlayerManager.instance.selectedCharacterData = null;
    }

    // Best to call this only once per login attempt.
    public bool ConnectToServer()
    {
        if (socketConnected = false || socket == null || !socket.Connected)
        {
            ConnectSocket();
        }
        return SocketConnected();
    }

    private void ConnectSocket()
    {
        try
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IAsyncResult result = socket.BeginConnect(serverIP, serverPort, null, null);
            bool success = result.AsyncWaitHandle.WaitOne(connectionTimeOut, true);

            if (!success)
            {
                socketConnected = false;
                socket.Close();
            }
            else
            {
                if (socket.Connected)
                {
                    socketConnected = true;
                    // Start Receive thread.
                    readThreadStarted = true;
                    readThread = new Thread(new ThreadStart(ChannelRead));
                    readThread.Start();
                }
                else
                {
                    socketConnected = false;
                    socket.Close();
                }
            }
        }
        catch (SocketException se)
        {
            socketConnected = false;
            readThreadStarted = false;
            Debug.Log(se);
        }
    }

    private void ChannelRead()
    {
        byte[] bufferLength = new byte[2]; // We use 2 bytes for short value.
        byte[] bufferData;
        short length; // Since we use short value, max length should be 32767.

        while (readThreadStarted)
        {
            if (socket.Receive(bufferLength) > 0)
            {
                // Get packet data length.
                length = BitConverter.ToInt16(bufferLength, 0);
                bufferData = new byte[length];

                // Get packet data.
                socket.Receive(bufferData);

                // Handle packet.
                ReceivablePacketManager.handle(new ReceivablePacket(Encryption.Decrypt(bufferData)));
            }
        }
    }

    public void ChannelSend(SendablePacket packet)
    {
        if (SocketConnected())
        {
            socket.Send(Encryption.Encrypt(packet.GetSendableBytes()));
        }
        else // Connection closed.
        {
            DisconnectFromServer();
            // Go to login screen.
            //  SceneFader.Fade("LoginScreen", Color.white, 0.5f);
        }
    }

    private bool SocketConnected()
    {
        // return !(socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0);
        return socketConnected && socket != null && socket.Connected;
    }
}
