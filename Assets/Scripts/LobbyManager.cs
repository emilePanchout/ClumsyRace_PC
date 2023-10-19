using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using TMPro;

public class LobbyManager : NetworkBehaviour
{
    private PlayerManager playerManager;

    public TMP_Text playerCountText;
    public List<PlayerSpawner> spawnPoints;

    private void Start()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        playerManager.playerCount.OnValueChanged += UpdatePlayers;

        UpdatePlayers(0, playerManager.playerCount.Value);
    }

    public void UpdatePlayers(int prev ,int curr)
    {
        UpdateText();
        PlacePlayer();
    }

    public void UpdateText()
    {
        playerCountText.text = playerManager.playerCount.Value + " / " + playerManager.maxPlayerCount.Value + " players";
    }

    public void PlacePlayer()
    {
        ResetSpawner();

        int i = 0;
        foreach (NetworkObject player in playerManager.playerList)
        {
            
            Debug.Log("/////////////////////////////////////////////////////");
            Debug.Log("placing player " + (i+1) + " out of " + playerManager.playerList.Count);

            int j = 0;
            foreach (PlayerSpawner spawner in spawnPoints)
            {
                
                Debug.Log("Checking spawner " + (j+1) + " out of " + spawnPoints.Count);
 
                if (spawnPoints[j].player == null)
                {
                    spawnPoints[j].SetPlayer(player);
                    Debug.Log("Player " + (i+1) + " placed on spawner " + (j+1));
                    break;
                }
                j++;
            }
            i++;
        }
    }

    public void ResetSpawner()
    {
        foreach (PlayerSpawner spawner in spawnPoints)
        {
            spawner.player = null;
        }
    }


}
