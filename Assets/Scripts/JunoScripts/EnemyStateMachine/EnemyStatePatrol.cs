using System.Collections;
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
	bool isPatrolling = false;
	bool stationary = false;

	/* Enter State =============================
    *   - When the state is entered, what happens?
    ============================================*/
	public override void EnterState(EnemyStateManager enemy)
	{
		Debug.Log("Patrolling");
		if (enemy.GetPatrolPointsCount() <= 1)
		{
			stationary = true;
		}

		patrolPointIter = 0;
		enemy.target = enemy.GetPatrolPoint(patrolPointIter); //set First target point to the next patrol point in the list
		isPatrolling = true;
	}

	/* Update State =============================
    *   - While in this state, what happens?
    ============================================*/
	public override void UpdateState(EnemyStateManager enemy)
	{
		if (isPatrolling) //Move to target point
			enemy.agent.destination = enemy.target.position;

		// Debug.Log(Vector3.Distance(enemy.agent.transform.position, enemy.target.position));
		//Once at at target point...
		if (isPatrolling && Vector3.Distance(enemy.transform.position, enemy.target.position) - enemy.playerHeight < 1)
		{
			isPatrolling = false;
			if (!stationary)
				enemy.StartCoroutine(PauseAtPoint(enemy));
		}

	}

	IEnumerator PauseAtPoint(EnemyStateManager enemy)
	{
		yield return new WaitForSeconds(enemy.GetPausePatrolTime());
		//Select next target point
		patrolPointIter++;
		if (patrolPointIter >= enemy.GetPatrolPointsCount())
			patrolPointIter = 0;

		enemy.target = enemy.GetPatrolPoint(patrolPointIter);
		enemy.transform.LookAt(enemy.target.position);
		isPatrolling = true;
	}
}
