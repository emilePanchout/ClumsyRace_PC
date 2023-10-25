using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float airMultiplier;

    [Header("Ground Check")]
    bool grounded;

    public Transform orientation;

    Vector2 movement;
    Vector3 moveDirection;

    Rigidbody rb;

    bool CanMove = true;
    bool CanJump = true;

    [Header("Camera")]
    public Camera playerCamera;
    private Vector2 _look; 
    private float RotationX = 0;
    public float rotationSpeed = 15;
    public float xAngleUpperLimit = 15;
    public float xAngleLowerLimit = 15;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(!IsLocalPlayer)
            enabled = false;
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            MovePlayer();
        }

        grounded = Physics.Raycast(transform.position, Vector3.down, 0.2f);//, Ground);

        if (Input.GetKey(KeyCode.Space) && grounded && CanJump)
        {
            Jump();
        }

        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag * Time.deltaTime;
        }
        else
        {
            rb.drag = 0;
        }


    }

    // Update is called once per frame
    void Update()
    {
        RotationX += -_look.y * rotationSpeed * Time.deltaTime;
        RotationX = Mathf.Clamp(RotationX, xAngleLowerLimit, xAngleUpperLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(RotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, _look.x * rotationSpeed * Time.deltaTime, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _look = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void EnableMovement(bool enable)
    {
        CanMove = enable;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * movement.y + orientation.right * movement.x;
        
        if(grounded)
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
