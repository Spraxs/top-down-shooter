using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public static ClientManager Instance;

    [SerializeField] private GameObject clientPlayerPrefab;

    [SerializeField] private GameObject clientOwnPlayerPrefab;

    private readonly Dictionary<long, Client> _onlineClients = new Dictionary<long, Client>();

    [HideInInspector] public Client currentClient;

    private List<ClientConnectData> connectingClients = new List<ClientConnectData>();

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {

        if (connectingClients.Count > 0)
        {
            ClientConnectData client = connectingClients[0];
            if (client.isOwnClient)
            {
                _CreateOwnClient(client.playerId, client.playerName, client.posX, client.posY);
            }
            else
            {
                _CreateClient(client.playerId, client.playerName, client.posX, client.posY);
            }

            connectingClients.Remove(client);
        }

    }

    public void CreateClient(long id, string name, float x, float y)
    {
        var client = new ClientConnectData
        {
            playerId = id,
            playerName = name,
            posX = x,
            posY = y,
            isOwnClient = false
        };

        connectingClients.Add(client);
    }

    public void CreateOwnClient(long id, string name, float x, float y)
    {
        var client = new ClientConnectData
        {
            playerId = id,
            playerName = name,
            posX = x,
            posY = y,
            isOwnClient = true
        };

        connectingClients.Add(client);
    }

    private void _CreateClient(long id, string name, float x, float y)
    {
        GameObject playerObject = Instantiate(clientPlayerPrefab);

        Client client = playerObject.GetComponent<Client>();

        client.id = id;
        client.name = name;

        client.maxHealth = 100;
        client.health = client.maxHealth;

        playerObject.transform.position = new Vector2(x, y);

        playerObject.name = client.id + "_player";

        _onlineClients.Add(client.id, client);
    }

    private void _CreateOwnClient(long id, string name, float x, float y)
    {
        GameObject playerObject = Instantiate(clientOwnPlayerPrefab);

        Client client = playerObject.GetComponent<Client>();

        client.id = id;
        client.name = name;

        client.maxHealth = 100;
        client.health = client.maxHealth;

        playerObject.transform.position = new Vector2(x, y);

        playerObject.name = client.id + "_player";

        _onlineClients.Add(client.id, client);

        currentClient = client;
    }

    public GameObject GetGameObjectById(long id)
    {
        return GameObject.Find(id + "_player");
    }

    public Client GetPlayer(long id)
    {
        return _onlineClients[id];
    }

    public List<Client> GetAllPlayers()
    {
        return _onlineClients.Values.ToList();
    }

    public void RemovePlayer(Client client)
    {
        _onlineClients.Remove(client.id);
    }
}
