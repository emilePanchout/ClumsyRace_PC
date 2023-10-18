using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour
{
    private LobbyManager lobbyManager;

    public void Start()
    {

    }

    public void PlaceInLobby()
    {

        lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();

        foreach (PlayerSpawner spawner in lobbyManager.spawnPoints)
        {
            if (!spawner.isUsed.Value)
            {
                spawner.player = gameObject;
                Debug.Log("SetPlayer");

            }
            break;
        }

    }
}
