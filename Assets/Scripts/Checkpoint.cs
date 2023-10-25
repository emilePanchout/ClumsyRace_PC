using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Checkpoint : MonoBehaviour
{

    public Transform SpawnPoint;
    public bool isPassed;
    public AudioSource audio;
    public ParticleSystem particle;

    private void OnTriggerEnter(Collider other)
    {
        GameObject player = other.gameObject.transform.parent.transform.parent.gameObject;
        if (other.CompareTag("Player") && NetworkManager.Singleton.LocalClient.PlayerObject == player.GetComponent<NetworkObject>() && !isPassed)
        {
            player.GetComponent<Player>().lastCheckpoint = SpawnPoint;
            isPassed = true;
            audio.Play();
            particle.Play();

        }
    }
}
