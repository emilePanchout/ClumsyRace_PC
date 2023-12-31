using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class RaceManager : NetworkBehaviour
{
    private PlayerManager playerManager;
    public MapLoader mapLoader;
    public Ranking ranking;


    public GameObject spectateCam;
    public RaceSpawn raceSpawner;
    public GameObject countdown;
    public TMP_Text countdownText;

    public bool oneHasFinished = false;
    public GameObject finishCountdown;
    public TMP_Text finishCountdownText;
    public GameObject finishImage;
    public int timeToStart = 11;
    public int timeTofFinish = 30;


    public List<GameObject> playerFinished;


    private void Start()
    {
        
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        LockCursor();
        mapLoader.LoadMap(playerManager.mapName);

        if(GameObject.Find("StartLine(Clone)") != null)
        {
            raceSpawner = GameObject.Find("StartLine(Clone)").GetComponent<RaceSpawn>();
        }
        else
        {
            raceSpawner = GameObject.Find("StartLine").GetComponent<RaceSpawn>();
        }

        spectateCam.transform.position = raceSpawner.transform.position + Vector3.up * 3;
        spectateCam.transform.rotation = raceSpawner.transform.rotation;

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
            player.GetComponent<Player>().ToggleCharacterController(true);

            player.GetComponent<Player>().SetCheckpoint(raceSpawner.spawnerList[i].transform);
            player.GetComponent<Player>().TeleportToCheckpoint();

            i++;
        }
    }

    ////////////////////////////////////////////////////////////////////

    public void StartFinishCountdown()
    {
        if(IsServer && !oneHasFinished)
        {
            StartFinishCountdownClientRpc();
            oneHasFinished = true;
        }
    }

    [ClientRpc]
    public void StartFinishCountdownClientRpc()
    {
        finishCountdown.SetActive(true);
        StartCoroutine(FinishCountdown(timeTofFinish));
    }


    IEnumerator FinishCountdown(int seconds)
    {
        int counter = seconds;
        while (counter > -1)
        {
            finishCountdownText.text = "0:" + counter.ToString() + " to finish";

            yield return new WaitForSeconds(1);

            counter--;
        }

        FinishGame();
    }


    public void FinishGame()
    {
        finishImage.SetActive(true);
        int i = 0;
        foreach(GameObject player in playerFinished)
        {
            ranking.rankingText[i].text = (i+1) + ". " + player.GetComponent<Player>().playerName.text;
            i++;
        }
        finishCountdown.SetActive(false);
        StartCoroutine(WaitEnd());
       
    }

    IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(4);

        if (IsServer)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("Lobby", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

    }



    ////////////////////////////////////////////////////////////////////

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
        countdown.SetActive(true);
        StartCoroutine(Countdown(timeToStart));
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
            player.GetComponent<PlayerMovement>().CanJump = true;
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


    ////////////////////////////////////////////////////////////////////

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }







}
