using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        int i = 0;
        foreach(NetworkObject player in playerManager.playerList)
        {
            player.transform.position = spawnPoints[i].transform.position;
            i++;
        }

        UpdateText();
    }

    public void UpdateText()
    {
        playerCountText.text = playerManager.playerCount.Value + " / " + playerManager.maxPlayerCount.Value + " players";

    }



}
