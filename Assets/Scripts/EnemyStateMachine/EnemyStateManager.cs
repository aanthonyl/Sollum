using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public delegate void PauseAction();

public class EnemyStateManager : MonoBehaviour
{
	/* Notes ====================================
    * EnemyStateManager
    *   - Controls switching states
    *   - Hold references to all of the different states
    *===========================================*/

	//Current State
	I_EnemyBaseState currentState;

	//All states
	public EnemyStatePatrol PatrolState = new();
	public EnemyStateSearch SearchState = new();

	//Public Variables ========================================================================
	public bool isPlayerHidden = false;
	public bool drawHearingDistance = false;
	public NavMeshAgent agent;
	public Transform target;
	public PauseAction pauseAction = null;

	//Private Variables ========================================================================
	private List<Transform> patrolPoints = new();
	BoxCollider sight;
	SpriteRenderer rend;
	Animator anim;
	float navRadius = 5f;
	[SerializeField]
	float maxHearingDist = 5f;
	[SerializeField]
	float pauseTime = 1f;

	private void OnDrawGizmos()
	{
		if (drawHearingDistance)
		{
			Gizmos.DrawWireSphere(transform.position, maxHearingDist);
		}
	}

	void Start()
	{
		//Get patrol points
		Transform pathsParent = this.transform.parent.GetChild(this.transform.GetSiblingIndex() + 1); //gets Paths gameobject
		for (int childIter = 0; childIter < pathsParent.childCount; childIter++) //Loop through paths children
		{
			// Debug.Log("Added" + pathsParent.GetChild(childIter).name);
			patrolPoints.Add(pathsParent.GetChild(childIter)); //Add patrol points (Children) to list
		}

		//Set State and enter state
		currentState = PatrolState; //Set current state to patrol
		currentState.EnterState(this); //Make specific enemy enter the current state
	}

	void Update()
	{
		currentState.UpdateState(this);
	}

	private void OnTriggerEnter(Collider col)
	{
		//If enemy sees player, chase
		if (col.gameObject.CompareTag("Player") && !isPlayerHidden) //if what enters the collider is the player AND the player is not hidden
		{
			SawPlayer(col.gameObject.transform);
		}
	}

	/* SwitchState ====================================
    *   - Switches state to whatever state is passed in
    *   - Called in Update state of the state's script
    *===========================================*/
	public void SwitchState(I_EnemyBaseState state)
	{
		currentState = state; //update current state to whatever the next state is
		currentState.EnterState(this); //Set state for gameobject
	}

	/* PlayerHidden ====================================
    *   - sets isPlayerHidden to "state"
    *===========================================*/
	public void PlayerHidden(bool state)
	{
		isPlayerHidden = state;
	}

	/* SawPlayer ====================================
    *   - Called when enemy sees player (i.e. not blocked or in a hiding spot)
    *   - Sets target to player, switches to chase state
    *===========================================*/
	public void SawPlayer(Transform player)
	{
		target = player;
		//SWITCH TO CHASE STATE HERE
	}

	/* HeardNoise ====================================
    *   - Called when enemy sees player (i.e. not blocked or in a hiding spot)
    *   - Sets target to player, switches to chase state
    *===========================================*/
	public void HeardNoise(Transform noise)
	{
		Debug.Log("Invoked");
		if (Vector3.Distance(transform.position, noise.position) <= maxHearingDist)
		{
			target = noise;
			SwitchState(SearchState);
		}
	}

	/* SwitchState ====================================
    *   - Takes a vector 3 "spot" and finds the nearest spot on the navmesh within radius
    *   - Returns a vector3
    *===========================================*/
	public Vector3 NearestOnNavmesh(Vector3 spot)
	{
		if (!NavMesh.SamplePosition(spot, out NavMeshHit nearestSpot, navRadius, 1))
			Debug.LogError("Failed to find spot on NavMesh near" + spot);
		return nearestSpot.position;
	}

	public void CallReturnToPatrol()
	{
		StartCoroutine(ReturnToPatrol());
	}

	public IEnumerator ReturnToPatrol()
	{
		yield return new WaitForSeconds(pauseTime);
		SwitchState(PatrolState);
	}

	//Accessor Methods
	public Transform GetPatrolPoint(int iter)
	{
		return patrolPoints[iter];
	}

	public int GetPatrolPointsCount()
	{
		return patrolPoints.Count;
	}

	public float GetPauseTime()
	{
		return pauseTime;
	}
}