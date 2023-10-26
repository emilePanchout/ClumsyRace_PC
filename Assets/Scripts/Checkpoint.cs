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
        
        if (other.CompareTag("Player") && NetworkManager.Singleton.LocalClient.PlayerObject == other.gameObject.transform.parent.parent.gameObject.GetComponent<NetworkObject>() && !isPassed)
        {
            GameObject player = other.gameObject.transform.parent.parent.gameObject;
            player.GetComponent<Player>().lastCheckpoint = SpawnPoint;
            isPassed = true;
            audio.Play();
            particle.Play();

        }
    }
}
