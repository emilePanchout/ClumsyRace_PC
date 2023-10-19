using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : NetworkBehaviour
{
    private PlayerManager playerManager;
    private LobbyManager lobbyManager;

    public TMP_Text playerName;


    public override void OnNetworkSpawn()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        playerManager.AddPlayer(NetworkObject);
        playerName.text = "J" + playerManager.playerList.Count.ToString();

    }


    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
            lobbyManager.UpdatePlayers(0,0);
        }
    }


    private void OnDestroy()
    {
        playerManager.RemovePlayer(NetworkObject);
    }

    void Update()
    {

    }


    public void PlaceInLobby()
    {
        Debug.Log("Placing players in lobby");
        foreach (PlayerSpawner spawner in lobbyManager.spawnPoints)
        {
            Debug.Log("Checking spawner ...");
            int i = 0;
            if(lobbyManager.spawnPoints[i].player == null)
            {
                lobbyManager.spawnPoints[i].SetPlayer(NetworkObject);
                Debug.Log("Player " + i + " placed");
                break;
            }
            i++;
        }
    }

}

   
