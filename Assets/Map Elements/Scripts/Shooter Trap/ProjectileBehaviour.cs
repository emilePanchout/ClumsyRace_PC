using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileBehaviour : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            other.transform.parent.GetComponentInParent<Player>().TeleportToCheckpoint();
            
            GameObject.Find("Trap shooter").gameObject.GetComponent<ShooterTrapStateManager>().Player = null;
            GameObject.Find("Patrol State").gameObject.GetComponent<ShooterTrapPatrolState>().CanSeePlayer = false;
            
            gameObject.GetComponent<NetworkObject>().Despawn();
        }
    }
}
