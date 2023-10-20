using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    private Vector2 _look;
    private Vector2 _move;
    [SerializeField] private bool _isJump;

    private Vector3 MoveDirection = Vector3.zero;
    private float RotationX = 0;


    private CharacterController m_characterController;
    public Camera m_playerCamera;


    [Header("Current state")]



    [Header("Movements")]
    public float walkSpeed = 4;
    public float jumpStrength = 4;
    public float gravityStrength = 8;

    [Header("Camera")]
    public float rotationSpeed = 15;
    public float xAngleUpperLimit = 15;
    public float xAngleLowerLimit = 15;





    //////////////////////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (IsOwner)
        {
            //MoveServerRpc(_look, _move, _isJump);

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float curSpeedX = walkSpeed * _move.y;
            float curSpeedY = walkSpeed * _move.x;
            float movementDirectionY = MoveDirection.y;

            MoveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (_isJump && m_characterController.isGrounded)
            {
                MoveDirection.y = jumpStrength;
            }
            else
            {
                MoveDirection.y = movementDirectionY;
            }

            if (!m_characterController.isGrounded)
            {
                MoveDirection.y -= gravityStrength * Time.deltaTime;
            }

            m_characterController.Move(MoveDirection * Time.deltaTime);

            RotationX += -_look.y * rotationSpeed * Time.deltaTime;
            RotationX = Mathf.Clamp(RotationX, xAngleLowerLimit, xAngleUpperLimit);

            m_playerCamera.transform.localRotation = Quaternion.Euler(RotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, _look.x * rotationSpeed * Time.deltaTime, 0);
        }

    }

    [ServerRpc]
    private void MoveServerRpc(Vector2 look, Vector2 move, bool isJump)
    {
        Debug.Log("RPC");
        
    }




    //////////////////////////////////////////////////////////////////////////////////////////////////////

    public void OnLook(InputAction.CallbackContext context)
    {
        _look = context.ReadValue<Vector2>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _isJump = Convert.ToBoolean(context.ReadValue<float>());
    }
}
