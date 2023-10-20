using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class RaceManager : NetworkBehaviour
{
    private PlayerManager playerManager;

    public RaceSpawn raceSpawner;


    private void Start()
    {
        
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        PreparePlayer();
    }

    public void PreparePlayer()
    {
        int i = 0;
        foreach(NetworkObject player in playerManager.playerList)
        {

            player.GetComponent<Player>().ToggleOwnName(false);
            player.GetComponent<Player>().ToggleCamera(true);
            player.GetComponent<Player>().ToggleInputs(true);
            player.GetComponent<Player>().ToggleKinematic(false);

            player.GetComponent<Player>().lastCheckpoint = raceSpawner.spawnerList[i];
            player.GetComponent<Player>().TeleportToCheckpoint();

            i++;
        }
    }






}
