using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log(other.name + " hit!");
            // TODO : Kill/Respawn player
            Destroy(gameObject);
        }
    }
}
