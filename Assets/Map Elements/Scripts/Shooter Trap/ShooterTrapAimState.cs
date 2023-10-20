using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTrapAimState : ShooterTrapState
{
    public bool IsInAttackAngle;
    public ShooterTrapAttackState ShooterTrapAttackState;
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
            IsInAttackAngle = ShooterTrapController.Aim(player);

            if (IsInAttackAngle)
            {
                return ShooterTrapAttackState;
            }
            else
            {
                return this;
            }
        }        
    }
}
