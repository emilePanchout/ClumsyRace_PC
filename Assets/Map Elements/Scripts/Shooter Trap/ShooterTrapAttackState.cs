using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTrapAttackState : ShooterTrapState
{
    public ShooterTrapPatrolState ShooterTrapPatrolState;
    public ShooterTrapController ShooterTrapController;

    public override ShooterTrapState RunCurrentState(GameObject player)
    {
        if (player == null)
        {
            return ShooterTrapPatrolState;
        }
        else
        {
            ShooterTrapController.Shoot(player);
            return this;
        }
    }
}
