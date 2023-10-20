using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class Player : NetworkBehaviour
{
    private PlayerManager playerManager;
    private LobbyManager lobbyManager;

    public TMP_Text playerName;
    public GameObject playerCamera;
    public PlayerInput playerInput;



    public override void OnNetworkSpawn()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        playerManager.AddPlayer(NetworkObject);
        playerName.text = "J" + playerManager.playerList.Count.ToString();

    }


    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
            lobbyManager.UpdatePlayers(0,0);
        }
    }

    private void OnDestroy()
    {
        playerManager.RemovePlayer(NetworkObject);
    }

    // Race functions

    public void ToggleCamera(bool camToggle)
    {
        if(IsOwner)
        {
            playerCamera.SetActive(camToggle);
        }

    }

    public void ToggleKinematic(bool kineToggle)
    {
        GetComponent<Rigidbody>().isKinematic = kineToggle;
    }

    public void ToggleOwnName(bool nameToggle)
    {
        if(IsOwner)
        {
            playerName.enabled = nameToggle;
        }
    }

    public void ToggleInputs(bool inputToggle)
    {
        if(IsOwner)
        {
            playerInput.enabled = inputToggle;
        }

    }

}

   
