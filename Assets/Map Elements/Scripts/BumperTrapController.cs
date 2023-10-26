using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperController : MonoBehaviour
{
    public float BumpForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var direction = (other.transform.parent.parent.position - transform.position).normalized;
            other.transform.parent.GetComponentInParent<Rigidbody>().AddForce(direction * BumpForce, ForceMode.Impulse);
        }
    }
}
