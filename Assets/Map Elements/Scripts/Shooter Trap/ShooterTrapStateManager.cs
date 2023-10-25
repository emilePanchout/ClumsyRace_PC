using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTrapStateManager : MonoBehaviour
{
    public ShooterTrapState currentState;
    public ShooterTrapPatrolState ShooterTrapPatrolState;

    public GameObject Player = null;

    // Update is called once per frame
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        ShooterTrapState nextState;
        if (currentState.ToString() == "Patrol State")
        {
            nextState = currentState?.RunCurrentState();
        }
        else
        {
            nextState = currentState?.RunCurrentState(Player);
        }

        if (nextState != null )
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(ShooterTrapState nextState)
    {
        currentState = nextState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player == null && other.CompareTag("Player"))
        {
            Player = other.gameObject;
            ShooterTrapPatrolState.CanSeePlayer = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Player == null && other.CompareTag("Player"))
        {
            Player = other.gameObject;
            ShooterTrapPatrolState.CanSeePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player = null;
            ShooterTrapPatrolState.CanSeePlayer = false;
        }
    }
}
