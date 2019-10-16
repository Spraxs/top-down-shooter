using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Player defaultPlayer;

    [SerializeField] private readonly Dictionary<long, Player> _onlinePlayers = new Dictionary<long, Player>();

    [HideInInspector] public GameObject playerObject;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        playerObject = Instantiate(playerPrefab);

        Player player = ScriptableObject.CreateInstance<Player>();

        playerObject.name = player.id + "_player";

        player.id = _onlinePlayers.Count;
        player.health = defaultPlayer.health;
        player.maxHealth = defaultPlayer.maxHealth;
        player.movementSpeed = defaultPlayer.movementSpeed;
        player.name = defaultPlayer.name;

        _onlinePlayers.Add(player.id, defaultPlayer);

        playerObject.GetComponent<PlayerHandler>().player = player;
    }

    public GameObject GetGameObjectById(long id)
    {
        return GameObject.Find(id + "_player");
    }

    public List<Player> GetAllPlayers()
    {
        return _onlinePlayers.Values.ToList();
    }

    public void RemovePlayer(Player player)
    {
        _onlinePlayers.Remove(player.id);
    }

}
