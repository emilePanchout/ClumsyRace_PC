using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : NetworkBehaviour
{
    public ConnectionApprovalHandler connectionHandler;

    public NetworkVariable<int> playerCount;
    public NetworkVariable<int> maxPlayerCount;

    public List<NetworkObject> playerList;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override void OnNetworkSpawn()
    {
        UpdatePlayerList();
    }

    public  void Update()
    {
        //UpdatePlayerList();
    }

    public void UpdatePlayerList()
    {
        if (IsServer)
        {
            playerCount.Value = connectionHandler.numberOfPlayers;
            maxPlayerCount.Value = connectionHandler.MaxNumberOfPlayers;


            playerList = new List<NetworkObject>();

            for (int i = 0; i < NetworkManager.Singleton.ConnectedClients.Count; i++)
            {
                playerList.Add(NetworkManager.Singleton.ConnectedClients[(ulong)i].PlayerObject);

            }
            Debug.Log("Player list updated to " + playerCount.Value);
        }

    }
}
