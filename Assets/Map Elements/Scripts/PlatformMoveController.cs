using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveController : MonoBehaviour
{
    public MovingAxis _MovingAxis = MovingAxis.Z;

    public float MovingSpeed = 1;
    public float MovingDistance = 4;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}

public enum MovingAxis
{
    X,
    Z
}