using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrampolineController : MonoBehaviour
{
    public float LaunchForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name + " entered");
            //other.GetComponent<Rigidbody>().AddForce(transform.up * LaunchForce, ForceMode.Impulse);
            other.transform.parent.GetComponentInParent<Rigidbody>().AddForce(transform.up * LaunchForce, ForceMode.Impulse);
        }
    }
}
