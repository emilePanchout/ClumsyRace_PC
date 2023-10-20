using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class RaceManager : NetworkBehaviour
{
    private PlayerManager playerManager;

    private void Start()
    {
        NetworkManager.Singleton.GetComponent<ConnectionApprovalHandler>().enabled = false;
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        PreparePlayer();
    }

    public void PreparePlayer()
    {
        foreach(NetworkObject player in playerManager.playerList)
        {

            player.GetComponent<Player>().ToggleOwnName(false);
            player.GetComponent<Player>().ToggleCamera(true);
            player.GetComponent<Player>().ToggleInputs(true);
            player.GetComponent<Player>().ToggleKinematic(false);

        }
    }


}
