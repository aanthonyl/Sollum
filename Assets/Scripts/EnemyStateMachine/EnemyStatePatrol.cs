using UnityEngine;

public class EnemyStatePatrol : I_EnemyBaseState
{
	/* Notes ====================================
    * EnemyStatePatrol
    *   - How did we get here   : This is the start/Enemy couldn't find player
    *   - What is happening     : Enemy is following a set path
    *   - What will stop this   : Enemy sees the player
    *===========================================*/

	//Private Variables
	int patrolPointIter;

	/* Enter State =============================
    *   - When the state is entered, what happens?
    ============================================*/
	public override void EnterState(EnemyStateManager enemy)
	{
		// Debug.Log("Patrolling");
		patrolPointIter = 1;
		enemy.target = enemy.GetPatrolPoint(patrolPointIter); //set First target point to the next patrol point in the list
															  //The enemy will always start out at the first point in the list. 
	}

	/* Update State =============================
    *   - While in this state, what happens?
    ============================================*/
	public override void UpdateState(EnemyStateManager enemy)
	{
		//Move to target point
		enemy.agent.destination = enemy.target.position;

		// Debug.Log(Vector3.Distance(enemy.agent.transform.position, targetPoint.position));
		//Once at at target point...
		if (Vector3.Distance(enemy.transform.position, enemy.target.position) - 1f < 1)
		{
			//Select next target point
			patrolPointIter++;
			if (patrolPointIter >= enemy.GetPatrolPointsCount())
				patrolPointIter = 0;

			enemy.target = enemy.GetPatrolPoint(patrolPointIter);
			enemy.transform.LookAt(enemy.target.position);
		}

	}
}
