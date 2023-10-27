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

            
            if(other.transform.parent.GetComponentInParent<NetworkObject>().IsLocalPlayer)
            {
                DespawnProjectileServerRpc();
            }

        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void DespawnProjectileServerRpc()
    {
        gameObject.GetComponent<NetworkObject>().Despawn();
    }
}
