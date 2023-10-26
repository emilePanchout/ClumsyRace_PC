using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrampolineController : MonoBehaviour
{
    public float LaunchForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent.GetComponentInParent<Rigidbody>().AddForce(transform.up * LaunchForce, ForceMode.Impulse);
        }
    }
}
