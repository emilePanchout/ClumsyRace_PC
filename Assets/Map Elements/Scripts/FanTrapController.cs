using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTrapController : MonoBehaviour
{
    public float RotationSpeed;
    public float FanForce;

    // Update is called once per frame
    void Update()
    {
        transform.Find("WHEEL").Rotate(new Vector3(0, 0, 10), RotationSpeed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.parent.GetComponentInParent<Rigidbody>().velocity.z < FanForce)
        {
            other.transform.parent.GetComponentInParent<Rigidbody>().velocity = other.transform.parent.GetComponentInParent<Rigidbody>().velocity + transform.forward;
        }
    }
}
