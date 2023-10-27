using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class StarGameButton : NetworkBehaviour
{
    public LobbyManager lobbyManager;
    private GameObject button;
   // public GameObject mapName;
    void Start()
    {
        button = gameObject;

        if (IsHost)
        {
            button.SetActive(true);
            //mapName.SetActive(true);

        }
        else if (IsClient)
        {
            button.SetActive(false);
            //mapName.SetActive(false);
        }
    }

    public void LaunchGame()

    {
        //if(File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/" + mapName.GetComponent<TMP_InputField>().text + ".json") || mapName.GetComponent<TMP_InputField>().text == "")
        lobbyManager.spawnPoints = new List<PlayerSpawner>();
        NetworkManager.Singleton.SceneManager.LoadScene("Race", UnityEngine.SceneManagement.LoadSceneMode.Single);
        //lobbyManager.playerManager.mapName = mapName.GetComponent<TMP_InputField>().text;

    }

}
