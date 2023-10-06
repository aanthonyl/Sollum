using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateChase : I_EnemyBaseState
{
    /* Notes ====================================
    * EnemyStateChase
    *   - How did we get here   : Enemy Saw player
    *   - What is happening     : Enemy runs towards the player, stops at certain distance if 
    *   - What will stop this   : Player gets certain distance away (Call return to patrol)
    *===========================================*/

    float stoppingDistance;
    float distToPlayer;

    /* Enter State =============================
    *   - When the state is entered, what happens?
    ============================================*/
    public override void EnterState(EnemyStateManager enemy)
    {
        if (enemy.meleeType)
        {
            stoppingDistance = 1f;
        }
        else if (enemy.shootType)
        {
            stoppingDistance = 5f;
        }
        else if (enemy.throwType)
        {
            stoppingDistance = 3f;
        }
        else
        {
            Debug.Log("No enemy type selected");
            stoppingDistance = 1f;
        }
    }

    /* Update State =============================
    *   - While in this state, what happens?
    ============================================*/
    public override void UpdateState(EnemyStateManager enemy)
    {
        distToPlayer = Vector3.Distance(enemy.agent.transform.position, enemy.target.position) - enemy.playerHeight;
        if (distToPlayer > stoppingDistance)
        {
            enemy.agent.destination = enemy.target.position; //move towards player
        }

        if (distToPlayer >= enemy.loseAggroDist)
        {
            enemy.StartCoroutine(enemy.ReturnToPatrol());
        }

        //Attacking will be dealt with in Kayla and jacks scripts,
        //Just important to make sure the stopping distances are the same!
        //Maybe link and find from kayla and jacks scripts
    }
}
