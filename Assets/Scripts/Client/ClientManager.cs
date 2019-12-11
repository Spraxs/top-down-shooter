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

    private WebManager webManager;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        webManager = WebManager.Instance;
    }

    public void SendClientPosition(float x, float y)
    {
        webManager.SendPacket(new PacketOutPlayerPositionChange(x, y));
    }

    public void MoveClient(long id, float x, float y)
    {
        Debug.Log("ID: " + id);


        ClientMovement clientMovement = _onlineClients[id].GetComponent<ClientMovement>();

        clientMovement.HandleClientMovement(new Vector2(x, y));
    }

    public void CreateClient(long id, string name, float x, float y)
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

    public void CreateOwnClient(long id, string name, float x, float y)
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

    public void DamageOwnPlayer(float damage, float playerHealth, Vector2 damagePosition)
    {
        currentClient.health = playerHealth < 0 ? 0 : playerHealth;

        /* Knock back

        Vector2 clientPosition = currentClient.transform.position;

        clientPosition.Normalize();
        damagePosition.Normalize();

        Vector2 force = clientPosition - damagePosition;

        force *= 25;

        currentClient.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        */
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

    public void RemoveClient(long playerId)
    {
        if (playerId == currentClient.id)
        {
            currentClient = null;
        }

        Client client = _onlineClients[playerId];

        // Remove client
        _onlineClients.Remove(playerId);

        // Destroy client
        Destroy(client.gameObject);
    }
}
