using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTrapPatrolState : ShooterTrapState
{
    public bool CanSeePlayer;
    public ShooterTrapAimState ShooterTrapAimState;
    public ShooterTrapController ShooterTrapController;

    public override ShooterTrapState RunCurrentState(GameObject player = null)
    {
        if (CanSeePlayer)
        {
            return ShooterTrapAimState;
        }
        else
        {
            ShooterTrapController.Patrol();
            return this;
        }
    }
}
