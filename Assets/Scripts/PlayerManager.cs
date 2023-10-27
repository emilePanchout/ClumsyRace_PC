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

    public string mapName;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override void OnNetworkSpawn()
    {
        UpdatePlayerList();
    }

    public void UpdatePlayerList()
    {
        if (IsServer)
        {
            playerCount.Value = connectionHandler.numberOfPlayers;
            maxPlayerCount.Value = connectionHandler.MaxNumberOfPlayers;
        }

    }

    public void AddPlayer(NetworkObject player)
    {
        playerList.Add(player);
        Debug.Log("Player added");
    }

    public void RemovePlayer(NetworkObject player)
    {
        playerList.Remove(player);
    }
}
