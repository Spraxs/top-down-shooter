﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    public void SendClientHandTransform(float posX, float posY, float rotZ, float scaleX)
    {
        webManager.SendPacket(new PacketOutPlayerHandTransformChange(posX, posY, rotZ, scaleX));
    }

    public void MoveClient(long id, float x, float y)
    {
        ClientMovement clientMovement = _onlineClients[id].GetComponent<ClientMovement>();

        clientMovement.HandleClientMovement(new Vector2(x, y));
    }
    
    public void MoveClientHand(long id, float posX, float posY, float rotZ, float scaleX)
    {
        ClientMovement clientMovement = _onlineClients[id].GetComponent<ClientMovement>();

        clientMovement.HandleHandMovement(new Vector2(posX, posY), rotZ, scaleX);
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

    public void DamageOwnPlayer(float damage, float playerHealth, Vector2 damageDirection)
    {
        currentClient.health = playerHealth < 0 ? 0 : playerHealth;

        damageDirection *= 20;

        currentClient.GetComponent<Rigidbody2D>().AddForce(damageDirection, ForceMode2D.Impulse);
    }

    public void PlayShootRayEffect(long playerId, Vector2 beginPosition, Vector2 endPosition, bool isHit)
    {
        Client client = GetPlayer(playerId);

        if (client ==  null) return;

        beginPosition = client.transform.position;

        client.PlayShootEffect(beginPosition, endPosition, isHit);

    }

    public void HandleClientDeath(long playerId, Vector2 deathPosition)
    {
        Client client = GetPlayer(playerId);

        client.gameObject.transform.position = deathPosition;

        EffectManager.Instance.Play(EffectManager.EffectType.DEATH, deathPosition);

        client.gameObject.SetActive(false);
    }

    public void HandleClientReSpawn(long playerId, Vector2 reSpawnPosition)
    {
        Client client = GetPlayer(playerId);

        client.health = client.maxHealth;
        client.gameObject.transform.position = reSpawnPosition;

        EffectManager.Instance.Play(EffectManager.EffectType.DEATH, reSpawnPosition);

        client.gameObject.SetActive(true);
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
            SceneManager.LoadScene(1);
        }

        Client client = _onlineClients[playerId];

        // Remove client
        _onlineClients.Remove(playerId);

        // Destroy client
        Destroy(client.gameObject);
    }

    public void UpdateClientTeam(long objectId, int teamId)
    {
        Client client;

        if (!_onlineClients.TryGetValue(objectId, out client)) return;

        Team team = (Team) teamId;

        client.Team = team;
    }
}
