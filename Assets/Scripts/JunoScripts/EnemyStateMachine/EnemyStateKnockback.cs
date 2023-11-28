using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateKnockback : I_EnemyBaseState
{
    /* Notes ====================================
    * EnemyStatePatrol
    *   - How did we get here   : This is the start/Enemy couldn't find player
    *   - What is happening     : Enemy is following a set path
    *   - What will stop this   : Enemy sees the player
    *===========================================*/

    /* Enter State =============================
    *   - When the state is entered, what happens?
    ============================================*/
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.StartCoroutine(enemy.ReturnToChase());
    }

    /* Update State =============================
    *   - While in this state, what happens?
    ============================================*/
    public override void UpdateState(EnemyStateManager enemy)
    {
        // Debug.Log("Being Knocked Back");
    }
}
