using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTrapStateManager : MonoBehaviour
{
    public ShooterTrapState currentState;
    public ShooterTrapPatrolState ShooterTrapPatrolState;

    private GameObject player = null;

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
            nextState = currentState?.RunCurrentState(player);
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
        if (player == null && other.name == "Player")
        {
            player = other.gameObject;
            ShooterTrapPatrolState.CanSeePlayer = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (player == null && other.name == "Player")
        {
            player = other.gameObject;
            ShooterTrapPatrolState.CanSeePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player = null;
            ShooterTrapPatrolState.CanSeePlayer = false;
        }
    }
}
