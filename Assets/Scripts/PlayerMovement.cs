using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 m_V2Look;
    private Vector2 m_V2Move;
    private Vector2 m_V2Jump;

    private Vector3 m_V3MoveDirection = Vector3.zero;
    private float m_fRotationX = 0;


    private CharacterController m_characterController;
    public Camera m_playerCamera;


    [Header("Current state")]
    [SerializeField] private bool m_isJump;
    [SerializeField] private bool m_isRunning;


    [Header("Movements")]
    public float walkSpeed;
    public float jumpStrength;
    public float gravityStrength;

    [Header("Camera")]
    public float rotationSpeed;
    public float xAngleUpperLimit;
    public float xAngleLowerLimit;





    //////////////////////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = walkSpeed * m_V2Move.y;
        float curSpeedY = walkSpeed * m_V2Move.x;
        float movementDirectionY = m_V3MoveDirection.y;

        m_V3MoveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (m_isJump && m_characterController.isGrounded)
        {
            m_V3MoveDirection.y = jumpStrength;
        }
        else
        {
            m_V3MoveDirection.y = movementDirectionY;
        }

        if (!m_characterController.isGrounded)
        {
            m_V3MoveDirection.y -= gravityStrength * Time.deltaTime;
        }

        m_characterController.Move(m_V3MoveDirection * Time.deltaTime);
        m_fRotationX += -m_V2Look.y * rotationSpeed * Time.deltaTime;
        m_fRotationX = Mathf.Clamp(m_fRotationX, xAngleLowerLimit, xAngleUpperLimit);
        m_playerCamera.transform.localRotation = Quaternion.Euler(m_fRotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, m_V2Look.x * rotationSpeed * Time.deltaTime, 0);
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////

    public void OnLook(InputAction.CallbackContext context)
    {
        m_V2Look = context.ReadValue<Vector2>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        m_V2Move = context.ReadValue<Vector2>();
        Debug.Log(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        m_isJump = Convert.ToBoolean(context.ReadValue<float>());
    }
}
