using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class RaceManager : NetworkBehaviour
{
    private PlayerManager playerManager;

    public RaceSpawn raceSpawner;
    public GameObject countdown;
    public TMP_Text countdownText;


    private void Start()
    {
        
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        PreparePlayer();
        StartCountdown();
    }

    public void PreparePlayer()
    {
        int i = 0;
        foreach(NetworkObject player in playerManager.playerList)
        {

            player.GetComponent<Player>().ToggleOwnName(false);
            player.GetComponent<Player>().ToggleCamera(true);
            //player.GetComponent<Player>().ToggleInputs(true);
            player.GetComponent<Player>().ToggleKinematic(false);

            player.GetComponent<Player>().lastCheckpoint = raceSpawner.spawnerList[i];
            player.GetComponent<Player>().TeleportToCheckpoint();

            i++;
        }
    }


    public void StartCountdown()
    {
        if(IsServer)
        {
            StartCountdownClientRpc();
        }
    }

    [ClientRpc]
    public void StartCountdownClientRpc()
    {
        StartCoroutine(Countdown(3));
        Debug.Log("clientRPC");
    }

    public void EndCountdown()
    {
        if(IsServer)
        {
            EndCountdownClientRpc();
        }
    }

    [ClientRpc]
    public void EndCountdownClientRpc()
    {
        countdown.SetActive(false);
        foreach (NetworkObject player in playerManager.playerList)
        {
            player.GetComponent<Player>().ToggleInputs(true);
        }

    }

    IEnumerator Countdown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            countdownText.text = counter.ToString();
 
            yield return new WaitForSeconds(1);

            counter--;
        }

        EndCountdown();
        

    }







}
