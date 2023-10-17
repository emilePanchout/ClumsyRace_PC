using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Text.RegularExpressions;

public class Manager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject connectionPanel;

    public bool isHost;

    public TMP_InputField ipField;
    public TMP_InputField portField;

    public void SelectHost()
    {
        mainPanel.SetActive(false);
        connectionPanel.SetActive(true);
        isHost = true;
    }

    public void SelectClient()
    {
        mainPanel.SetActive(false);
        connectionPanel.SetActive(true);
        isHost = false;
    }

    public void Cancel()
    {
        mainPanel.SetActive(true);
        connectionPanel.SetActive(false);
    }

    public void StartGame()
    {
        if(isHost)
        {
            StartHost();
        }
        else
        {
            StartClient();
        }
    }


    void SetUtpConnectionData()
    {
        if(ipField.text == "")
        {
            ipField.text = "127.0.0.1";
        }
        if (portField.text == "" || portField.text == "0")
        {
            portField.text = "9990";
        }

        var sanitizedIPText = SanitizeAlphaNumeric(ipField.text);
        var sanitizedPortText = SanitizeAlphaNumeric(portField.text);

        ushort.TryParse(sanitizedPortText, out var port);

        var utp = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        utp.SetConnectionData(sanitizedIPText, port);
    }

   
    private static string SanitizeAlphaNumeric(string dirtyString)
    {
        return Regex.Replace(dirtyString, "[^A-Za-z0-9.]", "");
    }

    private void StartHost()
    {
        SetUtpConnectionData();

        var result = NetworkManager.Singleton.StartHost();

        if (result)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("Lobby", UnityEngine.SceneManagement.LoadSceneMode.Single);

            return;
        }
    }

    private void StartClient()
    {
        SetUtpConnectionData();

        NetworkManager.Singleton.StartClient();
    }
}
