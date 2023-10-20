using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    private LobbyManager lobbyManager;
    public NetworkObject player;


    void Awake()
    {
        lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
        lobbyManager.spawnPoints.Add(this);

    }


    public void SetPlayer(NetworkObject newPlayer)
    {
        player = newPlayer;
        player.transform.position = transform.position + Vector3.up * 0.5f;
    }

    public void RemovePlayer()
    {
        player = null;
    }
}
