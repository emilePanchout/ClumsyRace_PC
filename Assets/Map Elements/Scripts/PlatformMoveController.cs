using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlatformMoveController : NetworkBehaviour
{
    public MovingAxis _MovingAxis;

    public float MovingSpeed;
    public float MovingDistance;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        if(NetworkManager.Singleton.IsServer)
        {
            gameObject.GetComponent<NetworkObject>().Spawn();
        }

        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            if (_MovingAxis == MovingAxis.Z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startPosition.z + Mathf.PingPong(MovingSpeed * Time.time, MovingDistance));
            }
            else if (_MovingAxis == MovingAxis.X)
            {
                transform.position = new Vector3(startPosition.x + Mathf.PingPong(MovingSpeed * Time.time, MovingDistance), transform.position.y, transform.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (NetworkManager.Singleton.IsServer && other.CompareTag("Player"))
        {
            other.transform.parent.parent.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (NetworkManager.Singleton.IsServer && other.CompareTag("Player"))
        {
            other.transform.parent.parent.SetParent(null);
        }
    }
}

public enum MovingAxis
{
    X,
    Z
}