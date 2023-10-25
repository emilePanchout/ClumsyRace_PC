using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StarGameButton : NetworkBehaviour
{
    public LobbyManager lobbyManager;
    private GameObject button;
    void Start()
    {
        button = gameObject;

        if (IsHost)
        {
            button.SetActive(true);
        }
        else if (IsClient)
        {
            button.SetActive(false);
        }
    }

    public void LaunchGame()

    {
        lobbyManager.spawnPoints = new List<PlayerSpawner>();
        NetworkManager.Singleton.SceneManager.LoadScene("Race", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

}
