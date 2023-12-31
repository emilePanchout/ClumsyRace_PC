using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectionApprovalHandler : MonoBehaviour
{
    private NetworkManager m_NetworkManager;
    public PlayerManager playerManager;

    public int MaxNumberOfPlayers = 6;
    public int numberOfPlayers = 0;

    public TMP_Text errorText;

    void Start()
    {
        m_NetworkManager = GetComponent<NetworkManager>();

        if (m_NetworkManager != null)
        {
            m_NetworkManager.OnClientDisconnectCallback += OnClientDisconnectCallback;
            m_NetworkManager.ConnectionApprovalCallback = CheckApprovalCallback;
        }

        if (MaxNumberOfPlayers == 0)
        {
            MaxNumberOfPlayers++;
        }
    }


    void CheckApprovalCallback(NetworkManager.ConnectionApprovalRequest req, NetworkManager.ConnectionApprovalResponse response)
    {
        bool isApproved = true;
        numberOfPlayers++;

        if (numberOfPlayers > MaxNumberOfPlayers)
        {
            isApproved = false;
            response.Reason = "Too many players in lobby!";
        }

        if (SceneManager.GetActiveScene().name == "Race")
        {
            isApproved = false;
            response.Reason = "Race already started";
        }

        response.Approved = isApproved;
        response.CreatePlayerObject = isApproved;
        response.Position = new Vector3(0, 3, 0);
        playerManager.UpdatePlayerList();


    }

    void OnClientDisconnectCallback(ulong obj)
    {
        if (!m_NetworkManager.IsServer && m_NetworkManager.DisconnectReason != string.Empty && !m_NetworkManager.IsApproved)
        {
            errorText.text = m_NetworkManager.DisconnectReason;
            Debug.Log($"Approval Declined Reason: {m_NetworkManager.DisconnectReason}");
        }


        numberOfPlayers--;
        playerManager.UpdatePlayerList();

    }
}
