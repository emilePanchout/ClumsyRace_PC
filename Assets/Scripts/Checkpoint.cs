using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Checkpoint : MonoBehaviour
{

    public Transform SpawnPoint;
    public bool isPassed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && NetworkManager.Singleton.LocalClient.PlayerObject == other.gameObject.GetComponent<NetworkObject>() && !isPassed)
        {
            other.gameObject.GetComponent<Player>().lastCheckpoint = SpawnPoint;
            isPassed = true;

        }
    }
}
