using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    private LobbyManager lobbyManager;

    public NetworkVariable<bool> isUsed;
    public GameObject player;


    void Start()
    {
        lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
        lobbyManager.spawnPoints.Add(this);

    }
}
