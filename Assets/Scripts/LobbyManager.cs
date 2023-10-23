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

        ReseTScene();

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
        if( SceneManager.GetActiveScene().name == "Lobby")
        {
            ResetSpawner();

            int i = 0;
            foreach (NetworkObject player in playerManager.playerList)
            {

                //Debug.Log("placing player " + (i + 1) + " out of " + playerManager.playerList.Count);

                int j = 0;
                foreach (PlayerSpawner spawner in spawnPoints)
                {

                    //Debug.Log("Checking spawner " + (j + 1) + " out of " + spawnPoints.Count);

                    if (spawnPoints[j].player == null)
                    {
                        if(IsOwner)
                        {
                            Debug.Log(spawnPoints[j] + " d");
                        }

                        spawnPoints[j].SetPlayer(player);
                        Debug.Log("Player " + (i + 1) + " placed on spawner " + (j + 1));
                        break;
                    }
                    j++;
                }
                i++;
            }
        }
        
    }


    public void ResetSpawner()
    {
        foreach (PlayerSpawner spawner in spawnPoints)
        {
            spawner.player = null;
        }
    }

    public void ResetPlayer()
    {
        foreach (NetworkObject player in playerManager.playerList)
        {
            player.gameObject.SetActive(true);
            player.GetComponent<Player>().ToggleOwnName(true);
            player.GetComponent<Player>().ToggleCamera(false);
            player.GetComponent<Player>().ToggleInputs(false);
            player.GetComponent<Player>().ToggleKinematic(true);
            player.GetComponent<Player>().ToggleCharacterController(false);
            player.GetComponent<Player>().lastCheckpoint = null;

        }
    }

    public void DelockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReseTScene()
    {
        //ResetSpawner();
        ResetPlayer();
        UpdatePlayers(0, playerManager.playerCount.Value);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
