using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : NetworkBehaviour
{
    public NetworkVariable<int> SpawnPointNumber;
    public GameObject lastCheckpoint;

    public float Timer;
    public bool IsRacing;

    public CinemachineBrain CameraBrain;
    private TMP_Text TimerText;

    private bool isLockedByPlayer;

    public string PlayerName = "Unknown";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(IsRacing)
        {
            Timer += Time.deltaTime;
            TimerText.text = (Mathf.Round(Timer * 100f) * 0.01f).ToString();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetPlayerSpawnPointServerRpc(int spawnPointNumber)
    {
        SpawnPointNumber.Value = spawnPointNumber;
    }

    public void StartTimer()
    {
        IsRacing = true;
        TimerText = GameObject.Find("Timer").GetComponent<TMP_Text>();
        TimerText.enabled = true;
        Timer = 0;
    }

    public float StopTimer()
    {
        IsRacing = false;
        return Timer;
    }

    public void OnToggleLock(InputAction.CallbackContext context)
    {
        if (!context.action.triggered || !IsLocalPlayer)
        {
            return;
        }

        if(Cursor.lockState.Equals(CursorLockMode.None) && !isLockedByPlayer)
        {
            return;
        }

        if (isLockedByPlayer)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isLockedByPlayer = false;
            CameraBrain.enabled = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isLockedByPlayer = true;
            CameraBrain.enabled = false;
        }
    }
}
