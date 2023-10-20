using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShooterTrapState : MonoBehaviour
{
    public abstract ShooterTrapState RunCurrentState(GameObject player = null);
}
