using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class FinishDetection : MonoBehaviour
{
    public PlayerManager playerManager;
    private RaceManager raceManager;
    private Ranking ranking;

    private void Start()
    {
        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
        //ranking = GameObject.Find("Ranking").GetComponent<Ranking>();
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject.transform.parent.gameObject.transform.parent.gameObject;

            player.SetActive(false);
            raceManager.playerFinished.Add(player);
            int index = raceManager.playerFinished.IndexOf(player);
            //ranking.rankingText[index].text = (index + 1).ToString() + ". " + player.GetComponent<Player>().playerName.text;

         

            if((index+1) == playerManager.playerList.Count)
            {
                raceManager.FinishGame();
            }
            else
            {
                raceManager.StartFinishCountdown();
            }
        }
    }
}

