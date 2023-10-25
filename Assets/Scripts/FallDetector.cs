using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject.transform.parent.transform.parent.gameObject;
            player.GetComponent<Player>().TeleportToCheckpoint();
        }
    }

}
