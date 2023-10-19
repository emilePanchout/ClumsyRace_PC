using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class RaceManager : NetworkBehaviour
{
    public PlayerManager playerManager;

    private void Start()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        PreparePlayer();
    }

    public void PreparePlayer()
    {
        foreach(NetworkObject player in playerManager.playerList)
        {
            player.GetComponent<Player>().ToggleCamera(true);
            player.GetComponent<Player>().ToggleKinematic(false);
            player.GetComponent<Player>().ToggleOwnName(false);
        }
    }


}
