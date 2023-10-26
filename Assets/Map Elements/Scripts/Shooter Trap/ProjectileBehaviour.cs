using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileBehaviour : NetworkBehaviour
{
    public ShooterTrapStateManager shooterParent;
    public ShooterTrapPatrolState patrolState;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            other.transform.parent.GetComponentInParent<Player>().TeleportToCheckpoint();
            
            shooterParent.Player = null;
            patrolState.CanSeePlayer = false;
            
            gameObject.GetComponent<NetworkObject>().Despawn();
        }
    }
}
